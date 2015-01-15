using Microsoft.Practices.Unity;
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
            List<Estimate> ordesList = db.Estimates.Include("tbl_items").Where(order => order.ContactId == contactID && order.StatusId == orderStatusID && order.isEstimate == false).Take(1).ToList();
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
            ShoppingCart shopCart = null;
            short orderStsID = Convert.ToInt16(orderStatus);

            try
            {
                int Orderid = Convert.ToInt32(orderID);

                tblOrder = db.Estimates.Where(estm => estm.EstimateId == Orderid && estm.StatusId == orderStsID).FirstOrDefault();
                if (tblOrder != null)
                {
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
                Item DeliveryItemOfOrder = GetDeliveryOrderItem(Convert.ToInt32(tblEstimate.EstimateId));
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

            return itemAddOnsList;
        }

        public List<Address> GetOrderCompanyAllAddresses(Estimate tblOrder)
        {
            List<Address> companyAddresesList = null;
            List<Address> tblContactCompanyAddList = null;

            Address modeAddress = null;

            if (tblOrder != null)
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


            return companyAddresesList;
        }


        public Item GetDeliveryOrderItem(int OrderId)
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
       
    }
}
