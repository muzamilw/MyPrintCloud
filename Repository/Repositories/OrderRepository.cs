﻿using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MPC.Models.Common;
using MPC.Interfaces.WebStoreServices;
using System.Web;
using System.Data.Common;
using System.Reflection;
using System.IO;
using MPC.ExceptionHandling;
namespace MPC.Repository.Repositories
{
    public class OrderRepository : BaseRepository<Estimate>, IOrderRepository
    {

        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        public OrderRepository(IUnityContainer container, IWebstoreClaimsHelperService myClaimHelper)
            : base(container)
        {
            this._myClaimHelper = myClaimHelper;
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Estimate> DbSet
        {
            get
            {
                return db.Estimates;
            }
        }

        public int GetFirstItemIDByOrderId(int orderId)
        {

            try
            {
                List<Item> itemsList = GetOrderItems(orderId);
                if (itemsList != null && itemsList.Count > 0)
                {
                    return Convert.ToInt32(itemsList[0].ItemId);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<Item> GetOrderItems(int OrderId)
        {

            return (from r in db.Items
                    where r.EstimateId == OrderId && (r.ItemType == null || r.ItemType != (int)ItemTypes.Delivery)
                    select r).ToList();
        }

        public long CreateNewOrder(long CompanyId, long ContactId, long OrganisationId, string orderTitle = null)
        {
            Prefix prefix = db.Prefixes.Where(c => c.SystemSiteId == 1).FirstOrDefault();

            Estimate orderObject = new Estimate();

            orderObject.CompanyId = (int)CompanyId;

            orderObject.OrganisationId = OrganisationId;

            orderObject.CompanyName = "N/A";

            orderObject.ContactId = ContactId;

            orderObject.isEstimate = false;

            orderObject.StatusId = (short)OrderStatus.ShoppingCart;

            orderObject.SectionFlagId = 145;

            orderObject.Estimate_Name = string.IsNullOrWhiteSpace(orderTitle) ? "WebStore New Order" : orderTitle;

            orderObject.isDirectSale = false;

            orderObject.Order_Date = DateTime.Now;

            orderObject.CreationDate = DateTime.Now;

            orderObject.CreationTime = DateTime.Now;

            orderObject.Order_CreationDateTime = DateTime.Now;

            if (prefix != null)
            {
                orderObject.Order_Code = prefix.OrderPrefix + "-001-" + prefix.OrderNext.ToString();
                prefix.OrderNext = prefix.OrderNext + 1;
            }

            db.Estimates.Add(orderObject);

            if (db.SaveChanges() > 0)
            {
                return orderObject.EstimateId;
            }
            else
            {
                return 0;
            }
        }

        public long GetOrderID(long CustomerId, long ContactId, string orderTitle, long OrganisationId)
        {
            long orderID = 0;

            orderID = GetOrderByContactID(ContactId, OrderStatus.ShoppingCart);

            if (orderID == 0)
            {
                orderID = CreateNewOrder(CustomerId, ContactId, OrganisationId, orderTitle);
            }

            return orderID;
        }

        private long GetOrderByContactID(long contactID, OrderStatus orderStatus)
        {
            int orderStatusID = (int)orderStatus;
            List<Estimate> ordesList = db.Estimates.Include("Items").Where(order => order.ContactId == contactID && order.StatusId == orderStatusID && order.isEstimate == false).Take(1).ToList();
            if (ordesList.Count > 0)
                return ordesList[0].EstimateId;
            else
                return 0;

        }

        public long GetUserShopCartOrderID(int status)
        {
            try
            {
                int custID = 0;
                int contactID = 0;
                long OrderId = 0;
                if (IsUserLoggedIn())
                {

                    contactID = (int)_myClaimHelper.loginContactID();
                    OrderId = (from order in db.Estimates
                               where order.ContactId == contactID && order.StatusId == status && order.isEstimate == false
                               select order.EstimateId).FirstOrDefault();



                }
                else
                {
                    custID = (int)_myClaimHelper.loginContactCompanyID();
                    OrderId = (from order in db.Estimates
                               where order.CompanyId == custID && order.StatusId == status && order.isEstimate == false
                               select order.EstimateId).FirstOrDefault();
                }
                return OrderId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool IsUserLoggedIn()
        {
            if (_myClaimHelper.loginContactID() > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        public ShoppingCart GetShopCartOrderAndDetails(long orderID, OrderStatus orderStatus)
        {
            Estimate tblOrder = null;
            ShoppingCart shopCart = new ShoppingCart();
            short orderStsID = Convert.ToInt16(orderStatus);

            try
            {
                long Orderid = orderID;

                tblOrder = db.Estimates.Where(estm => estm.EstimateId == Orderid && estm.StatusId == orderStsID).FirstOrDefault();
                if (tblOrder != null)
                {
                    if (tblOrder.BillingAddressId != null)
                        shopCart.BillingAddressID = (long)tblOrder.BillingAddressId;
                    else
                        shopCart.BillingAddressID = 0;
                    shopCart.ShippingAddressID = tblOrder.AddressId;
                    shopCart = ExtractShoppingCart(tblOrder);
                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return shopCart;
        }
        public DiscountVoucher GetVoucherRecord(int VId)
        {
            try
            {

                return (from c in db.DiscountVouchers
                        where c.DiscountVoucherId == VId
                        select c).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private ShoppingCart ExtractShoppingCart(Estimate tblEstimate)
        {

            ShoppingCart shopCart = new ShoppingCart();
            List<AddOnCostsCenter> childrenRecordsAllProductItemAddons = null;
            List<Item> ItemsOfOrder = GetOrderItems(Convert.ToInt32(tblEstimate.EstimateId));
            try
            {

                //1. Get All Items and Its Attament in a Singe Instant
                shopCart.CartItemsList = this.ExtractItemsAndAttatchments(ItemsOfOrder, out childrenRecordsAllProductItemAddons);

                //2. Get All Addons Used in that Items
                shopCart.ItemsSelectedAddonsList = childrenRecordsAllProductItemAddons;

                //3. Extract company address if any
                shopCart.AddressesList = this.GetOrderCompanyAllAddresses(tblEstimate); //this.GetOrderCompanyBillingShipingAddresses(tblEstimate);


                //4. Set Order Level Fields
                shopCart.DiscountVoucherID = (tblEstimate.DiscountVoucherID.HasValue && tblEstimate.DiscountVoucherID.Value > 0) ? tblEstimate.DiscountVoucherID.Value : 0;
                shopCart.VoucherDiscountRate = (tblEstimate.VoucherDiscountRate.HasValue && tblEstimate.VoucherDiscountRate.Value > 0) ? tblEstimate.VoucherDiscountRate.Value : 0;
                shopCart.DeliveryCostCenterID = (tblEstimate.DeliveryCostCenterId.HasValue && tblEstimate.DeliveryCostCenterId.Value > 0) ? tblEstimate.DeliveryCostCenterId.Value : 0;
                shopCart.DeliveryCost = (tblEstimate.DeliveryCost.HasValue && tblEstimate.DeliveryCost.Value > 0) ? tblEstimate.DeliveryCost.Value : 0;
                //5. get delivery item 
                Item DeliveryItemOfOrder = GetDeliveryOrderItem(tblEstimate.EstimateId);
                if (DeliveryItemOfOrder != null)
                {
                    shopCart.DeliveryTaxValue = DeliveryItemOfOrder.Qty1Tax1Value ?? 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return shopCart;
        }
        private List<ProductItem> ExtractItemsAndAttatchments(List<Item> orderItemsList, out  List<AddOnCostsCenter> childrenRecordsAllProductItemAddons)
        {
            List<ProductItem> productItemsList = new List<ProductItem>();
            List<AddOnCostsCenter> allItemsAddOnsList = new List<AddOnCostsCenter>();

            long? StockID = 0;
            string StockName = null;
            ProductItem prodItem = null;

            orderItemsList.ForEach
                (
                    item =>
                    {

                        if (item.IsOrderedItem.HasValue && item.IsOrderedItem.Value) //gets only attatchments which are added to cart
                        {
                            var Section = db.ItemSections.Where(i => i.ItemId == item.RefItemId & i.SectionNo == 1).FirstOrDefault();

                            if (item.ItemSections != null)
                                StockID = item.ItemSections.Where(i => i.ItemId == item.ItemId & i.SectionNo == 1).Select(o => o.StockItemID1).FirstOrDefault();
                            else
                                StockID = 0;
                            StockName = db.ItemStockOptions.Where(i => i.StockId == StockID && i.ItemId == item.RefItemId).Select(o => o.StockLabel).FirstOrDefault();


                            prodItem = CreateProductItem(item, StockName);
                            prodItem.Attatchment = this.ExtractAttachment(item);

                            productItemsList.Add(prodItem);
                            allItemsAddOnsList.AddRange(this.ExtractAdditionalAddons(item)); //Collects the addons for each item

                        }
                    }
                );

            childrenRecordsAllProductItemAddons = allItemsAddOnsList;
            return productItemsList;
        }
        public ProductItem CreateProductItem(Item tblItem, string PaperName)
        {
            short StatusID = 0;
            int invoiceID = 0;
            if (tblItem.Status != null)
                StatusID = tblItem.Status.StatusId;
            if (tblItem.InvoiceId != null)
                invoiceID = (int)tblItem.InvoiceId;
            long productCategoryID = db.Items.Where(s => s.ItemId == tblItem.ItemId).Select(s => s.ItemId).FirstOrDefault();
            string ProductName = db.ProductCategories.Where(p => p.ProductCategoryId == (int)productCategoryID).Select(s => s.CategoryName).FirstOrDefault();
            ProductItem prodItem = new ProductItem()
            {

                ItemID = (int)tblItem.ItemId,
                Status = StatusID,
                EstimateID = tblItem.EstimateId,
                InvoiceID = invoiceID,
                ProductName = tblItem.ProductName,
                ProductCategoryName = ProductName, //Product category Name
                ProductCategoryID = (int)productCategoryID,
                ImagePath = tblItem.ImagePath,
                ThumbnailPath = tblItem.ThumbnailPath,
                IconPath = tblItem.IconPath,
                IsEnabled = tblItem.IsEnabled,
                IsSpecialItem = tblItem.IsSpecialItem,
                IsPopular = tblItem.IsPopular,
                IsFeatured = tblItem.IsFeatured,
                IsPromotional = tblItem.IsPromotional,
                IsPublished = tblItem.IsPublished,
                ProductType = tblItem.ProductType,
                ProductSpecification = tblItem.ProductSpecification,
                CompleteSpecification = tblItem.CompleteSpecification,
                TipsAndHints = tblItem.TipsAndHints,
                Tax1 = tblItem.Tax1,
                Tax2 = tblItem.Tax2,
                Tax3 = tblItem.Tax3,
                Qty1 = tblItem.Qty1,
                Qty1CostCentreProfit = tblItem.Qty1CostCentreProfit,
                Qty1BaseCharge1 = tblItem.Qty1BaseCharge1,
                Qty1MarkUpID1 = tblItem.Qty1MarkUpId1,
                Qty1MarkUpPercentageValue = tblItem.Qty1MarkUpPercentageValue,
                Qty1MarkUp1Value = tblItem.Qty1MarkUp1Value,
                Qty1NetTotal = tblItem.Qty1NetTotal,
                Qty1Tax1Value = tblItem.Qty1Tax1Value,
                Qty1Tax2Value = tblItem.Qty1Tax2Value,
                Qty1Tax3Value = tblItem.Qty1Tax3Value,
                Qty1GrossTotal = tblItem.Qty1GrossTotal,
                RefItemID = tblItem.RefItemId,
                TemplateID = tblItem.TemplateId.HasValue ? (int)tblItem.TemplateId : (int?)null,
                EstimateProductionTime = tblItem.EstimateProductionTime,
                BaseChargeBroker = tblItem.BaseChargeBroker,
                NetTotalBroker = tblItem.NetTotalBroker,
                MarkUpValBroker = tblItem.MarkUpValueBroker,
                TaxValBroker = tblItem.TaxValueBroker,
                GrossTotalBroker = tblItem.GrossTotalBroker,
                PaperType = PaperName,
                ItemType = tblItem.ItemType,
                ProductWebDescription = tblItem.WebDescription,
            };
            return prodItem;
        }

        private ArtWorkAttatchment ExtractAttachment(Item tblItem)
        {
            ArtWorkAttatchment artWorkAttatchment = null;
            ItemAttachment tblItemAttchment = null;

            //if (tblItem != null) 
            //{ //he uploaded his desgn
            if (tblItemAttchment != null)
            {
                if (tblItem != null && tblItem.ItemAttachments.Count > 0)
                {
                    //Find the pdf he loaded

                    //  tblItemAttchment = tblItem.tbl_item_attachments.Where(attatchment => string.Compare(attatchment.FileType, ".pdf", true) == 0 && attatchment.CustomerID == tblItem.ContactCompanyID && string.Compare(attatchment.Type, Model.UploadFileTypes.Artwork.ToString(), true) == 0).Take(1).FirstOrDefault();



                    // tblItemAttchment = tblItem.tbl_item_attachments.Where(attatchment => attatchment.CustomerID == tblItem.ContactCompanyID && string.Compare(attatchment.Type, Model.UploadFileTypes.Artwork.ToString(), true) == 0).Take(1).FirstOrDefault();
                    List<ItemAttachment> newlistAttach = tblItem.ItemAttachments.Where(attatchment => attatchment.ItemId == tblItem.ItemId && string.Compare(attatchment.Type, UploadFileTypes.Artwork.ToString(), true) == 0).Take(2).ToList();
                    tblItemAttchment = newlistAttach[0];

                    if (tblItemAttchment != null)
                    {
                        if (tblItemAttchment.FileName.Contains("overlay"))
                        {
                            tblItemAttchment = newlistAttach[1];
                        }
                        artWorkAttatchment = new ArtWorkAttatchment();

                        artWorkAttatchment.FileName = tblItemAttchment.FileName;
                        artWorkAttatchment.FileTitle = tblItemAttchment.FileTitle;
                        artWorkAttatchment.FileExtention = tblItemAttchment.FileType;
                        artWorkAttatchment.FolderPath = tblItemAttchment.FolderPath;
                        artWorkAttatchment.UploadFileType = (UploadFileTypes)Enum.Parse(typeof(UploadFileTypes), tblItemAttchment.Type); //Model.UploadFileTypes.Artwork.ToString();
                    }

                }
            }
            //}

            artWorkAttatchment = artWorkAttatchment ?? new ArtWorkAttatchment();

            return artWorkAttatchment;

        }

        private List<AddOnCostsCenter> ExtractAdditionalAddons(Item tblItem)
        {
            List<AddOnCostsCenter> itemAddOnsList = new List<AddOnCostsCenter>();
            ItemSection tblItemFirstSection = null;
            List<SectionCostcentre> tblSectionCostList = null;

            //FirstSection
            if(tblItem.ItemSections != null && tblItem.ItemSections.Count > 0)
            {
                tblItemFirstSection = tblItem.ItemSections.Where(itmSect => itmSect.SectionNo.HasValue && itmSect.SectionNo.Value == 1).FirstOrDefault();
                if (tblItemFirstSection != null)
                {
                    tblSectionCostList = tblItemFirstSection.SectionCostcentres.Where(sectCostCenter => sectCostCenter.IsOptionalExtra == 1).ToList();

                    tblSectionCostList.ForEach(
                        sectCostCenter =>
                        {
                            AddOnCostsCenter addonCostCenter = new AddOnCostsCenter
                            {
                                AddOnName = sectCostCenter.CostCentre.Name,
                                CostCenterID = (int)sectCostCenter.CostCentreId,
                                ItemID = (int)tblItemFirstSection.ItemId,
                                EstimateProductionTime = sectCostCenter.CostCentre.EstimateProductionTime ?? 0
                            };

                            itemAddOnsList.Add(addonCostCenter); // cost center of particular item
                        });

                }

            }
           

            return itemAddOnsList;
        }

        public List<Address> GetOrderCompanyAllAddresses(Estimate tblOrder)
        {
            List<Address> companyAddresesList = null;
            List<Address> tblContactCompanyAddList = null;

            Address modeAddress = null;

            if (tblOrder != null)
            {
                if (tblOrder.Company != null)
                {
                    tblContactCompanyAddList = tblOrder.Company.Addresses.ToList();

                    if (tblContactCompanyAddList != null && tblContactCompanyAddList.Count > 0)
                    {
                        companyAddresesList = new List<Address>();
                        tblContactCompanyAddList.ForEach(tblAdressRow =>
                        {
                            modeAddress = new Address
                            {
                                AddressId = tblAdressRow.AddressId,
                                Address1 = tblAdressRow.Address1,
                                Address2 = tblAdressRow.Address2,
                                AddressName = tblAdressRow.AddressName,
                                IsDefaultAddress = (tblAdressRow.IsDefaultAddress.HasValue && tblAdressRow.IsDefaultAddress.Value) ? true : false,
                                IsDefaultShippingAddress = (tblAdressRow.IsDefaultShippingAddress.HasValue && tblAdressRow.IsDefaultShippingAddress.Value) ? true : false,
                                City = tblAdressRow.City,
                                Tel1 = tblAdressRow.Tel1,
                                State = tblAdressRow.State,
                                PostCode = tblAdressRow.PostCode,
                                Country = tblAdressRow.Country,
                            };

                            companyAddresesList.Add(modeAddress);
                        });
                    }
                }

               
            }


            return companyAddresesList;
        }


        public Item GetDeliveryOrderItem(long OrderId)
        {
            try
            {
                short itemType = Convert.ToInt16(ItemTypes.Delivery);
                return (from r in db.Items
                        where r.EstimateId == OrderId && r.ItemType == itemType
                        select r).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Estimate GetOrderByID(long orderId)
        {

            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Estimates.Where(order => order.EstimateId == orderId && order.isEstimate == false).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsVoucherValid(string voucherCode)
        {

            bool result = true;
            DiscountVoucher discountVocher = null;
            try
            {
                discountVocher = db.DiscountVouchers.Where(discVoucher => discVoucher.VoucherCode == voucherCode && discVoucher.IsEnabled && discVoucher.CompanyId == null).FirstOrDefault();
                if (discountVocher != null)
                {

                    if (discountVocher.ValidFromDate.HasValue && DateTime.Now < discountVocher.ValidFromDate.Value)
                        result = false;

                    else if (discountVocher.ValidUptoDate.HasValue && DateTime.Now > discountVocher.ValidUptoDate.Value)
                        result = false;

                    //else if (discountVocher.OrderID.HasValue)
                    //    result = false;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public Estimate CheckDiscountApplied(int orderId)
        {
            try
            {
                return (from c in db.Estimates
                        where c.EstimateId == orderId
                        select c).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool RollBackDiscountedItems(int orderId, double StateTax, StoreMode Mode)
        {

            double QtyNewTotal = 0;
            double QtyTaxVal = 0;

            List<Item> tblOrder = db.Items.Where(c => c.EstimateId == orderId && c.Qty1CostCentreProfit != null).ToList();
            if (tblOrder != null)
            {
                foreach (var item in tblOrder.Where(i => i.ItemType != Convert.ToInt32(ItemTypes.Delivery)))
                {
                    SectionCostcentre SC = item.ItemSections.FirstOrDefault().SectionCostcentres.Where(c => c.CostCentreId == (int)CostCentresForWeb.WebOrderCostCentre).FirstOrDefault();

                    QtyNewTotal = (double)item.Qty1NetTotal + (double)item.Qty1CostCentreProfit;
                    QtyTaxVal = (QtyNewTotal * StateTax) / 100;
                    item.Qty1NetTotal = QtyNewTotal;
                    item.Qty1BaseCharge1 = QtyNewTotal;
                    item.Qty1Tax1Value = QtyTaxVal;
                    item.Qty1GrossTotal = QtyNewTotal + QtyTaxVal;
                    item.ItemSections.FirstOrDefault().BaseCharge1 += (double)item.Qty1CostCentreProfit;
                    if (SC != null)
                    {
                        SC.Qty1NetTotal += (double)item.Qty1CostCentreProfit;
                        // SC.Qty1MarkUpValue = 0;
                    }
                    item.Qty1CostCentreProfit = 0;
                }
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public double SaveVoucherCodeAndRate(int orderId, string VCode)
        {
            try
            {

                Estimate record = db.Estimates.Where(c => c.EstimateId == orderId).FirstOrDefault();
                DiscountVoucher discountVocher = db.DiscountVouchers.Where(discVoucher => discVoucher.VoucherCode == VCode && discVoucher.IsEnabled).FirstOrDefault();
                if (record != null)
                {
                    record.DiscountVoucherID = Convert.ToInt16(discountVocher.DiscountVoucherId);
                    record.VoucherDiscountRate = discountVocher.DiscountRate;
                }
                if (db.SaveChanges() > 0)
                {
                    return discountVocher.DiscountRate;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception e)
            {
                return 0;
                throw e;
            }
        }

        public double PerformVoucherdiscountOnEachItem(int orderId, OrderStatus orderStatus, double StateTax, double VDiscountRate, StoreMode Mode)
        {
            short status = (short)orderStatus;
            double DiscountedAmount = 0;
            double TotalDiscAmount = 0;
            double TotalDiscAmountBroker = 0;
            double QtyNewTotal = 0;
            double QtyTaxVal = 0;


            List<Item> tblOrder = db.Items.Where(c => c.EstimateId == orderId && (c.ItemType == null || c.ItemType != 2) && (c.Qty1CostCentreProfit == null || c.Qty1CostCentreProfit == 0)).ToList();
            if (tblOrder != null)
            {
                foreach (var item in tblOrder)
                {
                    SectionCostcentre SC = item.ItemSections.FirstOrDefault().SectionCostcentres.Where(c => c.CostCentreId == (int)CostCentresForWeb.WebOrderCostCentre).FirstOrDefault();

                    DiscountedAmount = CalCulateVoucherDiscount(Convert.ToDouble(item.Qty1BaseCharge1), VDiscountRate);
                    item.Qty1CostCentreProfit = DiscountedAmount;
                    TotalDiscAmount += DiscountedAmount;
                    QtyNewTotal = item.Qty1NetTotal - DiscountedAmount ?? 0;
                    QtyTaxVal = (QtyNewTotal * StateTax) / 100;
                    item.Qty1NetTotal = QtyNewTotal;
                    item.Qty1BaseCharge1 = QtyNewTotal;
                    item.Qty1Tax1Value = QtyTaxVal;
                    item.Qty1GrossTotal = QtyNewTotal + QtyTaxVal;
                    item.ItemSections.FirstOrDefault().BaseCharge1 -= DiscountedAmount;
                    if (SC != null)
                    {
                        SC.Qty1NetTotal -= DiscountedAmount;
                        //SC.Qty1MarkUpValue = -DiscountedAmount;
                    }
                }
                if (db.SaveChanges() > 0)
                {

                    return TotalDiscAmount;

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }

        }
        private double CalCulateVoucherDiscount(double subTotal, double VoucherRate)
        {
            double discRate = VoucherRate;
            double discountedAmount = 0;

            if (discRate > 0)
            {
                discountedAmount = CalculatePercentage(subTotal, discRate);
            }
            return discountedAmount;
        }

        public static double CalculatePercentage(double itemValue, double percentageValue)
        {
            double percentValue = 0;

            percentValue = itemValue * (percentageValue / 100);

            return percentValue;
        }
        public bool ResetOrderVoucherCode(int orderId)
        {

            Estimate OrderRecord = db.Estimates.Where(c => c.EstimateId == orderId).FirstOrDefault();
            if (OrderRecord != null)
            {
                OrderRecord.DiscountVoucherID = 0;
                OrderRecord.VoucherDiscountRate = 0;
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// returns the order id of a logged in user if order exist in cart
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>

        public long GetCartOrderId(long contactId, long CompanyId)
        {

            return db.Estimates.Where(c => c.ContactId == contactId && c.CompanyId == CompanyId && c.StatusId == (int)OrderStatus.ShoppingCart).Select(i => i.EstimateId).FirstOrDefault();

        }
        /// <summary>
        /// update order detail for checkout page
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="loggedInContactID"></param>
        /// <param name="orderTotal"></param>
        /// <param name="voucherCode"></param>
        /// <param name="voucherDiscRate"></param>
        /// <param name="deliverCostCenterID"></param>
        /// <param name="deliveryEstimatedCompletionTime"></param>
        /// <param name="deliverCost"></param>
        /// <param name="isCorpFlow"></param>
        /// <returns></returns>
        /// UpdateOrderWithDetails(sOrderID, _myClaimHelper.loginContactID(), grandOrderTotal,deliveryCompletionTime, deliveryCost, UserCookieManager.StoreMode)
        public bool UpdateOrderWithDetails(long orderID, long loggedInContactID, double? orderTotal, int deliveryEstimatedCompletionTime,StoreMode isCorpFlow)
        {
            bool result = false;
            Estimate tblOrder = null;

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {

                try
                {

                    tblOrder = db.Estimates.Where(estm => estm.EstimateId == orderID).FirstOrDefault();

                    if (tblOrder != null)
                    {

                        tblOrder.Estimate_Total = orderTotal;

                       
                        tblOrder.DeliveryCompletionTime = deliveryEstimatedCompletionTime;
                        tblOrder.CreationDate = DateTime.Now;
                        UpdateNewOrderData(tblOrder, deliveryEstimatedCompletionTime, loggedInContactID); // sets end and start delivery data                    

                        if (db.SaveChanges() > 0)
                        {
                            result = true;
                            dbContextTransaction.Commit(); 
                        }
                    }
                }
                catch (Exception)
                {
                    result = false;
                    dbContextTransaction.Rollback();
                }
               
            }
         
            return result;
        }

        private void UpdateNewOrderData(Estimate tblOrder, int stardDilveryDasys, long? loggedInContactID)
        {
            tblOrder.Order_Date = DateTime.Now;

            if (stardDilveryDasys > 0)
            {
                DateTime StartDate =  AddBusinessdays(stardDilveryDasys, DateTime.Now);

                tblOrder.StartDeliveryDate = StartDate;

                tblOrder.FinishDeliveryDate = AddBusinessdays(2, StartDate);
            }
            else
            {
                DateTime StartDate = AddBusinessdays(1, DateTime.Now);
                tblOrder.StartDeliveryDate = StartDate;
                tblOrder.FinishDeliveryDate = AddBusinessdays(2, StartDate);
            }


            tblOrder.OrderManagerId = (int)loggedInContactID;
            tblOrder.Created_by = (int)loggedInContactID;

        }
        public DateTime AddBusinessdays(int ProductionDays, DateTime StartingDay)
        {
            var sign = ProductionDays < 0 ? -1 : 1;

            var unsignedDays = Math.Abs(ProductionDays);

            var weekdaysAdded = 0;

            DateTime Estimateddate = StartingDay;

            while (weekdaysAdded < unsignedDays)
            {
                Estimateddate = Estimateddate.AddDays(sign);

                if (Estimateddate.DayOfWeek != DayOfWeek.Saturday && Estimateddate.DayOfWeek != DayOfWeek.Sunday)

                    weekdaysAdded++;

            }
            return Estimateddate;
        }
        /// <summary>
        /// to check either order belongs to corporate or not base on order and customer id
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool IsOrderBelongToCorporate(long orderID, out long customerID)
        {
           bool result = false;
            try
            {
                
                customerID = 0;

                Estimate tblOrder = db.Estimates.Where(order => order.EstimateId == orderID).FirstOrDefault();
                if (tblOrder != null && tblOrder.Company.IsCustomer == (int)CustomerTypes.Corporate)
                {
                    customerID = tblOrder.Company.CompanyId;
                    result = true;
                }
          

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// Get order, items, addresses details by order id
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="BrokerID"></param>
        /// <returns></returns>
        public OrderDetail GetOrderReceipt(long orderID)
        {
            Estimate Order = null;
            //Model.ShoppingCart shopCart = null;
            OrderDetail userOrder = null;

            try
            {
                    Order = db.Estimates.Where(estm => estm.EstimateId == orderID).FirstOrDefault();
                    if (Order != null)
                    {
                        userOrder = new OrderDetail()
                        {

                            OrderID = Order.EstimateId,
                            OrderCode = Order.Order_Code,
                            ProductName = Order.Estimate_Name,
                            StatusID = Order.StatusId,
                            //StatusName = Order.tbl_Statuses.StatusName,
                            //StatusTypeID = Order.tbl_Statuses.StatusType,
                            ContactUserID = Order.ContactId ?? 0,
                            CustomerID = Order.CompanyId,
                            CustomerName = Order.Company.Name,
                            OrderDate = Order.Order_Date,
                            DeliveryDate = Order.StartDeliveryDate, //estimated Delivery date
                            DeliveryAddressID = Order.AddressId,
                            BillingAddressID = Order.BillingAddressId ?? 0,
                            DeliveryCostCentreID = Order.DeliveryCostCenterId ?? 0,
                            //InvoiceDate
                            YourRef = Order.CustomerPO,
                            SpecialInstNotes = Order.UserNotes,
                            PlacedBy = string.Format("{0} {1}", Order.CompanyContact == null ? "" : Order.CompanyContact.FirstName, Order.CompanyContact == null ? "" : Order.CompanyContact.LastName),
                            
                        };
                        //order details or shopping details
                        
                        userOrder.ItemDetail = this.ExtractShoppingCart(Order);
                        userOrder.BillingAdress = db.Addesses.Where(i => i.AddressId == Order.BillingAddressId).FirstOrDefault();
                        userOrder.ShippingAddress = db.Addesses.Where(i => i.AddressId == Order.AddressId).FirstOrDefault();
                        userOrder.DeliveryMethod = db.CostCentres.Where(c => c.CostCentreId == Order.DeliveryCostCenterId).Select(n => n.Name).FirstOrDefault();
                    }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userOrder;

        }



        public void updateTaxInCloneItemForServic(long orderId, double TaxValue, StoreMode Mode)
        {
           
                Estimate tblOrder = db.Estimates.Where(estm => estm.EstimateId == orderId).FirstOrDefault();
                if (tblOrder != null)
                {
                    List<Item> ClonedITem = db.Items.Where(i => i.EstimateId == tblOrder.EstimateId).ToList();

                    if (ClonedITem != null)
                    {
                        foreach (var item in ClonedITem)
                        {

                            //if (item.Qty1Tax1Value == null || item.Qty1Tax1Value == 0)
                            // {
                            if (Convert.ToInt32(item.ItemType) != Convert.ToInt32(ItemTypes.Delivery))
                            {

                                if (item.DefaultItemTax != null)
                                {
                                    item.Qty1GrossTotal = Math.Round(ServiceGrossTotalCalculation(item.Qty1NetTotal ?? 0, TaxValue), 2);
                                    item.Qty1Tax1Value = Math.Round(ServiceTotalTaxCalculation(item.Qty1NetTotal ?? 0, TaxValue), 2);
                                }
                                else
                                {

                                    item.Qty1GrossTotal = Math.Round(ServiceGrossTotalCalculation(item.Qty1NetTotal ?? 0, TaxValue), 2);
                                    item.Qty1Tax1Value = Math.Round(ServiceTotalTaxCalculation(item.Qty1NetTotal ?? 0, TaxValue), 2);

                                }

                                item.Tax1 = 0;
                            }
                            //  }
                            db.SaveChanges();
                        }
                    }
                } 
        }
        public static double ServiceTotalTaxCalculation(double QuantityBastotal, double Taxvalue)
        {
            double Quantity1Taxvalue = QuantityBastotal * Taxvalue;
            return Quantity1Taxvalue;

        }
        public static double ServiceGrossTotalCalculation(double QuantityBastotal, double Taxvalue)
        {
            double gross = QuantityBastotal + ServiceTotalTaxCalculation(QuantityBastotal, Taxvalue);
            return gross;
        }

        public bool UpdateOrderWithDetailsToConfirmOrder(long orderID, long loggedInContactID, OrderStatus orderStatus, Address billingAdd, Address deliveryAdd, double grandOrderTotal,
                                             string yourReferenceNumber, string specialInsTel, string specialInsNotes, bool isCorpFlow, StoreMode CurrntStoreMde, long BrokerContactCompanyID, Estimate order, Prefix prefix)
        {
            bool result = false;
            Estimate tblOrder = null;
            Company mdlCustomer = null;

            DbTransaction dbTrans = null;
            Organisation org = null;
            Company oBrokerCompany = null;


            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    Address billingAddress = billingAdd;
                Address deliveryAddress = deliveryAdd;

                short orderStatusID = (short)orderStatus;


                 tblOrder = db.Estimates.Where(estm => estm.EstimateId == orderID).FirstOrDefault();

                 if (tblOrder != null)
                {
                    
                      
                        // AddressManager.UpdateAddress(dbContext, billingAddress, deliveryAddress, tblOrder.ContactCompanyID);
                        if (billingAddress == null)// means they both are same
                            billingAddress = deliveryAddress;

                        //update order status
                        tblOrder.StatusId = orderStatusID;
                        UpdateNewOrderData(tblOrder, Convert.ToInt32(tblOrder.DeliveryCompletionTime), loggedInContactID); // sets end and start delivery data                    
                        tblOrder.UserNotes = specialInsNotes;
                        tblOrder.CustomerPO = yourReferenceNumber;
                        tblOrder.AddressId = (int)deliveryAddress.AddressId;
                        tblOrder.BillingAddressId = (int)billingAddress.AddressId;
                        tblOrder.Estimate_Total = grandOrderTotal; //OrderManager.GetOrderGrossTotalAndCreateDeliverySchedule(tblOrder);  
                        tblOrder.IsOfficialOrder = 1;
                        tblOrder.IsCreditApproved = 1;
                        tblOrder.CreationDate = DateTime.Now;
                        tblOrder.ContactId = loggedInContactID;

                        tblOrder.ClientStatus = Convert.ToInt16(ClientStatus.inProgress);

                        //if (prefix != null)
                        //{
                        //    tblOrder.Order_Code = prefix.OrderPrefix + "-001-" + prefix.OrderNext.ToString();
                        //    prefix.OrderNext = prefix.OrderNext + 1;
                        //}



                        // Order created date will be the date order actually placed
                        tblOrder.Order_Date = DateTime.Now;

                          List<long> MgrIds = new List<long>();

                        Company ObjComp = db.Companies.Where(c => c.CompanyId == tblOrder.CompanyId).FirstOrDefault();
                        if (ObjComp != null)
                        {
                            MgrIds.Add(ObjComp.StockNotificationManagerId1 ?? 0);
                            MgrIds.Add(ObjComp.StockNotificationManagerId2 ?? 0);
                            org = db.Organisations.Where(o => o.OrganisationId == ObjComp.OrganisationId).FirstOrDefault();
                        }

                        if (CurrntStoreMde == StoreMode.Retail)
                        {
                            //Update Customer Status as well if he is not a customer, he may be a prospect
                            mdlCustomer = new Company
                            {
                                CompanyId = tblOrder.CompanyId,
                                IsCustomer = 1,
                                TypeId = (int)CompanyTypes.SalesCustomer // from dummy to real customer 
                            };
                            //Update Customer
                            UpdateCustomer(mdlCustomer);
                        }


                        UpdateContactTelNo(loggedInContactID, specialInsTel);

                        //Update Item Status form shop cart to not progress
                        UpdateOrderedItems(orderStatus, tblOrder, ItemStatuses.NotProgressedToJob, CurrntStoreMde,org,MgrIds); // and Delete the items which are not of part

                        //Job Scheduling
                        //Update the order address id      
                        // we are commenting this function because delivery information will not handle by webstore decision changed on 17/12/2012
                        //OrderManager.CreateItemShippingJobSchedule(dbContext, tblOrder, deliveryAddress.AddressID);

                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        result = true;

                        //if (db.SaveChanges() > 0)
                        //{

                        //    result = true;
                          
                        //    dbContextTransaction.Commit();
                 
                        //}
                        //else
                        //{
                        //    dbContextTransaction.Rollback();
                        //    //throw new Exception("no changes made");
                        //}
                            


                 }

                }
                catch (Exception ex)
                {
                   dbContextTransaction.Rollback();
                   throw ex;
                }

            }

            return result;
        }
        private void UpdateOrderedItems(OrderStatus orderStatus, Estimate tblOrder, ItemStatuses itemStatus, StoreMode Mode, Organisation org, List<long> MgrIds)
        {
            
            tblOrder.Items.ToList().ForEach(item =>
            {
                if (item.IsOrderedItem.HasValue && item.IsOrderedItem.Value)
                {

                    if (orderStatus != OrderStatus.ShoppingCart)
                        item.StatusId = (short)itemStatus;

                    updateStockAndSendNotification(Convert.ToInt32(item.RefItemId), Mode, Convert.ToInt32(tblOrder.CompanyId), Convert.ToInt32(item.Qty1), Convert.ToInt32(tblOrder.ContactId), Convert.ToInt32(item.ItemId), Convert.ToInt32(tblOrder.EstimateId),MgrIds,org);

                }
                else
                {//Delete the non included items
                    bool result = false;
                    List<ArtWorkAttatchment> itemAttatchments = null;
                    Template clonedTempldateFiles = null;

                    result = RemoveCloneItem(item.ItemId, out itemAttatchments, out clonedTempldateFiles);
                    if (result)
                    {
                        
                       
                        RemoveItemAttacmentPhysically(itemAttatchments); // file removing physicslly
                        if (clonedTempldateFiles != null)
                             DeleteTemplateFiles(clonedTempldateFiles.ProductId,org.OrganisationId); // file removing
                    }

                    //dbContext.tbl_items.DeleteObject(item);
                }

            });
        }


        public static void RemoveItemAttacmentPhysically(List<ArtWorkAttatchment> attatchmentList)
        {
            string completePath = string.Empty;
            //@Server.MapPath(folderPath);
            try
            {
                if (attatchmentList != null)
                {
                    foreach (ArtWorkAttatchment itemAtt in attatchmentList)
                    {
                        completePath = HttpContext.Current.Server.MapPath(itemAtt.FolderPath + itemAtt.FileName);
                        if (itemAtt.UploadFileType == UploadFileTypes.Artwork)
                        {
                            //delete the thumb nails as well.
                            Utility.DeleteFile(completePath.Replace(itemAtt.FileExtention, "Thumb.png"));
                        }
                        Utility.DeleteFile(completePath); //
                    }
                }
                //System.Web

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool DeleteTemplateFiles(long ProductID, long OrganisationID)
        {
            try
            {

                bool result = false;

                var drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/" + ProductID.ToString());
                if (Directory.Exists(drURL))
                {
                    foreach (string item in System.IO.Directory.GetFiles(drURL))
                    {
                        System.IO.File.Delete(item);
                    }

                    Directory.Delete(drURL);
                }

                result = true;

                return result;

            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
        }
      

        public bool RemoveCloneItem(long itemID, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove)
        {
            try
            {
                
                    bool result = false;
                    clonedTemplateToRemove = null;
                    itemAttatchmetList = null;

                    Item tblItem = db.Items.Where(item => item.ItemId == itemID).FirstOrDefault();
                    if (tblItem != null)
                    {
                        if (RemoveCloneItem(tblItem, out itemAttatchmetList, out clonedTemplateToRemove))
                            result = db.SaveChanges() > 0 ? true : false;
                    }

                    return result;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool RemoveCloneItem(Item tblItem, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove)
        {
            bool result = false;
            List<ArtWorkAttatchment> itemAttatchments = null;
            Template clonedTemplate = null;

            try
            {
                if (tblItem != null)
                {
                 
                    itemAttatchments = new List<ArtWorkAttatchment>();

                    //Delete Attachments                       
                    tblItem.ItemAttachments.ToList().ForEach(att =>
                    {
                        db.ItemAttachments.Remove(att); // remove

                        //if (att.FileType == ".pdf")
                        itemAttatchments.Add(PopulateUploadedAttactchment(att)); // gathers attatments list as well.
                    });


                    //Remove the Templates if he has designed any
                    if (tblItem.TemplateId != null && tblItem.TemplateId > 0)
                    {
                        if (!ValidateIfTemplateIDIsAlreadyBooked(tblItem.ItemId, tblItem.TemplateId))
                            clonedTemplate = RemoveTemplates(tblItem.TemplateId);
                    }
                        
                    

                    //Section cost centeres
                    tblItem.ItemSections.ToList().ForEach(itemSection => itemSection.SectionCostcentres.ToList().ForEach(sectCost => db.SectionCostcentres.Remove(sectCost)));


                    //Item Section
                    tblItem.ItemSections.ToList().ForEach(itemsect => db.ItemSections.Remove(itemsect));



                    //Finally the item
                    db.Items.Remove(tblItem);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            itemAttatchmetList = itemAttatchments;
            clonedTemplateToRemove = clonedTemplate;
            return result;
        }
        public Template RemoveTemplates(long? templateID)
        {
            Template clonedTemplate = null;

            if (templateID.HasValue && templateID.Value > 0)
            {
                Template tblTemplate = db.Templates.Where(template => template.ProductId == templateID.Value).FirstOrDefault();

                if (tblTemplate != null)
                {
                    //color Style
                    tblTemplate.TemplateColorStyles.ToList().ForEach(tempColorStyle => db.TemplateColorStyles.Remove(tempColorStyle));

                    //backgourd
                    tblTemplate.TemplateBackgroundImages.ToList().ForEach(tempBGImages => db.TemplateBackgroundImages.Remove(tempBGImages));

                    //font
                    tblTemplate.TemplateFonts.ToList().ForEach(tempFonts => db.TemplateFonts.Remove(tempFonts));

                    //object
                    tblTemplate.TemplateObjects.ToList().ForEach(tempObj => db.TemplateObjects.Remove(tempObj));

                    //Page
                    tblTemplate.TemplatePages.ToList().ForEach(tempPage => db.TemplatePages.Remove(tempPage));


                    // the template to remove the files in web.ui
                    clonedTemplate = Clone<Template>(tblTemplate);

                    //finally template it self
                    db.Templates.Remove(tblTemplate);
                }
            }

            return clonedTemplate;

        }
        public static T Clone<T>(T source)
        {
            object item = Activator.CreateInstance(typeof(T));
            List<PropertyInfo> itemPropertyInfoCollection = source.GetType().GetProperties().ToList<PropertyInfo>();
            foreach (PropertyInfo propInfo in itemPropertyInfoCollection)
            {
                if (propInfo.CanRead && (propInfo.PropertyType.IsValueType || propInfo.PropertyType.FullName == "System.String"))
                {
                    PropertyInfo newProp = item.GetType().GetProperty(propInfo.Name);
                    if (newProp != null && newProp.CanWrite)
                    {
                        object va = propInfo.GetValue(source, null);
                        newProp.SetValue(item, va, null);
                    }
                }
            }

            return (T)item;
        }


        private bool ValidateIfTemplateIDIsAlreadyBooked(long itemID, long? templateID)
        {
            bool result = false;

            if (templateID.HasValue && templateID > 0)
            {
                int bookedCount = db.Items.Where(item => item.ItemId != itemID && item.TemplateId == templateID.Value).Count();
                if (bookedCount > 0)
                    result = true;
            }

            return result;
        }

        public ArtWorkAttatchment PopulateUploadedAttactchment(ItemAttachment attatchment)
        {

            UploadFileTypes resultUploadedFileType;

            ArtWorkAttatchment itemAttactchment = new ArtWorkAttatchment()
            {
                FileName = attatchment.FileName,
                FileTitle = attatchment.FileTitle,
                FileExtention = attatchment.FileType,
                FolderPath = attatchment.FolderPath,
                UploadFileType = Enum.TryParse(attatchment.Type, true, out resultUploadedFileType) ? resultUploadedFileType : UploadFileTypes.None
            };


            return itemAttactchment;
        }

        public void updateStockAndSendNotification(long itemID, StoreMode Mode, long companyId, int orderedQty, long contactId, long orderedItemid, long OrderId, List<long> MgrIds, Organisation org)
        {

            Item tblRefItemProduct = null;
            ItemStockControl tblItemStock = null;
           

            if (itemID > 0)
            {

                tblRefItemProduct = db.Items.Where(i => i.ItemId == itemID).FirstOrDefault();
                if (tblRefItemProduct.IsStockControl == true)
                {


                    //companySite = db.tbl_company_sites.FirstOrDefault();
                    tblItemStock = db.ItemStockControls.Where(i => i.ItemId == itemID).FirstOrDefault();
                    int currentStock = tblItemStock.InStock;
                    int lastModified = tblItemStock.InStock = tblItemStock.InStock - orderedQty;
                    if (tblItemStock.InStock < 0)
                    {
                        tblItemStock.InStock = 0;
                    }
                    ItemStockUpdateHistory stockLog = new ItemStockUpdateHistory();
                    stockLog.ItemId = (int)itemID;
                    stockLog.LastAvailableQty = currentStock;
                    stockLog.LastOrderedQty = orderedQty;
                    stockLog.LastModifiedQty = lastModified;
                    stockLog.LastModifiedDate = DateTime.Now;
                    stockLog.OrderID = (int)OrderId;
                    if (lastModified <= 0 && tblItemStock.isAllowBackOrder == true)
                    {
                        stockLog.ModifyEvent = Convert.ToInt32(StockLogEvents.BackOrder);
                    }
                    else if (lastModified <= tblItemStock.ThresholdLevel)
                    {
                        stockLog.ModifyEvent = Convert.ToInt32(StockLogEvents.ReachedThresholdLevel);
                    }
                    else
                    {
                        stockLog.ModifyEvent = Convert.ToInt32(StockLogEvents.Ordered);
                    }

                    db.ItemStockUpdateHistories.Add(stockLog);
                    db.SaveChanges();
                }


                if (tblItemStock != null)
                {
                    if (tblItemStock.InStock < tblItemStock.ThresholdLevel || tblItemStock.ThresholdLevel == null)
                    {
                        //EmailManager emailmgr = new EmailManager();
                        long ManagerID = 0;
                        
                      
                       

                        // send emails to the managers
                        if (tblItemStock.isAllowBackOrder == true)
                        {
                            if (Mode == StoreMode.Corp)
                            {
                                ManagerID = GetContactByRole(companyId, (int)Roles.Manager);
                                stockNotificationToManagers(MgrIds, companyId, org, StoreMode.Corp, ManagerID, itemID, (int)Events.BackOrder_Notifiaction_To_Manager, contactId, orderedItemid);
                               
                            }
                            else
                            {
                                stockNotificationToManagers(MgrIds, companyId, org, StoreMode.Retail, companyId, itemID, (int)Events.BackOrder_Notifiaction_To_Manager, contactId, orderedItemid);

                            }
                        }

                        if (Mode == StoreMode.Corp)
                        {
                            ManagerID = GetContactByRole(companyId, (int)Roles.Manager);
                            stockNotificationToManagers(MgrIds, companyId, org, StoreMode.Corp, ManagerID, itemID, (int)Events.ThresholdLevelReached_Notification_To_Manager, contactId, orderedItemid);
                        }

                        else
                        {
                            stockNotificationToManagers(MgrIds, companyId, org, StoreMode.Retail, companyId, itemID, (int)Events.ThresholdLevelReached_Notification_To_Manager, contactId, orderedItemid);

                        }
                    }
                }

            }
        }
        public bool UpdateCustomer(Company modelCustomer)
        {
            Company tblContactCompany = null;
            tblContactCompany = db.Companies.Where(customer => customer.CompanyId == modelCustomer.CompanyId).FirstOrDefault();

            return UpdateCustomer(tblContactCompany, modelCustomer);
        }

        public bool UpdateCustomer(Company tblContactCompany, Company modelCustomer)
        {

            if (tblContactCompany != null)
            {
                if (tblContactCompany.IsCustomer != (short)modelCustomer.IsCustomer)
                    tblContactCompany.IsCustomer = (short)modelCustomer.IsCustomer;

                if (tblContactCompany.TypeId != modelCustomer.TypeId && modelCustomer.TypeId > 0)
                    tblContactCompany.TypeId = modelCustomer.TypeId;

            }

            return true;
        }

        public long GetContactByRole(long CompanyID, int Role)
        {
          
                List<CompanyContact> ListOfAdmins = db.CompanyContacts.Where(i => i.CompanyId == CompanyID && i.ContactRoleId == Role).ToList();
                if (ListOfAdmins.Count > 0)
                {
                    return ListOfAdmins[0].ContactId;
                }
                else
                {
                    return 0;
                }
            
        }
        public bool UpdateContactTelNo(long contactId, string Mobile)
        {
            CompanyContact res = db.CompanyContacts.Where(i => i.ContactId == contactId).FirstOrDefault();
            if (res != null)
            {
                res.Mobile = Mobile;

                return true;

            }
            else
            {
                return false;
            }
        }

        public void stockNotificationToManagers(List<long> mangerList, long CompanyId, Organisation ServerSettings, StoreMode ModeOfStore, long salesId, long itemId, long emailevent, long contactId, long orderedItemid)
        {
            try
            {
               
                CampaignEmailParams obj = new CampaignEmailParams();
                List<SystemUser> listOfManagers = new List<SystemUser>();

               

                listOfManagers = (from c in db.SystemUsers
                                      where mangerList.Contains(c.SystemUserId)
                                      select c).ToList();
                    if (listOfManagers.Count() > 0)
                    {
                        Campaign stockCampaign = GetCampaignRecordByEmailEvent(emailevent);
                      
                        foreach (SystemUser stRec in listOfManagers)
                        {
                            obj.SystemUserID = stRec.SystemUserId;
                            obj.SalesManagerContactID = salesId;
                            obj.StoreID = CompanyId;
                            obj.CompanyId = CompanyId;
                            obj.CompanySiteID = 1;
                            obj.ItemID = (int)itemId;
                            obj.ContactId = contactId;
                            obj.orderedItemID = (int)orderedItemid;
                            //emailBodyGenerator(stockCampaign, SeverSettings, obj, null, ModeOfStore, "", "", "", stRec.Email, stRec.FullName);
                        }
                    }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Campaign GetCampaignRecordByEmailEvent(long iEmailEvent)
        {
            try
            {
                    var email = (from c in db.Campaigns
                                 where c.EmailEvent == iEmailEvent
                                 select c).FirstOrDefault();
                    return email;
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public double UpdateORderGrandTotal(long OrderID)
        {

            double Delevery = 0;
            double DeliveryTaxValue = 0;
            double TotalVat = 0;
            double Subtotal = 0;
            double calculate = 0;
            double GrandTotal = 0;

                List<Item> items = db.Items.Where(i => i.EstimateId == OrderID).ToList();

                foreach (var item in items)
                {

                    if (item.ItemType == (int)ItemTypes.Delivery)
                    {
                        Delevery = Convert.ToDouble(item.Qty1NetTotal);
                        DeliveryTaxValue = Convert.ToDouble(item.Qty1GrossTotal - item.Qty1NetTotal);

                    }
                    else
                    {

                        Subtotal = Subtotal + Convert.ToDouble(item.Qty1NetTotal);
                        TotalVat = Convert.ToDouble(item.Qty1GrossTotal) - Convert.ToDouble(item.Qty1NetTotal);
                        calculate = calculate + TotalVat;
                    }

                }

                GrandTotal = Subtotal + calculate + DeliveryTaxValue + Delevery;

          
            return GrandTotal;
        }

        public bool SaveDilveryCostCenter(long orderId, CostCentre ChangedCostCenter)
        {
            Estimate tblOrder = null;
            try
            {
                
                    tblOrder = db.Estimates.Where(estm => estm.EstimateId == orderId).FirstOrDefault();
                    if (tblOrder != null)
                    {
                        tblOrder.DeliveryCostCenterId = (int)ChangedCostCenter.CostCentreId;
                        tblOrder.DeliveryCost = ChangedCostCenter.SetupCost;
                    }
                    if (db.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
   

    }
}
