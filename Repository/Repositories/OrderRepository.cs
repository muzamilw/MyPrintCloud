using System.Linq.Expressions;
using GrapeCity.ActiveReports.PageReportModel;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using WebSupergoo.ABCpdf8;
using System.Data.Objects;
using System.Text;
using Ionic.Zip;
using GrapeCity.ActiveReports;
using System.Xml;
using MPC.Common;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Image = System.Drawing.Image;
using System.Globalization;

namespace MPC.Repository.Repositories
{
    public class OrderRepository : BaseRepository<Estimate>, IOrderRepository
    {

        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IPrefixRepository _prefixrepository;
        private readonly IItemRepository _ItemRepository;
        private readonly IPrefixService _PrefixService;
        //  public OrderRepository(IUnityContainer container, IWebstoreClaimsHelperService myClaimHelper, IPrefixService _PrefixService)
        private readonly IItemAttachmentRepository _ItemAttachmentRepository;
        private readonly IOrganisationRepository _Organisationrepository;
        private readonly ITemplateRepository _TemplateRepository;
        private readonly ITemplatePageRepository _TemplatePageRepository;
        private readonly ICampaignRepository _campaignRepository;
        public OrderRepository(IUnityContainer container, IWebstoreClaimsHelperService myClaimHelper, IPrefixRepository _prefixrepository, 
            IItemRepository _ItemRepository, IItemAttachmentRepository _ItemAttachmentRepository,
            IOrganisationRepository _Organisationrepository, IPrefixService _PrefixService,
            ITemplateRepository _TemplateRepository, ITemplatePageRepository _TemplatePageRepository
            , ICampaignRepository campaignRepository)
            : base(container)
        {
            this._myClaimHelper = myClaimHelper;
            this._prefixrepository = _prefixrepository;
            this._PrefixService = _PrefixService;
            this._ItemRepository = _ItemRepository;
            this._ItemAttachmentRepository = _ItemAttachmentRepository;
            this._Organisationrepository = _Organisationrepository;
            this._TemplateRepository = _TemplateRepository;
            this._TemplatePageRepository = _TemplatePageRepository;
            this._campaignRepository = campaignRepository;
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

        public int GetFirstItemIDByOrderId(long orderId)
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
        public List<Item> GetOrderItems(long OrderId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return (from r in db.Items.Include("ItemSections.SectionCostcentres")
                        where r.EstimateId == OrderId && r.IsOrderedItem == true && (r.ItemType == null || r.ItemType != (int)ItemTypes.Delivery)
                        select r).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long CreateNewOrder(long CompanyId, long ContactId, long OrganisationId, string orderTitle = null)
        {
            try
            {
               // Prefix prefix = db.Prefixes.Where(c => c.OrganisationId == OrganisationId).FirstOrDefault();

                Estimate orderObject = new Estimate();

                orderObject.CompanyId = (int)CompanyId;

                orderObject.OrganisationId = OrganisationId;

                orderObject.ContactId = ContactId;

                orderObject.isEstimate = false;

                orderObject.StatusId = (short)OrderStatus.ShoppingCart;

                orderObject.SectionFlagId = db.SectionFlags.Where(s => s.OrganisationId == OrganisationId && s.SectionId == (int)OrderSectionFlag.UrgentOrder).FirstOrDefault().SectionFlagId;

                orderObject.Estimate_Name = string.IsNullOrWhiteSpace(orderTitle) ? "WebStore New Order" : orderTitle;

                orderObject.isDirectSale = false;

                orderObject.Order_Date = DateTime.Now;

                orderObject.CreationDate = DateTime.Now;

                orderObject.CreationTime = DateTime.Now;

                orderObject.Order_CreationDateTime = DateTime.Now;

                //if (prefix != null)
                //{
                //    orderObject.Order_Code = prefix.OrderPrefix + "-" + prefix.OrderNext.ToString();
                //    prefix.OrderNext = prefix.OrderNext + 1;
                //}

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long GetOrderID(long CustomerId, long ContactId, string orderTitle, long OrganisationId)
        {
            try
            {
                long orderID = 0;

                orderID = GetOrderByContactID(ContactId, OrderStatus.ShoppingCart, CustomerId);

                if (orderID == 0)
                {
                    orderID = CreateNewOrder(CustomerId, ContactId, OrganisationId, orderTitle);
                }

                return orderID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private long GetOrderByContactID(long contactID, OrderStatus orderStatus, long CompanyId)
        {
            try
            {
                int orderStatusID = (int)orderStatus;
                List<Estimate> ordesList = db.Estimates.Include("Items").Where(order => order.ContactId == contactID && order.CompanyId == CompanyId && order.StatusId == orderStatusID && order.isEstimate == false).Take(1).ToList();
                if (ordesList.Count > 0)
                    return ordesList[0].EstimateId;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        /// <summary>
        /// Delete Order
        /// </summary>
        public void DeleteOrder(long orderId)
        {
            db.usp_DeleteOrderByID(orderId);
        }
        public bool IsUserLoggedIn()
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
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

                tblOrder = db.Estimates.Where(estm => estm.EstimateId == Orderid && estm.StatusId == (int)OrderStatus.ShoppingCart).FirstOrDefault();
                if (tblOrder != null)
                {

                    shopCart = ExtractShoppingCart(tblOrder);
                    if (tblOrder.BillingAddressId != null)
                        shopCart.BillingAddressID = (long)tblOrder.BillingAddressId;
                    else
                        shopCart.BillingAddressID = 0;
                    shopCart.ShippingAddressID = tblOrder.AddressId;

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
            int DeliveryTime = 0;
            ShoppingCart shopCart = new ShoppingCart();
            List<AddOnCostsCenter> childrenRecordsAllProductItemAddons = null;
            List<Item> ItemsOfOrder = GetOrderItems(Convert.ToInt32(tblEstimate.EstimateId));
            try
            {

                //1. Get All Items and Its Attament in a Singe Instant
                shopCart.CartItemsList = this.ExtractItemsAndAttatchments(ItemsOfOrder, Convert.ToInt64(tblEstimate.OrganisationId), out childrenRecordsAllProductItemAddons);

                //2. Get All Addons Used in that Items
                shopCart.ItemsSelectedAddonsList = childrenRecordsAllProductItemAddons;

                //3. Extract company address if any
                shopCart.AddressesList = this.GetOrderCompanyAllAddresses(tblEstimate); //this.GetOrderCompanyBillingShipingAddresses(tblEstimate);


                //4. Set Order Level Fields
                shopCart.DiscountVoucherID = (tblEstimate.DiscountVoucherID.HasValue && tblEstimate.DiscountVoucherID.Value > 0) ? tblEstimate.DiscountVoucherID.Value : 0;
                shopCart.VoucherDiscountRate = (tblEstimate.VoucherDiscountRate.HasValue && tblEstimate.VoucherDiscountRate.Value > 0) ? tblEstimate.VoucherDiscountRate.Value : 0;
                shopCart.DeliveryCostCenterID = (tblEstimate.DeliveryCostCenterId.HasValue && tblEstimate.DeliveryCostCenterId.Value > 0) ? tblEstimate.DeliveryCostCenterId.Value : 0;

                // shopCart.DeliveryCost = (tblEstimate.DeliveryCost.HasValue && tblEstimate.DeliveryCost.Value > 0) ? tblEstimate.DeliveryCost.Value : 0;
                //5. get delivery item 
                Item DeliveryItemOfOrder = GetDeliveryOrderItem(tblEstimate.EstimateId);
                if (DeliveryItemOfOrder != null)
                {
                    shopCart.DeliveryTaxValue = DeliveryItemOfOrder.Qty1Tax1Value ?? 0;
                    shopCart.DeliveryCost = DeliveryItemOfOrder.Qty1NetTotal ?? 0;
                    shopCart.DeliveryDiscountVoucherID = DeliveryItemOfOrder.DiscountVoucherID;
                }
                foreach (ProductItem itm in shopCart.CartItemsList)
                {
                    if (itm.EstimateProductionTime != null && itm.EstimateProductionTime > 0)
                    {
                        DeliveryTime += Convert.ToInt32(itm.EstimateProductionTime);
                    }
                }
                foreach (AddOnCostsCenter cc in shopCart.ItemsSelectedAddonsList)
                {
                    if (cc.EstimateProductionTime != null && cc.EstimateProductionTime > 0)
                    {
                        DeliveryTime += Convert.ToInt32(cc.EstimateProductionTime);
                    }
                }
                shopCart.TotalProductionTime = DeliveryTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return shopCart;
        }

        public ShoppingCart ExtractShoppingCartForOrder(Estimate tblEstimate)
        {
            ShoppingCart shopCart = new ShoppingCart();
            List<AddOnCostsCenter> childrenRecordsAllProductItemAddons = null;
            List<Item> ItemsOfOrder = GetOrderItems(Convert.ToInt32(tblEstimate.EstimateId));
            try
            {
                //1. Get All Items and Its Attament in a Singe Instant
                shopCart.CartItemsList = this.ExtractItemsAndAttatchments(ItemsOfOrder, Convert.ToInt64(tblEstimate.OrganisationId), out childrenRecordsAllProductItemAddons);

                //3. Extract company address if any
                shopCart.AddressesList = this.GetOrderCompanyAllAddresses(tblEstimate); //this.GetOrderCompanyBillingShipingAddresses(tblEstimate);

                shopCart.ItemsSelectedAddonsList = childrenRecordsAllProductItemAddons;

                //4. Set Order Level Fields
                shopCart.DiscountVoucherID = (tblEstimate.DiscountVoucherID.HasValue && tblEstimate.DiscountVoucherID.Value > 0) ? tblEstimate.DiscountVoucherID.Value : 0;
                shopCart.VoucherDiscountRate = (tblEstimate.VoucherDiscountRate.HasValue && tblEstimate.VoucherDiscountRate.Value > 0) ? tblEstimate.VoucherDiscountRate.Value : 0;
                shopCart.DeliveryCostCenterID = (tblEstimate.DeliveryCostCenterId.HasValue && tblEstimate.DeliveryCostCenterId.Value > 0) ? tblEstimate.DeliveryCostCenterId.Value : 0;
                // shopCart.DeliveryCost = (tblEstimate.DeliveryCost.HasValue && tblEstimate.DeliveryCost.Value > 0) ? tblEstimate.DeliveryCost.Value : 0;
                //5. get delivery item 
                Item DeliveryItemOfOrder = GetDeliveryOrderItem(tblEstimate.EstimateId);
                if (DeliveryItemOfOrder != null)
                {
                    shopCart.DeliveryTaxValue = DeliveryItemOfOrder.Qty1Tax1Value ?? 0;
                    shopCart.DeliveryCost = DeliveryItemOfOrder.Qty1NetTotal ?? 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return shopCart;
        }


        private List<ProductItem> ExtractItemsAndAttatchments(List<Item> orderItemsList, long OrganisationId, out  List<AddOnCostsCenter> childrenRecordsAllProductItemAddons)
        {
            List<ProductItem> productItemsList = new List<ProductItem>();
            List<AddOnCostsCenter> allItemsAddOnsList = new List<AddOnCostsCenter>();

            long? StockOptionID = 0;
            string StockName = "";
            ProductItem prodItem = null;

            orderItemsList.ForEach
                (
                    item =>
                    {

                        if (item.IsOrderedItem.HasValue && item.IsOrderedItem.Value) //gets only attatchments which are added to cart
                        {
                            var Section = db.ItemSections.Where(i => i.ItemId == item.ItemId & i.SectionNo == 1).FirstOrDefault();

                            if (Section != null)
                                StockOptionID = Section.StockItemID2;


                            if (StockOptionID > 0)
                            {
                                ItemStockOption stockOption = db.ItemStockOptions.Where(i => i.ItemStockOptionId == StockOptionID && i.ItemId == item.RefItemId).FirstOrDefault();
                                if(stockOption != null)
                                {
                                     StockName = stockOption.StockLabel;
                                }
                            }
                            
                            prodItem = CreateProductItem(item, StockName);
                            prodItem.Attatchment = this.ExtractAttachment(item);
                            if (item.ItemAttachments != null)
                            {
                                prodItem.OtherItemAttatchments = item.ItemAttachments.Where(attatchment => attatchment.ItemId == item.ItemId && string.Compare(attatchment.Type, UploadFileTypes.Artwork.ToString(), true) == 0).ToList();
                            }
                            else 
                            {
                                prodItem.OtherItemAttatchments = null;
                            }

                            if (item.ProductType == (int)ProductType.PrintProduct)
                            {
                                prodItem.OtherItemTemplateAttatchments = GetTemplatePagesWithURL(Convert.ToInt64(item.TemplateId), OrganisationId);
                            }
                            else 
                            {
                                prodItem.OtherItemTemplateAttatchments = null;
                            }
                            productItemsList.Add(prodItem);
                            allItemsAddOnsList.AddRange(this.ExtractAdditionalAddons(item)); //Collects the addons for each item

                        }
                    }
                );

            childrenRecordsAllProductItemAddons = allItemsAddOnsList;
            return productItemsList;
        }

        private List<ItemTemplatePage> GetTemplatePagesWithURL(long TemplateId, long OrganisationId) 
        {
            if (TemplateId > 0)
            {
                var query = from tmp in db.TemplatePages
                            where tmp.ProductId == TemplateId
                            select new ItemTemplatePage()
                            {
                                FilePath = "/mpc_content/Designer/Organisation" + OrganisationId + "/Templates/" + TemplateId + "/",
                                PageId = tmp.ProductPageId
                            };
                List<ItemTemplatePage> temPages = query.ToList<ItemTemplatePage>();
                int count = 1;
                foreach (ItemTemplatePage itm in temPages)
                {
                    itm.FilePath = itm.FilePath + "p" + count + ".jpg";
                    count++;
                }
                return temPages;
            }
            else 
            {
                return null;
            }
        }

        public ProductItem CreateProductItem(Item tblItem, string PaperName)
        {
            short StatusID = 0;
            int invoiceID = 0;
            if (tblItem.Status != null)
                StatusID = tblItem.Status.StatusId;
            if (tblItem.InvoiceId != null)
                invoiceID = (int)tblItem.InvoiceId;

            long productCategoryID = db.ProductCategoryItems.Where(i => i.ItemId == tblItem.RefItemId).Select(s => s.CategoryId ?? 0).FirstOrDefault();

            string CategoryName = db.ProductCategories.Where(p => p.ProductCategoryId == productCategoryID).Select(s => s.CategoryName).FirstOrDefault();
            ProductItem prodItem = new ProductItem()
            {

                ItemID = (int)tblItem.ItemId,
                Status = StatusID,
                EstimateID = tblItem.EstimateId,
                InvoiceID = invoiceID,
                ProductName = tblItem.ProductName,
                ProductFriendlyName = tblItem.Title,
                ProductCategoryName = CategoryName, //Product category Name
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
                DiscountedAmount = tblItem.Qty1CostCentreProfit,
                DiscountedVoucherId = tblItem.DiscountVoucherID,
            };
            return prodItem;
        }

        private ArtWorkAttatchment ExtractAttachment(Item tblItem)
        {
            ArtWorkAttatchment artWorkAttatchment = null;
            ItemAttachment tblItemAttchment = null;



            if (tblItem.ItemAttachments != null && tblItem.ItemAttachments.Count > 0)
            {

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
                    artWorkAttatchment.ImageFileType = "Thumb" + tblItemAttchment.ImageFileType;
                    artWorkAttatchment.UploadFileType = (UploadFileTypes)Enum.Parse(typeof(UploadFileTypes), tblItemAttchment.Type); //Model.UploadFileTypes.Artwork.ToString();
                }

            }
            else
            {
                List<ItemAttachment> newlistAttach = db.ItemAttachments.Where(attatchment => attatchment.ItemId == tblItem.ItemId && string.Compare(attatchment.Type, UploadFileTypes.Artwork.ToString(), true) == 0).ToList();
                if (newlistAttach != null && newlistAttach.Count > 0)
                {
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
                        //artWorkAttatchment.ImageFileType = "Thumb" + tblItemAttchment.ImageFileType;
                        artWorkAttatchment.UploadFileType = (UploadFileTypes)Enum.Parse(typeof(UploadFileTypes), tblItemAttchment.Type); //Model.UploadFileTypes.Artwork.ToString();
                    }
                }

            }

            artWorkAttatchment = artWorkAttatchment ?? new ArtWorkAttatchment();

            return artWorkAttatchment;

        }

        private List<AddOnCostsCenter> ExtractAdditionalAddons(Item tblItem)
        {
            List<AddOnCostsCenter> itemAddOnsList = new List<AddOnCostsCenter>();
            ItemSection tblItemFirstSection = null;
            List<SectionCostcentre> tblSectionCostList = null;

            //FirstSection
            if (tblItem.ItemSections != null && tblItem.ItemSections.Count > 0)
            {
                tblItemFirstSection = tblItem.ItemSections.Where(itmSect => itmSect.SectionNo.HasValue && itmSect.SectionNo.Value == 1).FirstOrDefault();
                if (tblItemFirstSection != null)
                {
                    if (tblItemFirstSection.SectionCostcentres != null && tblItemFirstSection.SectionCostcentres.Count > 0)
                    {
                        tblSectionCostList = tblItemFirstSection.SectionCostcentres.Where(sectCostCenter => sectCostCenter.IsOptionalExtra == 1).ToList();

                        tblSectionCostList.ForEach(
                            sectCostCenter =>
                            {
                                AddOnCostsCenter addonCostCenter = new AddOnCostsCenter
                                {
                                    AddOnName = !string.IsNullOrEmpty(sectCostCenter.Name) ? sectCostCenter.Name : sectCostCenter.CostCentreId != null && sectCostCenter.CostCentreId > 0 ? db.CostCentres.Where(c => c.CostCentreId == sectCostCenter.CostCentreId).FirstOrDefault().Name : "",
                                    CostCenterID = (int)sectCostCenter.CostCentreId,
                                    CostCentreDescription = !string.IsNullOrEmpty(sectCostCenter.Qty1WorkInstructions) ? sectCostCenter.Qty1WorkInstructions : "",
                                    ItemID = (int)tblItemFirstSection.ItemId,
                                    EstimateProductionTime = sectCostCenter.CostCentre != null ? sectCostCenter.CostCentre.EstimateProductionTime ?? 0 : 0
                                };

                                itemAddOnsList.Add(addonCostCenter); // cost center of particular item
                            });
                    }

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
                    if (tblOrder.Company.Addresses != null)
                    {
                        tblContactCompanyAddList = tblOrder.Company.Addresses.ToList();
                    }


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
        public bool SetOrderCreationDateAndCode(long orderId, long OrganisationId)
        {

            Estimate tblOrd = db.Estimates.Where(estm => estm.EstimateId == orderId).FirstOrDefault();
            Prefix prefix = _PrefixService.GetDefaultPrefix(OrganisationId);

            if (prefix != null)
            {
                tblOrd.Order_Code = prefix.OrderPrefix + "-" + prefix.OrderNext.ToString();
                prefix.OrderNext = prefix.OrderNext + 1;
            }
            tblOrd.CreationDate = DateTime.Now;
            tblOrd.IsCreditApproved = 1;
            tblOrd.IsOfficialOrder = 1;
            db.SaveChanges();
            return true;

        }

        //public bool IsVoucherValid(string voucherCode)
        //{

        //    bool result = true;
        //    DiscountVoucher discountVocher = null;
        //    try
        //    {
        //        discountVocher = db.DiscountVouchers.Where(discVoucher => discVoucher.VoucherCode == voucherCode && discVoucher.IsEnabled && discVoucher.CompanyId == null).FirstOrDefault();
        //        if (discountVocher != null)
        //        {

        //            if (discountVocher.ValidFromDate.HasValue && DateTime.Now < discountVocher.ValidFromDate.Value)
        //                result = false;

        //            else if (discountVocher.ValidUptoDate.HasValue && DateTime.Now > discountVocher.ValidUptoDate.Value)
        //                result = false;

        //            //else if (discountVocher.OrderID.HasValue)
        //            //    result = false;
        //        }
        //        else
        //        {
        //            result = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}
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
        //public bool RollBackDiscountedItems(int orderId, double StateTax, StoreMode Mode)
        //{

        //    double QtyNewTotal = 0;
        //    double QtyTaxVal = 0;

        //    List<Item> tblOrder = db.Items.Where(c => c.EstimateId == orderId && c.Qty1CostCentreProfit != null).ToList();
        //    if (tblOrder != null)
        //    {
        //        foreach (var item in tblOrder.Where(i => i.ItemType != Convert.ToInt32(ItemTypes.Delivery)))
        //        {
        //            SectionCostcentre SC = item.ItemSections.FirstOrDefault().SectionCostcentres.Where(c => c.CostCentreId == (int)CostCentresForWeb.WebOrderCostCentre).FirstOrDefault();

        //            QtyNewTotal = (double)item.Qty1NetTotal + (double)item.Qty1CostCentreProfit;
        //            QtyTaxVal = (QtyNewTotal * StateTax) / 100;
        //            item.Qty1NetTotal = QtyNewTotal;
        //            item.Qty1BaseCharge1 = QtyNewTotal;
        //            item.Qty1Tax1Value = QtyTaxVal;
        //            item.Qty1GrossTotal = QtyNewTotal + QtyTaxVal;
        //            item.ItemSections.FirstOrDefault().BaseCharge1 += (double)item.Qty1CostCentreProfit;
        //            if (SC != null)
        //            {
        //                SC.Qty1NetTotal += (double)item.Qty1CostCentreProfit;
        //                // SC.Qty1MarkUpValue = 0;
        //            }
        //            item.Qty1CostCentreProfit = 0;
        //        }
        //        if (db.SaveChanges() > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public double SaveVoucherCodeAndRate(int orderId, string VCode)
        //{
        //    try
        //    {

        //        Estimate record = db.Estimates.Where(c => c.EstimateId == orderId).FirstOrDefault();
        //        DiscountVoucher discountVocher = db.DiscountVouchers.Where(discVoucher => discVoucher.VoucherCode == VCode && discVoucher.IsEnabled).FirstOrDefault();
        //        if (record != null)
        //        {
        //            record.DiscountVoucherID = Convert.ToInt16(discountVocher.DiscountVoucherId);
        //            record.VoucherDiscountRate = discountVocher.DiscountRate;
        //        }
        //        if (db.SaveChanges() > 0)
        //        {
        //            return discountVocher.DiscountRate;
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        return 0;
        //        throw e;
        //    }
        //}

        //public double PerformVoucherdiscountOnEachItem(int orderId, OrderStatus orderStatus, double StateTax, double VDiscountRate, StoreMode Mode)
        //{
        //    short status = (short)orderStatus;
        //    double DiscountedAmount = 0;
        //    double TotalDiscAmount = 0;
        //    double TotalDiscAmountBroker = 0;
        //    double QtyNewTotal = 0;
        //    double QtyTaxVal = 0;


        //    List<Item> tblOrder = db.Items.Where(c => c.EstimateId == orderId && (c.ItemType == null || c.ItemType != 2) && (c.Qty1CostCentreProfit == null || c.Qty1CostCentreProfit == 0)).ToList();
        //    if (tblOrder != null)
        //    {
        //        foreach (var item in tblOrder)
        //        {
        //            SectionCostcentre SC = item.ItemSections.FirstOrDefault().SectionCostcentres.Where(c => c.CostCentreId == (int)CostCentresForWeb.WebOrderCostCentre).FirstOrDefault();

        //            DiscountedAmount = CalCulateVoucherDiscount(Convert.ToDouble(item.Qty1BaseCharge1), VDiscountRate);
        //            item.Qty1CostCentreProfit = DiscountedAmount;
        //            TotalDiscAmount += DiscountedAmount;
        //            QtyNewTotal = item.Qty1NetTotal - DiscountedAmount ?? 0;
        //            QtyTaxVal = (QtyNewTotal * StateTax) / 100;
        //            item.Qty1NetTotal = QtyNewTotal;
        //            item.Qty1BaseCharge1 = QtyNewTotal;
        //            item.Qty1Tax1Value = QtyTaxVal;
        //            item.Qty1GrossTotal = QtyNewTotal + QtyTaxVal;
        //            item.ItemSections.FirstOrDefault().BaseCharge1 -= DiscountedAmount;
        //            if (SC != null)
        //            {
        //                SC.Qty1NetTotal -= DiscountedAmount;
        //                //SC.Qty1MarkUpValue = -DiscountedAmount;
        //            }
        //        }
        //        if (db.SaveChanges() > 0)
        //        {

        //            return TotalDiscAmount;

        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    else
        //    {
        //        return 0;
        //    }

        //}
        //private double CalCulateVoucherDiscount(double subTotal, double VoucherRate)
        //{
        //    double discRate = VoucherRate;
        //    double discountedAmount = 0;

        //    if (discRate > 0)
        //    {
        //        discountedAmount = CalculatePercentage(subTotal, discRate);
        //    }
        //    return discountedAmount;
        //}

        public static double CalculatePercentage(double itemValue, double percentageValue)
        {
            double percentValue = 0;

            percentValue = itemValue * (percentageValue / 100);

            return percentValue;
        }
        //public bool ResetOrderVoucherCode(int orderId)
        //{

        //    Estimate OrderRecord = db.Estimates.Where(c => c.EstimateId == orderId).FirstOrDefault();
        //    if (OrderRecord != null)
        //    {
        //        OrderRecord.DiscountVoucherID = 0;
        //        OrderRecord.VoucherDiscountRate = 0;
        //        if (db.SaveChanges() > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
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
        public bool UpdateOrderWithDetails(long orderID, long loggedInContactID, double? orderTotal, int deliveryEstimatedCompletionTime, StoreMode isCorpFlow,CompanyContact Contact)
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

                        // if null then not update address detail
                        if (Contact != null)
                        {
                            tblOrder.AddressId =Convert.ToInt32(Contact.AddressId);
                            tblOrder.BillingAddressId =Convert.ToInt32(Contact.ShippingAddressId);
                        }

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
                DateTime StartDate = AddBusinessdays(stardDilveryDasys, DateTime.Now);

                tblOrder.StartDeliveryDate = StartDate;

                tblOrder.FinishDeliveryDate = AddBusinessdays(2, StartDate);
            }
            else
            {
                DateTime StartDate = AddBusinessdays(1, DateTime.Now);
                tblOrder.StartDeliveryDate = StartDate;
                tblOrder.FinishDeliveryDate = StartDate;//AddBusinessdays(2, StartDate);
            }


            //tblOrder.Created_by = (int)loggedInContactID;

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
            catch (Exception ex)
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
                db.Configuration.LazyLoadingEnabled = false;
                Order = db.Estimates.Where(estm => estm.EstimateId == orderID).FirstOrDefault();
                if (Order != null)
                {
                    userOrder = new OrderDetail()
                    {

                        OrderID = Order.EstimateId,
                        OrderCode = Order.Order_Code,
                        ProductName = Order.Estimate_Name,
                        StatusID = Order.StatusId,
                        ContactUserID = Order.ContactId ?? 0,
                        CustomerID = Order.CompanyId,
                        CustomerName = "",
                        OrderDate = Order.Order_Date,
                        DeliveryDate = Order.StartDeliveryDate,
                        DeliveryAddressID = Order.AddressId,
                        BillingAddressID = Order.BillingAddressId ?? 0,
                        DeliveryCostCentreID = Order.DeliveryCostCenterId ?? 0,
                        //InvoiceDate
                        YourRef = Order.CustomerPO,
                        SpecialInstNotes = Order.UserNotes,
                        
                    };
                    //order details or shopping details
                    ShoppingCart shopCart = this.ExtractShoppingCart(Order);
                    if (shopCart != null)
                    {
                       
                        userOrder.ProductsList = shopCart.CartItemsList;
                        userOrder.DeliveryCost = shopCart.DeliveryCost;
                        userOrder.DeliveryCostTaxValue = shopCart.DeliveryTaxValue;
                        userOrder.ItemsSelectedAddonsList = shopCart.ItemsSelectedAddonsList;
                        userOrder.DeliveryDiscountVoucherId = shopCart.DeliveryDiscountVoucherID;
                    }

                    userOrder.BillingAdress = db.Addesses.Include("State").Include("Country").Where(i => i.AddressId == Order.BillingAddressId).FirstOrDefault();
                    userOrder.ShippingAddress = db.Addesses.Include("State").Include("Country").Where(i => i.AddressId == Order.AddressId).FirstOrDefault();
                    if (Order.DeliveryCostCenterId != null)
                    {
                        userOrder.DeliveryMethod = db.CostCentres.Where(c => c.CostCentreId == Order.DeliveryCostCenterId).Select(n => n.Name).FirstOrDefault();
                    }
                    if (Order.ContactId != null)
                    {
                        userOrder.CompanyContact = db.CompanyContacts.Where(c => c.ContactId == (long)Order.ContactId).FirstOrDefault();
                        if (userOrder.CompanyContact != null)
                        {
                            userOrder.PlacedBy = userOrder.CompanyContact.FirstName + " " + userOrder.CompanyContact.LastName;
                        }
                    }
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
                                             string yourReferenceNumber, string specialInsTel, string specialInsNotes, bool isCorpFlow, StoreMode CurrntStoreMde, Estimate order, Prefix prefix)
        {
            bool result = false;
            Estimate tblOrder = null;
            Company mdlCustomer = null;

            Organisation org = null;



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
                            //MgrIds.Add(ObjComp.StockNotificationManagerId1 ?? 0);
                            //MgrIds.Add(ObjComp.StockNotificationManagerId2 ?? 0);
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
                        //UpdateOrderedItems(orderStatus, tblOrder, ItemStatuses.NotProgressedToJob, CurrntStoreMde, org, MgrIds); // and Delete the items which are not of part

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

        public bool UpdateOrderStatusAfterPrePayment(Estimate tblOrder, OrderStatus orderStatus, StoreMode mode)
        {
            bool result = false;
            Organisation org = null;
            try
            {
                if (tblOrder != null)
                {
                    tblOrder.StatusId = (short)orderStatus;
                    tblOrder.Order_Date = DateTime.Now;
                    tblOrder.ArtworkByDate = DateTime.Now.AddDays(2);
                    tblOrder.DataByDate = DateTime.Now.AddDays(2);
                    tblOrder.PaperByDate = DateTime.Now.AddDays(2);
                    tblOrder.TargetPrintDate = DateTime.Now.AddDays(2);
                    tblOrder.TargetBindDate = DateTime.Now.AddDays(2);
                 
                    //List<Guid> StockManagerIds = new List<Guid>();
                    //if (mode == StoreMode.Retail)
                    //{
                    //    long? StoreId = db.Companies.Where(c => c.CompanyId == tblOrder.CompanyId).Select(s => s.StoreId).FirstOrDefault();
                    //    if (StoreId != null && StoreId > 0)
                    //    {
                    //        Company Store = db.Companies.Where(c => c.CompanyId == StoreId).FirstOrDefault();
                    //        if (Store != null)
                    //        {
                    //            if (Store.StockNotificationManagerId1 != null)
                    //            {
                    //                StockManagerIds.Add((Guid)Store.StockNotificationManagerId1);
                    //            }
                    //            if (Store.StockNotificationManagerId2 != null)
                    //            {
                    //                StockManagerIds.Add((Guid)Store.StockNotificationManagerId2);
                    //            }
                    //            org = db.Organisations.Where(o => o.OrganisationId == Store.OrganisationId).FirstOrDefault();
                    //            tblOrder.OrderManagerId = Store.AccountManagerId;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    Company Store = db.Companies.Where(c => c.CompanyId == tblOrder.CompanyId).FirstOrDefault();
                    //    if (Store != null)
                    //    {
                    //        if (Store.StockNotificationManagerId1 != null)
                    //        {
                    //            StockManagerIds.Add((Guid)Store.StockNotificationManagerId1);
                    //        }
                    //        if (Store.StockNotificationManagerId2 != null)
                    //        {
                    //            StockManagerIds.Add((Guid)Store.StockNotificationManagerId2);
                    //        }
                    //        org = db.Organisations.Where(o => o.OrganisationId == Store.OrganisationId).FirstOrDefault();
                    //        tblOrder.OrderManagerId = Store.AccountManagerId;
                    //    }
                    //}
                    //if (StockManagerIds != null && StockManagerIds.Count > 0)
                    //{
                    //    tblOrder.SalesPersonId = StockManagerIds[0];
                    //    tblOrder.OfficialOrderSetBy = StockManagerIds[0];
                    //    tblOrder.CreditLimitSetBy = StockManagerIds[0];
                    //}
                    // Approve the credit after user has pay online
                    tblOrder.IsCreditApproved = 1;

                   // UpdateOrderedItems(orderStatus, tblOrder, ItemStatuses.NotProgressedToJob, mode, org, null);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/mpc_content/Exception/ErrorLog.txt");

                using (StreamWriter writer = new StreamWriter(virtualFolderPth, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString() +
                         "" + Environment.NewLine + "mode :" + mode + " orderStatus" + orderStatus + "tblOrder" + tblOrder.EstimateId);
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
            }

            return result;
        }
        private void UpdateOrderedItems(OrderStatus orderStatus, Estimate tblOrder, ItemStatuses itemStatus, StoreMode Mode, Organisation org, List<Guid> MgrIds)
        {

            tblOrder.Items.ToList().ForEach(item =>
            {
                if (item.IsOrderedItem.HasValue && item.IsOrderedItem.Value == true)
                {
                    if (orderStatus != OrderStatus.ShoppingCart)
                        item.StatusId = (short)itemStatus;

                    db.Configuration.LazyLoadingEnabled = false;
                    Item ActualItem = db.Items.Where(i => i.ItemId == item.RefItemId).FirstOrDefault();
                    if (ActualItem != null)
                    {
                        if (ActualItem.IsStockControl == true && ActualItem.ProductType == (int)ProductType.NonPrintProduct)
                        {
                            ItemSection FirstItemSection = db.ItemSections.Where(sec => sec.SectionNo == 1 && sec.ItemId == item.ItemId).FirstOrDefault();
                            if (FirstItemSection != null)
                            {
                                updateStockAndSendNotification(FirstItemSection.StockItemID1 ?? 0, ActualItem.ItemId, Mode, tblOrder.CompanyId, Convert.ToInt32(item.Qty1), Convert.ToInt32(tblOrder.ContactId), item.ItemId, tblOrder.EstimateId, MgrIds, org);
                            }
                        }
                    }
                }
                else
                {
                    //Delete the non included items
                    bool result = false;
                    List<ArtWorkAttatchment> itemAttatchments = null;
                    Template clonedTempldateFiles = null;

                    result = RemoveCloneItem(item.ItemId, out itemAttatchments, out clonedTempldateFiles);
                    if (result)
                    {


                        RemoveItemAttacmentPhysically(itemAttatchments); // file removing physicslly
                        if (clonedTempldateFiles != null)
                            DeleteTemplateFiles(clonedTempldateFiles.ProductId, org.OrganisationId); // file removing
                    }
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
                    if (tblItem.ItemAttachments != null)
                    {
                        if (tblItem.ItemAttachments.Count > 0)
                        {
                            //Delete Attachments                       
                            tblItem.ItemAttachments.ToList().ForEach(att =>
                            {
                                db.ItemAttachments.Remove(att); // remove

                                //if (att.FileType == ".pdf")
                                itemAttatchments.Add(PopulateUploadedAttactchment(att)); // gathers attatments list as well.
                            });
                        }
                    }

                    //Remove the Templates if he has designed any
                    if (tblItem.TemplateId != null && tblItem.TemplateId > 0)
                    {
                        if (!ValidateIfTemplateIDIsAlreadyBooked(tblItem.ItemId, tblItem.TemplateId))
                            clonedTemplate = RemoveTemplates(tblItem.TemplateId);
                    }

                    if (tblItem.ItemSections != null)
                    {


                        //Section cost centeres
                        tblItem.ItemSections.ToList().ForEach(itemSection => itemSection.SectionCostcentres.ToList().ForEach(sectCost => db.SectionCostcentres.Remove(sectCost)));


                        //Item Section
                        tblItem.ItemSections.ToList().ForEach(itemsect => db.ItemSections.Remove(itemsect));


                    }
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
                Template tblTemplate = db.Templates.Include("TemplatePages").Include("TemplateColorStyles")
                    .Include("TemplateBackgroundImages")
                    .Include("TemplateFonts").Include("TemplateObjects")
                    .Where(template => template.ProductId == templateID.Value).FirstOrDefault();

                if (tblTemplate != null)
                {
                    //color Style
                    if (tblTemplate.TemplateColorStyles != null)
                    {
                        if (tblTemplate.TemplateColorStyles.Count > 0)
                        {
                            tblTemplate.TemplateColorStyles.ToList().ForEach(tempColorStyle => db.TemplateColorStyles.Remove(tempColorStyle));
                        }
                    }
                    //backgourd
                    if (tblTemplate.TemplateBackgroundImages != null)
                    {
                        if (tblTemplate.TemplateBackgroundImages.Count > 0)
                        {
                            tblTemplate.TemplateBackgroundImages.ToList().ForEach(tempBGImages => db.TemplateBackgroundImages.Remove(tempBGImages));
                        }


                    }
                    //font
                    if (tblTemplate.TemplateFonts != null)
                    {
                        if (tblTemplate.TemplateFonts.Count > 0)
                        {
                            tblTemplate.TemplateFonts.ToList().ForEach(tempFonts => db.TemplateFonts.Remove(tempFonts));
                        }

                    }
                    //object
                    if (tblTemplate.TemplateObjects != null)
                    {
                        if (tblTemplate.TemplateObjects.Count > 0)
                        {
                            tblTemplate.TemplateObjects.ToList().ForEach(tempObj => db.TemplateObjects.Remove(tempObj));
                        }

                    }
                    //Page
                    if (tblTemplate.TemplatePages != null)
                    {
                        if (tblTemplate.TemplatePages.Count > 0)
                        {
                            tblTemplate.TemplatePages.ToList().ForEach(tempPage => db.TemplatePages.Remove(tempPage));
                        }

                    }

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

        public void updateStockAndSendNotification(long StockID, long ItemId, StoreMode Mode, long companyId, int orderedQty, long contactId, long orderedItemid, long OrderId, List<Guid> MgrIds, Organisation org)
        {
            StockItem tblItemStock = null;

            if (StockID > 0)
            {
                tblItemStock = db.StockItems.Where(i => i.StockItemId == StockID && i.OrganisationId == org.OrganisationId).FirstOrDefault();
                if (tblItemStock != null)
                {
                    double currentStock = tblItemStock.inStock ?? 0;
                    int lastModified = Convert.ToInt32(tblItemStock.inStock) - orderedQty;
                    tblItemStock.inStock = lastModified;
                    tblItemStock.LastOrderDate = DateTime.Now;
                    tblItemStock.LastOrderQty = orderedQty;
                    if (tblItemStock.inStock < 0)
                    {
                        tblItemStock.inStock = 0;
                    }
                    ItemStockUpdateHistory stockLog = new ItemStockUpdateHistory();
                    stockLog.ItemId = (int)ItemId;
                    stockLog.StockItemId = StockID;
                    //stockLog.LastAvailableQty = currentStock;
                    //stockLog.LastOrderedQty = orderedQty;
                    stockLog.LastModifiedQty = orderedQty;// lastModified;
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


                    if (tblItemStock != null)
                    {
                        long ManagerID = 0;
                        // send emails to the managers
                        if (tblItemStock.isAllowBackOrder == true)
                        {
                            if (Mode == StoreMode.Corp)
                            {
                                ManagerID = GetContactByRole(companyId, (int)Roles.Manager);
                                _campaignRepository.stockNotificationToManagers(MgrIds, companyId, org, StoreMode.Corp, ManagerID, ItemId, (int)Events.BackOrder_Notifiaction_To_Manager, contactId, orderedItemid, tblItemStock.StockItemId, OrderId);

                            }
                            else
                            {
                                _campaignRepository.stockNotificationToManagers(MgrIds, companyId, org, StoreMode.Retail, companyId, ItemId, (int)Events.BackOrder_Notifiaction_To_Manager, contactId, orderedItemid, tblItemStock.StockItemId, OrderId);

                            }
                        }
                        if (tblItemStock.inStock < tblItemStock.ThresholdLevel)
                        {
                            if (Mode == StoreMode.Corp)
                            {
                                ManagerID = GetContactByRole(companyId, (int)Roles.Manager);
                                _campaignRepository.stockNotificationToManagers(MgrIds, companyId, org, StoreMode.Corp, ManagerID, ItemId, (int)Events.ThresholdLevelReached_Notification_To_Manager, contactId, orderedItemid, tblItemStock.StockItemId, OrderId);
                            }

                            else
                            {
                                _campaignRepository.stockNotificationToManagers(MgrIds, companyId, org, StoreMode.Retail, companyId, ItemId, (int)Events.ThresholdLevelReached_Notification_To_Manager, contactId, orderedItemid, tblItemStock.StockItemId, OrderId);

                            }
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
        /// <summary>
        /// return order with updated status
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="orderStatus"></param>
        /// <param name="currentStoreMode"></param>
        /// <returns></returns>
        public bool UpdateOrderAndCartStatus(long OrderID, OrderStatus orderStatus, StoreMode currentStoreMode, Organisation Org, List<Guid> ManagerIds, long StoreId)
        {
            Prefix prefix = db.Prefixes.Where(c => c.OrganisationId == Org.OrganisationId).FirstOrDefault();

            Estimate tblOrder = db.Estimates.Where(estm => estm.EstimateId == OrderID).FirstOrDefault();

            tblOrder.StatusId = (short)orderStatus;
            tblOrder.Order_Date = DateTime.Now;
            tblOrder.ArtworkByDate = DateTime.Now.AddDays(2);
            tblOrder.DataByDate = DateTime.Now.AddDays(2);
            tblOrder.PaperByDate = DateTime.Now.AddDays(2);
            tblOrder.TargetPrintDate = DateTime.Now.AddDays(2);
            tblOrder.TargetBindDate = DateTime.Now.AddDays(2);
            if (prefix != null)
            {
                tblOrder.Order_Code = prefix.OrderPrefix + "-" + prefix.OrderNext.ToString();
                prefix.OrderNext = prefix.OrderNext + 1;
            }
            if (ManagerIds != null && ManagerIds.Count > 0)
            {
                tblOrder.SalesPersonId = ManagerIds[0];
                tblOrder.OfficialOrderSetBy = ManagerIds[0];
                tblOrder.CreditLimitSetBy = ManagerIds[0];
            }
            Company oCompany = db.Companies.Where(c => c.CompanyId == StoreId).FirstOrDefault();
            if (oCompany != null)
            {
                tblOrder.OrderManagerId = oCompany.AccountManagerId;
            }

            UpdateOrderedItems(orderStatus, tblOrder, ItemStatuses.NotProgressedToJob, currentStoreMode, Org, ManagerIds);

            db.SaveChanges();

            return true;

        }
        public Estimate GetLastOrderByContactID(long contactID)
        {

            List<Estimate> ordesList = db.Estimates.Where(order => order.ContactId == contactID && order.isEstimate == false).Take(1).ToList();
            if (ordesList.Count > 0)
                return ordesList[0];
            else
                return null;
        }
        public List<Order> GetOrdersListByContactID(long contactUserID, OrderStatus? orderStatus, string fromDate, string toDate, string orderRefNumber, int pageSize, int pageNumber)
        {
            List<Order> ordersList = null;
            int resultsCount = 0;
            int startIndex = 0;
            DateTime resultFromDate;
            DateTime resultToDate;

            DateTime? actualFromDate = null;
            DateTime? actualToDate = null;


            int orderStatusID = (orderStatus.HasValue && (int)orderStatus.Value > 0) ? (int)orderStatus.Value : 0;

            if (!string.IsNullOrWhiteSpace(fromDate) && DateTime.TryParse(fromDate, out resultFromDate))
                actualFromDate = resultFromDate;

            if (!string.IsNullOrWhiteSpace(toDate) && DateTime.TryParse(toDate, out resultToDate))
                actualToDate = resultToDate;

            if (actualToDate.HasValue)
            {
                actualToDate = actualToDate.Value.AddHours(23);
                actualToDate = actualToDate.Value.AddMinutes(59.5);

            }

            var query = from tblOrd in db.Estimates
                        join tblStatuses in db.Statuses on tblOrd.StatusId equals tblStatuses.StatusId
                        join tblContacts in db.CompanyContacts on tblOrd.ContactId equals tblContacts.ContactId
                        join tblcompany in db.Companies on tblContacts.CompanyId equals tblcompany.CompanyId
                        orderby tblOrd.Order_Date descending
                        where tblOrd.ContactId == contactUserID // only that specific user
                        && tblOrd.isEstimate == false
                        && tblStatuses.StatusType == 2 //The status type should be 2 only for orders
                        && tblOrd.StatusId != (int)OrderStatus.ShoppingCart // Not Shopping Cart
                        && tblOrd.StatusId != (int)OrderStatus.ArchivedOrder // Not Archived
                        && tblOrd.StatusId == (orderStatusID > 0 ? (short?)orderStatusID : tblOrd.StatusId)
                            // && (tblOrd.CustomerPO.Contains(orderRefNumber)) //== ((orderRefNumber == null || orderRefNumber == "") ? tblOrd.CustomerPO : orderRefNumber) || tblcompany.Name.Contains(orderRefNumber) || tblContacts.FirstName.Contains(orderRefNumber) || tblContacts.LastName.Contains(orderRefNumber)) 
                        && (actualFromDate.HasValue ? tblOrd.Order_Date >= actualFromDate : true)
                        && (actualToDate.HasValue ? tblOrd.StartDeliveryDate <= actualToDate : true)
                            && (tblOrd.CustomerPO == ((orderRefNumber == null || orderRefNumber == "") ? tblOrd.CustomerPO : orderRefNumber))
                        select new Order()
                        {
                            OrderID = tblOrd.EstimateId,
                            OrderCode = tblOrd.Order_Code,
                            ProductName = tblOrd.Estimate_Name,
                            StatusID = tblOrd.StatusId,
                            StatusName = tblStatuses.StatusName,
                            StatusTypeID = tblStatuses.StatusType,
                            ContactUserID = tblOrd.ContactId,
                            CustomerID = tblOrd.CompanyId,
                            OrderDate = tblOrd.Order_Date,
                            DeliveryDate = tblOrd.StartDeliveryDate,
                            YourRef = tblOrd.CustomerPO,
                            ClientStatusID = tblOrd.ClientStatus,
                            RejectionReason = tblOrd.RejectionReason,
                            // SOrderDate =tblOrd.Order_Date.HasValue?tblOrd.Order_Date.Value.ToString("MMMM dd, yyyy"):string.Empty, // FormatDateValue(tblOrd.Order_Date),
                            // SOrderDeliveryDate = tblOrd.StartDeliveryDate.HasValue? tblOrd.StartDeliveryDate.Value.ToString("MMMM dd, yyyy") : string.Empty,
                            // ClientStatusName=tblStatuses.StatusName;
                        };
            //query.ToList().ForEach(o => o.SOrderDate = o.DeliveryDate != null ? o.OrderDate.Value.ToString("MMMM dd, yyyy") : string.Empty);
            //query.ToList().ForEach(o => o.SOrderDeliveryDate = o.DeliveryDate != null ? o.DeliveryDate.Value.ToString("MMMM dd, yyyy") : string.Empty);
            ordersList = query.ToList<Order>();

            if (orderStatusID > 0)
            {
                ordersList = ordersList.Where(st => st.StatusID == orderStatusID).ToList();
            }
            // filter list by PO
            if (!string.IsNullOrEmpty(orderRefNumber))
            {
                ordersList = ordersList.Where(po => po.YourRef != null && po.YourRef.Contains(orderRefNumber)).ToList();
                // resultData = resultData.Where(po => po.YourRef == null ? po.YourRef != orderRefNumber : po.YourRef.Contains(orderRefNumber)).ToList();
            }

            if (actualFromDate != null && actualToDate != null)
            {
                //   DateTime actualFrommDate =Convert.ToDateTime(actualFromDate).AddHours(11).AddMinutes(59);

                DateTime actualtooDate = Convert.ToDateTime(actualToDate).AddHours(23).AddMinutes(59);

                ordersList = ordersList.Where(date => date.OrderDate >= actualFromDate && date.OrderDate <= actualtooDate).ToList();

            }
            else if (actualFromDate != null)
            {
                ordersList = ordersList.Where(fromdate => fromdate.OrderDate >= actualFromDate).ToList();
            }
            else if (actualToDate != null)
            {
                ordersList = ordersList.Where(todate => todate.OrderDate <= actualToDate).ToList();
            }



            ordersList.ForEach(o => o.SOrderDate = o.DeliveryDate != null ? o.OrderDate.Value.ToString("MMMM dd, yyyy") : string.Empty);
            ordersList.ForEach(o => o.SOrderDeliveryDate = o.DeliveryDate != null ? o.DeliveryDate.Value.ToString("MMMM dd, yyyy") : string.Empty);
            return ordersList;
        }
        public string FormatDateValue(DateTime? dateTimeValue)
        {
            string formatString = null;
            const string defaultFormat = "MMMM d, yyyy";

            if (dateTimeValue.HasValue)
                return dateTimeValue.Value.ToString(string.IsNullOrWhiteSpace(formatString) ? defaultFormat : formatString);
            else
                return string.Empty;
        }
        public Order GetOrderAndDetails(long orderID)
        {
            //  db.Configuration.LazyLoadingEnabled = false;
            Estimate tblOrd = null;
            CompanyContact tblCC = null;
            //Model.ShoppingCart shopCart = null;
            Order userOrder = null;

            tblOrd = db.Estimates.Where(estm => estm.EstimateId == orderID).FirstOrDefault();

            //tbl_prefixes prefix = PrefixManager.GetDefaultPrefix(dbContext);

            //if (prefix != null)
            //{
            //    tblOrder.Order_Code = prefix.OrderPrefix + "-001-" + prefix.OrderNext.ToString();
            //    prefix.OrderNext = prefix.OrderNext + 1;
            //}
            if (tblOrd != null)
            {
                tblCC = db.CompanyContacts.Where(cc => cc.ContactId == tblOrd.ContactId).FirstOrDefault();
                userOrder = new Order()
                {
                    OrderID = tblOrd.EstimateId,
                    OrderCode = tblOrd.Order_Code,
                    ProductName = tblOrd.Estimate_Name,
                    StatusID = tblOrd.StatusId,
                    ClientStatusID = tblOrd.ClientStatus,
                    StatusName = tblOrd.Status.StatusName,
                    StatusTypeID = tblOrd.Status.StatusType,
                    ContactUserID = tblOrd.ContactId,
                    CustomerID = tblOrd.CompanyId,
                    OrderDate = tblOrd.Order_Date,
                    DeliveryDate = tblOrd.StartDeliveryDate, //estimated Delivery date
                    DeliveryAddressID = tblOrd.AddressId,
                    BillingAddressID = (long)tblOrd.BillingAddressId,
                    YourRef = tblOrd.CustomerPO,
                    SpecialInstNotes = tblOrd.UserNotes,
                    PlacedBy = string.Format("{0} {1}", tblOrd.CompanyContact.FirstName, tblOrd.CompanyContact.LastName),
                    // CompanyName = tblCC.Name,
                    OrderTotal = tblOrd.Estimate_Total ?? 0,
                    DeliveryCost = tblOrd.DeliveryCost ?? 0,
                    CompanyName = tblOrd.Company.Name
                };

                //userOrder.OrderDetails = this.ExtractShoppingCart(tblOrd);
                userOrder.OrderDetails = ExtractShoppingCartForOrder(tblOrd);




            }
            return userOrder;
        }
        public Address GetAddress(long AddressId)
        {

            return db.Addesses.Where(i => i.AddressId == AddressId).FirstOrDefault();
        }
        public long ReOrder(long ExistingOrderId, long loggedInContactID, double StatTaxVal, StoreMode mode, bool isIncludeTax, int TaxID, long OrganisationId)
        {
            Estimate ExistingOrder = null;
            Estimate shopCartOrder = null;
            bool result = false;
            // DbTransaction transaction = null;
            List<Item> ClonedItems = new List<Item>();
            long OrderIdOfReorderItems = 0;
            //using (var dbContextTransaction = db.Database.BeginTransaction())
            //{
            try
            {
                ExistingOrder = db.Estimates.Where(estm => estm.EstimateId == ExistingOrderId).FirstOrDefault();
                //    transaction = DALUtility.BeginTransactionWithOpenCon(dbContext);

                if (ExistingOrder != null)
                {
                    //productManager = new ProductManager();
                    //  shopCartOrder = OrderManager.GetShoppingCartOrderByContactID(dbContext, loggedInContactID, OrderManager.OrderStatus.ShoppingCart);
                    shopCartOrder = GetShoppingCartOrderByContactID(loggedInContactID, OrderStatus.ShoppingCart);
                    //create a new cart
                    if (shopCartOrder == null)
                    {
                        // shopCartOrder = Clone<db.Estimates>(ExistingOrder); // copying order header
                        shopCartOrder = ExistingOrder;
                        // shopCartOrder.EstimateId = 0;
                        // Order status will be shopping cart
                        shopCartOrder.StatusId = (int)OrderStatus.ShoppingCart;
                        shopCartOrder.DeliveryCompletionTime = 0;
                        shopCartOrder.DeliveryCost = 0;
                        shopCartOrder.DeliveryCostCenterId = 0;
                        shopCartOrder.StartDeliveryDate = null;
                        Prefix prefix = _prefixrepository.GetDefaultPrefix(OrganisationId);
                        if (prefix != null)
                        {
                            shopCartOrder.Order_Code = prefix.OrderPrefix + "-" + prefix.OrderNext.ToString();
                            prefix.OrderNext = prefix.OrderNext + 1;
                        }
                        shopCartOrder.Order_CompletionDate = null;
                        shopCartOrder.Order_ConfirmationDate = null;
                        shopCartOrder.Order_CreationDateTime = DateTime.Now;
                        shopCartOrder.CustomerPO = null;

                        db.Estimates.Add(shopCartOrder); //dbcontext added



                        OrderIdOfReorderItems = shopCartOrder.EstimateId;
                    }
                    else
                    {
                        OrderIdOfReorderItems = shopCartOrder.EstimateId;
                    }
                    List<Item> esxistingOrderItems = db.Items.Where(i => i.EstimateId == ExistingOrderId && i.IsOrderedItem == true).ToList();
                    //Clone items related to this order
                    esxistingOrderItems.Where(i => i.ItemType != Convert.ToInt32(ItemTypes.Delivery)).ToList().ForEach(orderITem =>
                    {
                        Item item = _ItemRepository.CloneReOrderItem(OrderIdOfReorderItems, orderITem.ItemId, loggedInContactID, shopCartOrder.Order_Code, OrganisationId);
                        ClonedItems.Add(item);
                        CopyAttachments(orderITem.ItemId, item, shopCartOrder.Order_Code, false, shopCartOrder.CreationDate ?? DateTime.Now);

                    });

                    if (ExistingOrder.DiscountVoucherID.HasValue && ExistingOrder.VoucherDiscountRate > 0)
                    {
                        if (RollBackDiscountedItemsWithdbContext(ClonedItems, StatTaxVal))
                        {
                            ExistingOrder.VoucherDiscountRate = null;
                            ExistingOrder.DiscountVoucherID = null;
                            shopCartOrder.VoucherDiscountRate = null;
                            shopCartOrder.DiscountVoucherID = null;
                        }
                    }
                    else if (isIncludeTax)// apply the new state Tax Value to the cloned item 
                    {
                        ApplyCurrentTax(ClonedItems, StatTaxVal, TaxID);
                    }
                    result = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // RollBackTransaction(transaction);
                //dbContextTransaction.Rollback();
                throw ex;
            }
            //finally
            //{
            //    if (result)
            //    {
            //        //result = DALUtility.CommitTransaction(transaction, dbContext);
            //    }
            //    else
            //    {
            //      //  DALUtility.RollBackTransaction(transaction, dbContext);
            //    }


            //   // dbContext.Dispose();
            //   // dbContext = null;
            //}
            return shopCartOrder.EstimateId;
            //}
        }
        public string GetTemplateAttachmentFileName(string ProductCode, string OrderCode, string ItemCode, string SideCode, string VirtualFolderPath, string extension, DateTime CreationDate)
        {
            string FileName = CreationDate.Year.ToString() + CreationDate.ToString("MMMM") + CreationDate.Day.ToString() + "-" + ProductCode + "-" + OrderCode + "-" + ItemCode + "-" + SideCode + extension;

            return FileName;
        }

        public bool RollBackDiscountedItemsWithdbContext(List<Item> clonedItems, double StateTax)
        {
            double QtyNewTotal = 0;
            double QtyTaxVal = 0;
            Organisation vwCompanySite = null;
            //   CompanySiteManager compSiteManager = new CompanySiteManager();
            //  vwCompanySite = compSiteManager.GetCompanySiteDataWithTaxes();
            if (clonedItems != null)
            {
                foreach (var item in clonedItems.Where(i => i.ItemType != Convert.ToInt32(ItemTypes.Delivery)))
                {
                    SectionCostcentre SC = item.ItemSections.FirstOrDefault().SectionCostcentres.Where(c => c.CostCentreId == (int)CostCentresForWeb.WebOrderCostCentre).FirstOrDefault();
                    //if (Mode == StoreMode.Broker)
                    //{
                    //    QtyNewTotal = (double)item.NetTotalBroker + (double)item.CostCentreProfitBroker;
                    //    QtyTaxVal = (QtyNewTotal * StateTax) / 100;
                    //    item.NetTotalBroker = QtyNewTotal;
                    //    item.BaseChargeBroker = QtyNewTotal;
                    //    item.TaxValueBroker = QtyTaxVal;
                    //    item.GrossTotalBroker = QtyNewTotal + QtyTaxVal;
                    //    item.tbl_item_sections.FirstOrDefault().BaseCharge1Broker += (double)item.CostCentreProfitBroker;
                    //    if (SC != null)
                    //    {
                    //        SC.QtyChargeBroker += (double)item.CostCentreProfitBroker;
                    //        // SC.Qty1MarkUpValue = 0;
                    //    }
                    //    item.CostCentreProfitBroker = 0;
                    //}
                    QtyNewTotal = (double)item.Qty1NetTotal + (double)item.Qty1CostCentreProfit;
                    QtyTaxVal = (QtyNewTotal * StateTax) / 100;
                    item.Qty1NetTotal = QtyNewTotal;
                    item.Qty1BaseCharge1 = QtyNewTotal;
                    item.Qty1Tax1Value = QtyTaxVal;
                    item.Qty1GrossTotal = QtyNewTotal + QtyTaxVal;
                    //item.Tax1 = vwCompanySite.StateTaxID;
                    item.Tax1 = 0;
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

        public Estimate GetShoppingCartOrderByContactID(long contactID, OrderStatus orderStatus)
        {
            int orderStatusID = (int)orderStatus;
            List<Estimate> ordesList = db.Estimates.Where(order => order.ContactId == contactID && order.StatusId == orderStatusID && order.isEstimate == false).Take(1).ToList();
            if (ordesList.Count > 0)
                return ordesList[0];
            else
                return null;

        }

        public List<Order> GetOrdersListExceptPendingOrdersByContactID(long contactUserID, OrderStatus? orderStatus, string fromDate, string toDate, string orderRefNumber, int pageSize, int pageNumber)
        {

            List<Order> ordersList = null;
            //  int resultsCount = 0;
            //  int startIndex = 0;
            DateTime resultFromDate;
            DateTime resultToDate;

            DateTime? actualFromDate = null;
            DateTime? actualToDate = null;

            int orderStatusID = (orderStatus.HasValue && (int)orderStatus.Value > 0) ? (int)orderStatus.Value : 0;

            if (!string.IsNullOrWhiteSpace(fromDate) && DateTime.TryParse(fromDate, out resultFromDate))
                actualFromDate = resultFromDate;

            if (!string.IsNullOrWhiteSpace(toDate) && DateTime.TryParse(toDate, out resultToDate))
                actualToDate = resultToDate;



            if (actualToDate.HasValue)
            {
                actualToDate = actualToDate.Value.AddHours(23);
                actualToDate = actualToDate.Value.AddMinutes(59.5);

            }

            var query = from tblOrd in db.Estimates
                        join tblStatuses in db.Statuses on tblOrd.StatusId equals tblStatuses.StatusId
                        join tblContacts in db.CompanyContacts on tblOrd.ContactId equals tblContacts.ContactId
                        join tblcompany in db.Companies on tblContacts.CompanyId equals tblcompany.CompanyId
                        orderby tblOrd.Order_Date descending
                        where tblOrd.ContactId == contactUserID // only that specific user
                        && tblOrd.isEstimate == false
                        && tblStatuses.StatusType == 2 //The status type should be 2 only for orders
                        && tblOrd.StatusId != (int)OrderStatus.ShoppingCart // Not Shopping Cart
                        && tblOrd.StatusId != (int)OrderStatus.ArchivedOrder // Not Archived
                            // && tblOrd.StatusID != (int)OrderStatus.PendingCorporateApprovel // Not Archived
                        && tblOrd.StatusId == (orderStatusID > 0 ? (short?)orderStatusID : tblOrd.StatusId)
                        && (tblOrd.CustomerPO == ((orderRefNumber == null || orderRefNumber == "") ? tblOrd.CustomerPO : orderRefNumber) || tblcompany.Name.Contains(orderRefNumber) || tblContacts.FirstName.Contains(orderRefNumber) || tblContacts.LastName.Contains(orderRefNumber))
                        && (actualFromDate.HasValue ? tblOrd.Order_Date >= actualFromDate : true)
                        && (actualToDate.HasValue ? tblOrd.Order_Date <= actualToDate : true)

                        select new Order()
                        {
                            OrderID = tblOrd.EstimateId,
                            OrderCode = tblOrd.Order_Code,
                            ProductName = tblOrd.Estimate_Name,
                            StatusID = tblOrd.StatusId,
                            StatusName = tblStatuses.StatusName,
                            StatusTypeID = tblStatuses.StatusType,
                            ContactUserID = tblOrd.ContactId,
                            CustomerID = tblOrd.CompanyId,
                            OrderDate = tblOrd.Order_Date,
                            DeliveryDate = tblOrd.StartDeliveryDate,
                            YourRef = tblOrd.CustomerPO,
                            ClientStatusID = tblOrd.ClientStatus,
                        };

            //  resultsCount = query.Count();
            //   if (resultsCount > 0 && resultsCount > pageSize)
            //   {
            //  startIndex = OrderManager.GetStartPageIndex(pageNumber, pageSize);
            //  ordersList = query.Skip(startIndex).Take(pageSize).ToList(); //all records
            //  }
            // else
            // {
            ordersList = query.ToList<Order>();
            //  }
            return ordersList;
        }

        // totalRecordsCount = resultsCount;

        public string GetAttachmentFileName(string ProductCode, string OrderCode, string ItemCode, string SideCode, string VirtualFolderPath, string extension, DateTime OrderCreationDate)
        {
            string FileName = OrderCreationDate.Year.ToString() + OrderCreationDate.ToString("MMMM") + OrderCreationDate.Day.ToString() + "-" + ProductCode + "-" + OrderCode + "-" + ItemCode + "-" + SideCode + extension;
            //checking whether file exists or not
            while (System.IO.File.Exists(VirtualFolderPath + FileName))
            {
                string fileName1 = System.IO.Path.GetFileNameWithoutExtension(FileName);
                fileName1 += "a";
                FileName = fileName1 + extension;
            }

            return FileName;
        }
        public void GenerateThumbnailForPdf(byte[] PDFFile, string sideThumbnailPath, bool insertCuttingMargin)
        {
            try
            {
                using (Doc theDoc = new Doc())
                {
                    theDoc.Read(PDFFile);
                    theDoc.PageNumber = 1;
                    theDoc.Rect.String = theDoc.CropBox.String;

                    if (insertCuttingMargin)
                    {
                        theDoc.Rect.Inset((int)MPC.Models.Common.Constants.CuttingMargin, (int)MPC.Models.Common.Constants.CuttingMargin);
                    }

                    Stream oImgstream = new MemoryStream();

                    theDoc.Rendering.DotsPerInch = 300;
                    theDoc.Rendering.Save("tmp.png", oImgstream);
                    theDoc.Clear();
                    theDoc.Dispose();
                    CreatAndSaveThumnail(oImgstream, sideThumbnailPath);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool CreatAndSaveThumnail(Stream oImgstream, string sideThumbnailPath)
        {
            try
            {
                string baseAddress = sideThumbnailPath.Substring(0, sideThumbnailPath.LastIndexOf('\\'));
                sideThumbnailPath = Path.GetFileNameWithoutExtension(sideThumbnailPath) + "Thumb.png";

                sideThumbnailPath = baseAddress + "\\" + sideThumbnailPath;

                Image origImage = Image.FromStream(oImgstream);

                float WidthPer, HeightPer;

                int NewWidth, NewHeight;
                int ThumbnailSizeWidth = 400;
                int ThumbnailSizeHeight = 400;

                if (origImage.Width > origImage.Height)
                {
                    NewWidth = ThumbnailSizeWidth;
                    WidthPer = (float)ThumbnailSizeWidth / origImage.Width;
                    NewHeight = Convert.ToInt32(origImage.Height * WidthPer);
                }
                else
                {
                    NewHeight = ThumbnailSizeHeight;
                    HeightPer = (float)ThumbnailSizeHeight / origImage.Height;
                    NewWidth = Convert.ToInt32(origImage.Width * HeightPer);
                }
                Bitmap origThumbnail = new Bitmap(NewWidth, NewHeight, origImage.PixelFormat);
                Graphics oGraphic = Graphics.FromImage(origThumbnail);
                oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle oRectangle = new Rectangle(0, 0, NewWidth, NewHeight);
                oGraphic.DrawImage(origImage, oRectangle);
                origThumbnail.Save(sideThumbnailPath, ImageFormat.Png);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool ApplyCurrentTax(List<Item> ClonedITem, double TaxValue, int TaxID)
        {
            Organisation vwCompanySite = null;
            // CompanySiteManager compSiteManager = new CompanySiteManager();
            vwCompanySite = _Organisationrepository.GetCompanySiteDataWithTaxes();
            if (ClonedITem != null)
            {
                foreach (var item in ClonedITem.Where(i => i.ItemType != Convert.ToInt32(ItemTypes.Delivery)))
                {
                    item.Tax1 = TaxID;
                    item.Qty1GrossTotal = _ItemRepository.GrossTotalCalculation(item.Qty1NetTotal ?? 0, TaxValue);
                    item.Qty1Tax1Value = _ItemRepository.CalculatePercentage(item.Qty1NetTotal ?? 0, TaxValue);
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

        public void CopyAttachments(long itemID, Item NewItem, string OrderCode, bool CopyTemplate, DateTime OrderCreationDate)
        {
            int sideNumber = 1;
            List<ItemAttachment> attchmentRes = _ItemAttachmentRepository.GetItemAttactchments(itemID);
            List<ItemAttachment> Newattchments = new List<ItemAttachment>();
            ItemAttachment obj = null;
            // MPCEntities dbContext = new MPCEntities();
            foreach (ItemAttachment attachment in attchmentRes)
            {
                obj = new ItemAttachment();

                obj.ApproveDate = attachment.ApproveDate;
                obj.Comments = attachment.Comments;
                obj.ContactId = attachment.ContactId;
                obj.ContentType = attachment.ContentType;
                //  obj.custome = attachment.cu
                obj.FileTitle = attachment.FileTitle;
                obj.FileType = attachment.FileType;
                obj.FolderPath = attachment.FolderPath;
                obj.IsApproved = attachment.IsApproved;
                obj.isFromCustomer = attachment.isFromCustomer;
                obj.Parent = attachment.Parent;
                obj.Type = attachment.Type;
                obj.UploadDate = attachment.UploadDate;
                obj.Version = attachment.Version;
                obj.ItemId = NewItem.ItemId;
                if (NewItem.TemplateId > 0)
                {
                    obj.FileName = GetTemplateAttachmentFileName(NewItem.ProductCode, OrderCode, NewItem.ItemCode, "Side" + sideNumber.ToString(), attachment.FolderPath, attachment.FileType, OrderCreationDate); //NewItemID + " Side" + sideNumber + attachment.FileType;
                }
                else
                {
                    obj.FileName = GetAttachmentFileName(NewItem.ProductCode, OrderCode, NewItem.ItemCode, sideNumber.ToString() + "Copy", attachment.FolderPath, attachment.FileType, OrderCreationDate); //NewItemID + " Side" + sideNumber + attachment.FileType;
                }
                sideNumber += 1;

                db.ItemAttachments.Add(obj);
                Newattchments.Add(obj);

                // Copy physical file
                string sourceFileName = null;
                string destFileName = null;
                if (NewItem.TemplateId > 0 && CopyTemplate == true)
                {
                    sourceFileName = HttpContext.Current.Server.MapPath(attachment.FolderPath + System.IO.Path.GetFileNameWithoutExtension(attachment.FileName) + "Thumb.png");
                    destFileName = HttpContext.Current.Server.MapPath(obj.FolderPath + obj.FileName);
                }
                else
                {
                    sourceFileName = HttpContext.Current.Server.MapPath(attachment.FolderPath + attachment.FileName);
                    destFileName = HttpContext.Current.Server.MapPath(obj.FolderPath + obj.FileName);
                }

                if (File.Exists(sourceFileName))
                {
                    File.Copy(sourceFileName, destFileName);

                    // Generate the thumbnail

                    byte[] fileData = File.ReadAllBytes(destFileName);

                    if (obj.FileType == ".pdf" || obj.FileType == ".TIF" || obj.FileType == ".TIFF")
                    {
                        GenerateThumbnailForPdf(fileData, destFileName, false);

                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        ms.Write(fileData, 0, fileData.Length);
                        CreatAndSaveThumnail(ms, destFileName);
                    }
                }

            }

            db.SaveChanges();
        }

        public List<Order> GetAllCorpOrders(long ContactCompany, OrderStatus? orderStatus, string fromDate, string toDate, string orderRefNumber, bool IsManager, long TerritoryId)
        {

            List<Order> ordersList = null;
            int resultsCount = 0;
            int startIndex = 0;
            DateTime resultFromDate;
            DateTime resultToDate;

            DateTime? actualFromDate = null;
            DateTime? actualToDate = null;

            int orderStatusID = (orderStatus.HasValue && (int)orderStatus.Value > 0) ? (int)orderStatus.Value : 0;

            if (!string.IsNullOrWhiteSpace(fromDate) && DateTime.TryParse(fromDate, out resultFromDate))
                actualFromDate = resultFromDate;

            if (!string.IsNullOrWhiteSpace(toDate) && DateTime.TryParse(toDate, out resultToDate))
                actualToDate = resultToDate;

            //if (actualToDate.HasValue)
            //{
            //    actualToDate = actualToDate.Value.AddHours(23);
            //    actualToDate = actualToDate.Value.AddMinutes(59.5);

            //}

            var query = from tblOrd in db.Estimates
                        join tblStatuses in db.Statuses on tblOrd.StatusId equals tblStatuses.StatusId
                        join tblContacts in db.CompanyContacts on tblOrd.ContactId equals tblContacts.ContactId
                        join tblcompany in db.Companies on tblContacts.CompanyId equals tblcompany.CompanyId
                        orderby tblOrd.Order_Date descending
                        where tblOrd.CompanyId == ContactCompany //|| tblOrd.ContactID == contactUserID// only that specific user
                        && tblOrd.isEstimate == false
                        && tblStatuses.StatusType == 2 //The status type should be 2 only for orders
                        && tblOrd.StatusId != (int)OrderStatus.ShoppingCart // Not Shopping Cart
                        && tblOrd.StatusId != (int)OrderStatus.ArchivedOrder // Not Archived
                        
                        select new Order()
                        {
                            OrderID = tblOrd.EstimateId,
                            OrderCode = tblOrd.Order_Code,
                            ProductName = tblOrd.Estimate_Name,
                            StatusID = tblOrd.StatusId,
                            StatusName = tblStatuses.StatusName,
                            StatusTypeID = tblStatuses.StatusType,
                            ContactUserID = tblOrd.ContactId,
                            CustomerID = tblOrd.CompanyId,
                            OrderDate = tblOrd.Order_Date,
                            DeliveryDate = tblOrd.StartDeliveryDate,
                            YourRef = tblOrd.CustomerPO,
                            CustomerName = tblContacts.FirstName,
                            TerritoryId = tblContacts.TerritoryId ?? 0,
                            CompanyName = tblcompany.Name,
                            RejectionReason=tblOrd.RejectionReason
                        };

            // filter list by status
            List<Order> resultData = new List<Order>();
            resultData = query.ToList<Order>();
            if (orderStatusID > 0)
            {
                resultData = resultData.Where(st => st.StatusID == orderStatusID).ToList();
            }
            // filter list by PO
            if (!string.IsNullOrEmpty(orderRefNumber))
            {
                resultData = resultData.Where(po => po.YourRef != null && po.YourRef.Contains(orderRefNumber)).ToList();
                // resultData = resultData.Where(po => po.YourRef == null ? po.YourRef != orderRefNumber : po.YourRef.Contains(orderRefNumber)).ToList();
            }

            if (actualFromDate != null && actualToDate != null)
            {
             //   DateTime actualFrommDate =Convert.ToDateTime(actualFromDate).AddHours(11).AddMinutes(59);

                DateTime actualtooDate = Convert.ToDateTime(actualToDate).AddHours(23).AddMinutes(59);

                resultData = resultData.Where(date => date.OrderDate >= actualFromDate && date.OrderDate <= actualtooDate).ToList();
               
            }
            else if (actualFromDate != null)
            {
                resultData = resultData.Where(fromdate => fromdate.OrderDate >= actualFromDate).ToList();
            }
            else if (actualToDate != null)
            {
                resultData = resultData.Where(todate => todate.OrderDate <= actualToDate).ToList();
            }

        
            resultData.ForEach(o => o.SOrderDate = o.DeliveryDate != null ? o.OrderDate.Value.ToString("MMMM dd, yyyy") : string.Empty);
            resultData.ForEach(o => o.SOrderDeliveryDate = o.DeliveryDate != null ? o.DeliveryDate.Value.ToString("MMMM dd, yyyy") : string.Empty);
            if (IsManager == true)
            {
                resultData.Where(i => i.TerritoryId == TerritoryId).ToList();
            }
            return resultData;
        }

        /// <summary>
        /// gets cart order by company id
        /// </summary>
        /// <param name="ContactId"></param>
        /// <param name="TemporaryCustomerId"></param>
        /// <returns></returns>
        public long GetOrderIdByCompanyId(long CompanyId, OrderStatus orderStatus)
        {
            try
            {
                int orderStatusID = (int)orderStatus;

                if (CompanyId > 0)
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    Estimate Order =
                        db.Estimates
                            .Where(
                                order =>
                                    order.CompanyId == CompanyId && order.StatusId == orderStatusID &&
                                    order.isEstimate == false)
                            .FirstOrDefault();
                    if (Order != null)
                    {
                        return Order.EstimateId;
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
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }
        public List<Order> GetPendingApprovelOrdersList(long contactUserID, bool isApprover, long companyId)
        {
            List<Order> ordersList = null;
            int orderStatusID = (int)OrderStatus.PendingCorporateApprovel;
            var query = from tblOrd in db.Estimates
                        join tblStatuses in db.Statuses on tblOrd.StatusId equals tblStatuses.StatusId
                        join tblContacts in db.CompanyContacts on tblOrd.ContactId equals tblContacts.ContactId
                        join tblContactCompany in db.Companies on tblContacts.CompanyId equals tblContactCompany.CompanyId
                        orderby tblOrd.Order_Date descending
                        where tblOrd.ContactId == (isApprover ? tblOrd.ContactId : contactUserID)   // only that specifc user
                        && tblOrd.isEstimate == false
                        && tblStatuses.StatusType == 2 //The status type should be 2 only for orders                            
                        && tblOrd.StatusId == orderStatusID // only pending approvel
                        && tblContactCompany.IsCustomer == (int)CustomerTypes.Corporate
                        && tblContactCompany.CompanyId == companyId
                        select new Order()
                        {
                            OrderID = tblOrd.EstimateId,
                            OrderCode = tblOrd.Order_Code,
                            ProductName = tblOrd.Estimate_Name,
                            StatusID = tblOrd.StatusId,
                            StatusName = tblStatuses.StatusName,
                            StatusTypeID = tblStatuses.StatusType,
                            ContactUserID = tblOrd.ContactId,
                            CustomerID = tblOrd.CompanyId,
                            OrderDate = tblOrd.Order_Date,
                            DeliveryDate = tblOrd.StartDeliveryDate,
                            YourRef = tblOrd.CustomerPO,
                            ContactTerritoryID = tblContacts.TerritoryId,
                            CustomerName = tblContacts.FirstName + " " + tblContacts.LastName,
                        };
            ordersList = query.ToList<Order>();
            ordersList.ForEach(o => o.SOrderDate = o.DeliveryDate != null ? o.OrderDate.Value.ToString("MMMM dd, yyyy") : string.Empty);
            ordersList.ForEach(o => o.SOrderDeliveryDate = o.DeliveryDate != null ? o.DeliveryDate.Value.ToString("MMMM dd, yyyy") : string.Empty);

            return ordersList;
        }

        public long ApproveOrRejectOrder(long orderID, long loggedInContactID, OrderStatus orderStatus, Guid OrdermangerID,string RejectionReason, string BrokerPO = "")
        {
            long result = 0;
            Estimate tblOrder = null;
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;

            // MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];
            //  DbTransaction dbTrans = null;
            try
            {
                short orderStatusID = (short)orderStatus;

                tblOrder = db.Estimates.Where(estm => estm.EstimateId == orderID).FirstOrDefault();

                if (tblOrder != null)
                {
                    tblOrder.StatusId = orderStatusID;

                    tblOrder.OrderManagerId = OrdermangerID;
                    tblOrder.RejectionReason = RejectionReason;

                    db.Estimates.Attach(tblOrder);

                    db.Entry(tblOrder).State = EntityState.Modified;

                    if (db.SaveChanges() > 0)
                    {
                        result = tblOrder.ContactId ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (result > 0)
                //    DALUtility.CommitTransaction(dbTrans, dbContext);
                //else
                //    DALUtility.RollBackTransaction(dbTrans, dbContext);

                //dbContext = null;
                //dbTrans = null;
            }

            return result;
        }


        public void DeleteOrderBySP(long OrderID)
        {
            try
            {
                db.usp_DeleteOrderByID(OrderID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteCart(long CompanyID)
        {
            try
            {
                db.usp_DeleteCarts(CompanyID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Estimate GetOrderByOrderID(long OrderID)
        {
            try
            {
                return db.Estimates.Where(o => o.EstimateId == OrderID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Estimate> GetCartOrdersByCompanyID(long CompanyID)
        {
            try
            {
                return db.Estimates.Where(c => c.CompanyId == CompanyID && c.StatusId == 3).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Download Artwork


        //public string GenerateOrderArtworkArchive(int OrderID, string sZipName)
        //{

        //    string ReturnRelativePath = string.Empty;
        //    string ReturnPhysicalPath = string.Empty;
        //    string sZipFileName = string.Empty;
        //    bool IncludeOrderReport = false;
        //    bool IncludeOrderXML = false;
        //    bool IncludeJobCardReport = false;
        //    bool MakeArtWorkProductionReady = false;

        //    string sCreateDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/" + this.OrganisationId);
        //    bool ArtworkProductionReadyResult = false;

        //    //string sZipName = string.Format("{0}_{1}_{2}", ((tbl_estimates)EditRecord).Order_Code, ContactCompanyName, BrokerName);

        //    try
        //    {
        //        db.Configuration.LazyLoadingEnabled = false;
        //        //filter the items which are of type delivery i.e. itemtype = 2
        //        List<Item> ItemsList = db.Items.Include("ItemAttachments").Where(i => i.EstimateId == OrderID && (i.ItemType == null || i.ItemType != 2)).ToList();
        //        Estimate oOrder = db.Estimates.Include("Company").Where(g => g.EstimateId == OrderID).Single();

        //        Company store = db.Companies.Where(c => c.CompanyId == oOrder.Company.StoreId).FirstOrDefault();
        //        if (store != null)
        //        {
        //            IncludeOrderReport = store.includeEmailArtworkOrderReport ?? false;
        //            IncludeJobCardReport = store.includeEmailArtworkOrderJobCard ?? false;
        //            IncludeOrderXML = store.includeEmailArtworkOrderXML ?? false;
        //            MakeArtWorkProductionReady = store.makeEmailArtworkOrderProductionReady ?? false;
        //        }
        //        else
        //        {
        //            store = db.Companies.Where(c => c.CompanyId == oOrder.Company.CompanyId).FirstOrDefault();
        //            if(store != null)
        //            {
        //                IncludeOrderReport = store.includeEmailArtworkOrderReport ?? false;
        //                IncludeJobCardReport = store.includeEmailArtworkOrderJobCard ?? false;
        //                IncludeOrderXML = store.includeEmailArtworkOrderXML ?? false;
        //                MakeArtWorkProductionReady = store.makeEmailArtworkOrderProductionReady ?? false;
        //            }
        //        }
        //        if (!IncludeOrderReport && !IncludeJobCardReport && !IncludeOrderXML && !MakeArtWorkProductionReady)
        //        {
        //            IncludeOrderReport = true;
        //        }

        //        //making the artwork production ready and regenerating template PDFs
        //        if (MakeArtWorkProductionReady)
        //        {
        //            ArtworkProductionReadyResult = MakeOrderArtworkProductionReady(oOrder);

        //        }

        //        if (sZipName == string.Empty)
        //        {
        //            sZipFileName = GetArchiveFileName(oOrder.Order_Code, oOrder.Company.Name, oOrder.EstimateId);
        //        }
        //        else
        //        {
        //            if (Path.HasExtension(sZipName))
        //                sZipFileName = sZipName;
        //            else
        //                sZipFileName = sZipName + ".zip";

        //        }

        //        //ReturnRelativePath = szDirectory + "/" + PathConstants.DownloadableFilesPath + sZipFileName;
        //        ReturnPhysicalPath = sCreateDirectory + sZipFileName;
        //        if (File.Exists(ReturnPhysicalPath))
        //        {
        //            //ReturnRelativePath = szDirectory + "/" + PathConstants.DownloadableFilesPath + sZipFileName;
        //            //  ReturnPhysicalPath = sCreateDirectory + sZipFileName;
        //            return ReturnPhysicalPath;
        //        }
        //        else
        //        {
        //            if (!Directory.Exists(sCreateDirectory))
        //            {
        //                Directory.CreateDirectory(sCreateDirectory);
        //            }

        //            string sFileFullPath = string.Empty;
        //            string sFolderPath = string.Empty;
        //            using (ZipFile zip = new ZipFile())
        //            {

        //                foreach (Item item in ItemsList)
        //                {

        //                    sFolderPath = sCreateDirectory + "\\" + MakeValidFileName(item.ProductCode ?? "pc101" + "-" + item.ProductName);

        //                    string ZipfolderName = MakeValidFileName(item.ProductCode ?? "pc101" + "-" + item.ProductName);

        //                    Directory.CreateDirectory(sFolderPath);

        //                    ZipEntry d = zip.AddDirectory(sFolderPath, "");

        //                    // item attachments
        //                    foreach (var attach in item.ItemAttachments)
        //                    {
        //                        //if artwork is production ready then pick the attachments from new location.
        //                        if (MakeArtWorkProductionReady && ArtworkProductionReadyResult)
        //                        {
        //                            attach.FolderPath = "MPC_Content/Artworks/" + OrganisationId + "/Production/";
        //                          //  attach.FolderPath = attach.FolderPath.Replace("Attachments", "Production");
        //                        }

        //                        string path = attach.FolderPath + "\\" + attach.FileName + attach.FileType;
        //                        sFileFullPath = HttpContext.Current.Server.MapPath("~/" + path);
        //                        //sFileFullPath = sCreateDirectory + "\\" + attach.FolderPath + "\\" + attach.FileName;
        //                        if (System.IO.File.Exists(sFileFullPath))
        //                        {
        //                            ZipEntry e = zip.AddFile(sFileFullPath, ZipfolderName);
        //                            e.Comment = "Created by My Print Cloud";
        //                        }
        //                    }

        //                    //job card report
        //                    if (IncludeJobCardReport)
        //                    {
        //                        string sJCReportPath = ExportOrderReportPDF(165, item.ItemId, true,OrderID);
        //                        if (System.IO.File.Exists(sJCReportPath))
        //                        {
        //                            ZipEntry jcr = zip.AddFile(sJCReportPath, ZipfolderName);
        //                            jcr.Comment = "Job Card Report by My Print Cloud";
        //                        }
        //                    }

        //                    //cleanup
        //                    Directory.Delete(sFolderPath);
        //                }

        //                //order report
        //                if (IncludeOrderReport)
        //                {
        //                    string sOrderReportPath = ExportOrderReportPDF(103, Convert.ToInt64(OrderID), false,OrderID);
        //                    if (System.IO.File.Exists(sOrderReportPath))
        //                    {
        //                        ZipEntry r = zip.AddFile(sOrderReportPath, "");
        //                        r.Comment = "Order Report by My Print Cloud";
        //                    }
        //                }
        //                // here xml comes
        //                if (IncludeOrderXML)
        //                {
        //                    string sOrderXMLReportPath = ExportOrderReportXML(OrderID, "", "0");
        //                    if (System.IO.File.Exists(sOrderXMLReportPath))
        //                    {
        //                        ZipEntry r = zip.AddFile(sOrderXMLReportPath, "");
        //                        r.Comment = "Order XML Report by My Print Cloud";
        //                    }
        //                }
        //                zip.Comment = "This zip archive was created by My Print Cloud MIS";
        //                if (Directory.Exists(sCreateDirectory))
        //                {
        //                    zip.Save(sCreateDirectory + "\\" + sZipFileName);
        //                }

        //                // DeleteFiles();
        //            }
        //            ReturnRelativePath = sCreateDirectory;
        //            ReturnPhysicalPath = sCreateDirectory + sZipFileName;
        //            return ReturnPhysicalPath;
        //        }
        //    }
        //    catch (System.Exception ex1)
        //    {
        //        throw ex1;
        //    }

        //}

        //private string ExportOrderReportPDF(int iReportID, long iRecordID, bool isItem,long OrderID)
        //{
        //    string sFilePath = string.Empty;
        //    try
        //    {
        //        Report currentReport = db.Reports.Where(c => c.ReportId == iReportID).FirstOrDefault();
        //        if (currentReport.ReportId > 0)
        //        {
        //            byte[] rptBytes = null;
        //            rptBytes = System.Text.Encoding.Unicode.GetBytes(currentReport.ReportTemplate);
        //            // Encoding must be done
        //            System.IO.MemoryStream ms = new System.IO.MemoryStream(rptBytes);
        //            // Load it to memory stream
        //            ms.Position = 0;
        //            SectionReport currReport = new SectionReport();
        //            string sFileName = iRecordID + "OrderReport.pdf";
        //            // FileNamesList.Add(sFileName);
        //            currReport.LoadLayout(ms);
        //            if (isItem)
        //            {
        //                sFileName = iRecordID + "JobCardReport.pdf";
        //              //  FileNamesList.Add(sFileName);
        //                List<usp_JobCardReport_Result> rptSource = db.usp_JobCardReport(OrganisationId, OrderID, iRecordID).ToList();
        //                currReport.DataSource = rptSource;
        //            }
        //            else
        //            {

        //                List<usp_OrderReport_Result> rptOrderSource = db.usp_OrderReport(OrganisationId, OrderID).ToList();
        //                currReport.DataSource = rptOrderSource;
        //            }

        //            if (currReport != null)
        //            {
        //                currReport.Run();
        //                GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdf = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
        //                string Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/" + this.OrganisationId + "/");
        //                if (!Directory.Exists(Path))
        //                {
        //                    Directory.CreateDirectory(Path);
        //                }
        //                // PdfExport pdf = new PdfExport();
        //                sFilePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/" + this.OrganisationId + "/") + sFileName;

        //                pdf.Export(currReport.Document, sFilePath);
        //                ms.Close();
        //                currReport.Document.Dispose();
        //                pdf.Dispose();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return sFilePath;
        //}

        //private string ExportOrderReportXML(int iRecordID, string OrderCode, string XMLFormat)
        //{
        //    string sFilePath = string.Empty;
        //    bool isCorporate = false;
        //    string CurrencySymbol = string.Empty;
        //    db.Configuration.LazyLoadingEnabled = true;
        //    db.Configuration.ProxyCreationEnabled = true;
        //    try
        //    {
        //        Estimate orderEntity = new Estimate();
        //        if (iRecordID > 0)
        //            orderEntity = db.Estimates.Where(e => e.EstimateId == iRecordID).FirstOrDefault();
        //        if (OrderCode != "")
        //            orderEntity = db.Estimates.Where(e => e.Order_Code == OrderCode).FirstOrDefault();

        //        long CurrencyID = db.Organisations.Where(c => c.OrganisationId == OrganisationId).Select(c => c.CurrencyId ?? 0).FirstOrDefault();
        //        Currency curr = db.Currencies.Where(c => c.CurrencyId == CurrencyID).FirstOrDefault();
        //        if (curr != null)
        //         CurrencySymbol = curr.CurrencySymbol;
        //        List<PrePayment> paymentsList = db.PrePayments.Where(p => p.OrderId == iRecordID).ToList();
        //        if (orderEntity != null)
        //        {

        //            XmlDocument XDoc = new XmlDocument();

        //            XmlNode docNode = XDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        //            XDoc.AppendChild(docNode);
        //            // Create root node.
        //            XmlElement XElemRoot = XDoc.CreateElement("Order");
        //            //Add the node to the document.
        //            XDoc.AppendChild(XElemRoot);

        //            XmlElement XTemp = XDoc.CreateElement("OrderDetail");


        //            XmlAttribute OrderUserNotesAttr = XDoc.CreateAttribute("UserNotes");
        //            if (!string.IsNullOrEmpty(orderEntity.UserNotes))
        //                OrderUserNotesAttr.Value = orderEntity.UserNotes;
        //            else
        //                OrderUserNotesAttr.Value = string.Empty;
        //            XTemp.SetAttributeNode(OrderUserNotesAttr);


        //            int SID = 0;
        //            if (orderEntity.SourceId != null)
        //                SID = (int)orderEntity.SourceId;

        //            string SourceName = db.PipeLineSources.Where(s => s.SourceId == SID).Select(x => x.Description).FirstOrDefault();
        //            XmlAttribute OrderSourceAttr = XDoc.CreateAttribute("Source");
        //            if (!string.IsNullOrEmpty(SourceName))
        //            {
        //                OrderSourceAttr.Value = SourceName;
        //            }
        //            else
        //            {
        //                OrderSourceAttr.Value = string.Empty;
        //            }
        //            XTemp.SetAttributeNode(OrderSourceAttr);


        //            DateTime dtCreation = new DateTime();
        //            string CreationDate = string.Empty;
        //            XmlAttribute OrderCreationAttr = XDoc.CreateAttribute("CreationDate");
        //            if (orderEntity.CreationDate != null)
        //            {
        //                dtCreation = Convert.ToDateTime(orderEntity.CreationDate);
        //                CreationDate = dtCreation.ToString("dd/MMM/yyyy");
        //                OrderCreationAttr.Value = CreationDate;
        //            }
        //            else
        //            {
        //                OrderCreationAttr.Value = string.Empty;
        //            }

        //            XTemp.SetAttributeNode(OrderCreationAttr);

        //            XmlAttribute OrderFooterAttr = XDoc.CreateAttribute("OrderFooter");
        //            if (!string.IsNullOrEmpty(orderEntity.FootNotes))
        //                OrderFooterAttr.Value = orderEntity.FootNotes;
        //            else
        //                OrderFooterAttr.Value = string.Empty;
        //            XTemp.SetAttributeNode(OrderFooterAttr);

        //            XmlAttribute OrderHeaderAttr = XDoc.CreateAttribute("OrderHeader");
        //            if (!string.IsNullOrEmpty(orderEntity.HeadNotes))
        //                OrderHeaderAttr.Value = orderEntity.HeadNotes;
        //            else
        //                OrderHeaderAttr.Value = string.Empty;
        //            XTemp.SetAttributeNode(OrderHeaderAttr);

        //            XmlAttribute OrderPONumbAttr = XDoc.CreateAttribute("CustomerPORef");
        //            int isPO = 1;
        //            string POString = string.Empty;
        //            if (orderEntity.CustomerPO != null)
        //            {
        //                OrderPONumbAttr.Value = orderEntity.CustomerPO;
        //            }
        //            else
        //            {
        //                OrderPONumbAttr.Value = string.Empty;
        //            }
        //            XTemp.SetAttributeNode(OrderPONumbAttr);


        //            XmlAttribute OrderCreditAppAttr = XDoc.CreateAttribute("OrderCreditApproved");
        //            int isApproved = 1;
        //            string ApproveString = string.Empty;
        //            if (orderEntity.IsCreditApproved != null)
        //            {
        //                isApproved = (int)orderEntity.IsCreditApproved;
        //                if (isApproved == 1)
        //                    ApproveString = "True";
        //                else
        //                    ApproveString = "False";
        //            }
        //            OrderCreditAppAttr.Value = ApproveString;
        //            XTemp.SetAttributeNode(OrderCreditAppAttr);

        //            SectionFlag oFlag = db.SectionFlags.Where(a => a.SectionFlagId == orderEntity.SectionFlagId).FirstOrDefault();
        //            XmlAttribute OrderflagAttr = XDoc.CreateAttribute("OrderFlag");
        //            if (oFlag != null)
        //                OrderflagAttr.Value = oFlag.FlagName;
        //            else
        //                OrderflagAttr.Value = string.Empty;
        //            XTemp.SetAttributeNode(OrderflagAttr);

        //            string OrderDateSta = string.Empty;
        //            //if (orderEntity.Order_Date != null)
        //            DateTime dtStart = new DateTime();
        //            XmlAttribute OrderDateStartAttr = XDoc.CreateAttribute("StartDeliveryDate");
        //            if (orderEntity.Order_Date != null)
        //            {
        //                dtStart = Convert.ToDateTime(orderEntity.StartDeliveryDate);
        //                OrderDateSta = dtStart.ToString("dd/MMM/yyyy");
        //                OrderDateStartAttr.Value = OrderDateSta;
        //            }
        //            else
        //                OrderDateStartAttr.Value = string.Empty;
        //            XTemp.SetAttributeNode(OrderDateStartAttr);

        //            string OrderDateFin = string.Empty;
        //            //if (orderEntity.Order_Date != null)
        //            DateTime dtFinsih = new DateTime();
        //            XmlAttribute OrderDateFinishAttr = XDoc.CreateAttribute("FinishDeliveryDate");
        //            if (orderEntity.Order_Date != null)
        //            {
        //                dtFinsih = Convert.ToDateTime(orderEntity.FinishDeliveryDate);
        //                OrderDateFin = dtFinsih.ToString("dd/MMM/yyyy");
        //                OrderDateFinishAttr.Value = OrderDateFin;
        //            }
        //            else
        //                OrderDateFinishAttr.Value = string.Empty;
        //            XTemp.SetAttributeNode(OrderDateFinishAttr);


        //            string OrderDate = string.Empty;
        //            //if (orderEntity.Order_Date != null)
        //            DateTime dt = new DateTime();
        //            XmlAttribute OrderDateAtt = XDoc.CreateAttribute("OrderDate");
        //            if (orderEntity.Order_Date != null)
        //            {
        //                dt = Convert.ToDateTime(orderEntity.Order_Date);
        //                OrderDate = dt.ToString("dd/MMM/yyyy");
        //                OrderDateAtt.Value = OrderDate;
        //            }
        //            else
        //                OrderDateAtt.Value = string.Empty;
        //            XTemp.SetAttributeNode(OrderDateAtt);

        //            XmlAttribute OrderCod = XDoc.CreateAttribute("OrderCode");
        //            OrderCod.Value = orderEntity.Order_Code; ;
        //            XTemp.SetAttributeNode(OrderCod);

        //            XmlAttribute status = XDoc.CreateAttribute("Status");
        //            status.Value = orderEntity.Status.StatusName;
        //            XTemp.SetAttributeNode(status);

        //            XmlAttribute Title = XDoc.CreateAttribute("Title");
        //            Title.Value = orderEntity.Estimate_Name;
        //            XTemp.SetAttributeNode(Title);

        //            XElemRoot.AppendChild(XTemp);

        //            foreach (Item items in orderEntity.Items)
        //            {
        //                if (items.ItemType == 2)
        //                {
        //                    XmlAttribute deliverycost = XDoc.CreateAttribute("DeliveryCost");
        //                    deliverycost.Value = Convert.ToString(items.Qty1GrossTotal ?? 0);
        //                    XTemp.SetAttributeNode(deliverycost);
        //                    XElemRoot.AppendChild(XTemp);
        //                }
        //            }

        //            XmlElement XCompanyTemp = XDoc.CreateElement("Company");

        //            XmlAttribute CredAttr = XDoc.CreateAttribute("CreditReference");
        //            if (!string.IsNullOrEmpty(orderEntity.Company.CreditReference))
        //                CredAttr.Value = orderEntity.Company.CreditReference;
        //            else
        //                CredAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(CredAttr);


        //            XmlAttribute CorpHomeAttr = XDoc.CreateAttribute("CorporateHomeURL");
        //            if (!string.IsNullOrEmpty(orderEntity.Company.RedirectWebstoreURL))
        //                CorpHomeAttr.Value = orderEntity.Company.RedirectWebstoreURL;
        //            else
        //                CorpHomeAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(CorpHomeAttr);

        //            XmlAttribute LinkAttr = XDoc.CreateAttribute("LinkedInURL");
        //            if (!string.IsNullOrEmpty(orderEntity.Company.LinkedinURL))
        //                LinkAttr.Value = orderEntity.Company.LinkedinURL;
        //            else
        //                LinkAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(LinkAttr);

        //            XmlAttribute TwitAttr = XDoc.CreateAttribute("TwitterURL");
        //            if (!string.IsNullOrEmpty(orderEntity.Company.TwitterURL))
        //                TwitAttr.Value = orderEntity.Company.TwitterURL;
        //            else
        //                TwitAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(TwitAttr);


        //            XmlAttribute FaceAttr = XDoc.CreateAttribute("FacebookURL");
        //            if (!string.IsNullOrEmpty(orderEntity.Company.FacebookURL))
        //                FaceAttr.Value = orderEntity.Company.FacebookURL;
        //            else
        //                FaceAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(FaceAttr);

        //            XmlAttribute WebUrlAttr = XDoc.CreateAttribute("WebURL");
        //            if (!string.IsNullOrEmpty(orderEntity.Company.URL))
        //                WebUrlAttr.Value = orderEntity.Company.URL;
        //            else
        //                WebUrlAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(WebUrlAttr);


        //            int nominalCode = 0;
        //            XmlAttribute NominalAttr = XDoc.CreateAttribute("DefaultNominalCode");
        //            if (orderEntity.Company != null)
        //                nominalCode = orderEntity.Company.DefaultNominalCode;
        //            string NominalCode = db.ChartOfAccounts.Where(x => x.Id == nominalCode).Select(a => a.Name ?? "N/A").FirstOrDefault();
        //            if (!string.IsNullOrEmpty(NominalCode))
        //                NominalAttr.Value = NominalCode;
        //            else
        //                NominalAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(NominalAttr);


        //            XmlAttribute AccBalAttr = XDoc.CreateAttribute("AccountBalance");
        //            if (orderEntity.Company.AccountBalance != null)
        //                AccBalAttr.Value = Convert.ToString(orderEntity.Company.AccountBalance);
        //            else
        //                AccBalAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(AccBalAttr);


        //            XmlAttribute VatRefAttr = XDoc.CreateAttribute("VATRefNumber");
        //            if (!string.IsNullOrEmpty(orderEntity.Company.VATRegReference))
        //                VatRefAttr.Value = orderEntity.Company.VATRegReference;
        //            else
        //                VatRefAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(VatRefAttr);


        //            XmlAttribute VatRegAttr = XDoc.CreateAttribute("VATRegNumber");
        //            if (!string.IsNullOrEmpty(orderEntity.Company.VATRegNumber))
        //                VatRegAttr.Value = orderEntity.Company.VATRegNumber;
        //            else
        //                VatRegAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(VatRegAttr);


        //            XmlAttribute CreditLimitAttr = XDoc.CreateAttribute("CreditLimit");
        //            if (orderEntity.Company.CreditLimit != null)
        //                CreditLimitAttr.Value = Convert.ToString(orderEntity.Company.CreditLimit);
        //            else
        //                CreditLimitAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(CreditLimitAttr);

        //            XmlAttribute NotesAttr = XDoc.CreateAttribute("Notes");
        //            if (!string.IsNullOrEmpty(orderEntity.Company.Notes))
        //                NotesAttr.Value = orderEntity.Company.Notes;
        //            else
        //                NotesAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(NotesAttr);


        //            int CustTypeS = orderEntity.Company.IsCustomer;
        //            if (CustTypeS == 3)
        //                isCorporate = true;

        //            if (isCorporate)
        //            {

        //                XmlAttribute WebAccessAttr = XDoc.CreateAttribute("WebAccessCode");
        //                if (!string.IsNullOrEmpty(orderEntity.Company.WebAccessCode))
        //                    WebAccessAttr.Value = orderEntity.Company.WebAccessCode;
        //                else
        //                    WebAccessAttr.Value = string.Empty;
        //                XCompanyTemp.SetAttributeNode(WebAccessAttr);

        //            }



        //            XmlAttribute AccAtNoAttr = XDoc.CreateAttribute("AccountNo");
        //            if (!string.IsNullOrEmpty(orderEntity.Company.AccountNumber))
        //                AccAtNoAttr.Value = orderEntity.Company.AccountNumber;
        //            else
        //                AccAtNoAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(AccAtNoAttr);

        //            XmlAttribute phneAttr = XDoc.CreateAttribute("Phone");
        //            if (!string.IsNullOrEmpty(orderEntity.Company.PhoneNo))
        //                phneAttr.Value = orderEntity.Company.PhoneNo;
        //            else
        //                phneAttr.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(phneAttr);



        //            int CustType = orderEntity.Company.IsCustomer;
        //            string CustomerType = string.Empty;
        //            if (CustType == 1)
        //                CustomerType = "Retail Customer";
        //            else if (CustType == 2)
        //                CustomerType = "Supplier";
        //            else if (CustType == 3)
        //            {
        //                CustomerType = "Corporate Store";
        //                isCorporate = true;
        //            }
        //            else if (CustType == 4)
        //            {
        //                CustomerType = "Retail Store";
        //            }

        //            XmlAttribute typeAttr = XDoc.CreateAttribute("Type");
        //            typeAttr.Value = CustomerType;
        //            XCompanyTemp.SetAttributeNode(typeAttr);


        //            XmlAttribute comp = XDoc.CreateAttribute("Name");
        //            if (orderEntity.Company != null)
        //                comp.Value = orderEntity.Company.Name;
        //            else
        //                comp.Value = string.Empty;
        //            XCompanyTemp.SetAttributeNode(comp);


        //            XElemRoot.AppendChild(XCompanyTemp);

        //            XmlElement XContactTemp = XDoc.CreateElement("CompanyContact");

        //            XmlAttribute cSkypeAtt = XDoc.CreateAttribute("SkypeID");
        //            if (!string.IsNullOrEmpty(orderEntity.CompanyContact.SkypeId))
        //                cSkypeAtt.Value = orderEntity.CompanyContact.SkypeId;
        //            else
        //                cSkypeAtt.Value = "";
        //            XContactTemp.SetAttributeNode(cSkypeAtt);

        //            XmlAttribute cFaxAtt = XDoc.CreateAttribute("FAX");
        //            if (!string.IsNullOrEmpty(orderEntity.CompanyContact.FAX))
        //                cFaxAtt.Value = orderEntity.CompanyContact.FAX;
        //            else
        //                cFaxAtt.Value = "";
        //            XContactTemp.SetAttributeNode(cFaxAtt);


        //            XmlAttribute cDirectAtt = XDoc.CreateAttribute("DirectLine");
        //            if (!string.IsNullOrEmpty(orderEntity.CompanyContact.HomeTel1))
        //                cDirectAtt.Value = orderEntity.CompanyContact.HomeTel1;
        //            else
        //                cDirectAtt.Value = "";
        //            XContactTemp.SetAttributeNode(cDirectAtt);


        //            XmlAttribute cCellAtt = XDoc.CreateAttribute("CellNumbber");
        //            if (!string.IsNullOrEmpty(orderEntity.CompanyContact.Mobile))
        //                cCellAtt.Value = orderEntity.CompanyContact.Mobile;
        //            else
        //                cCellAtt.Value = "";
        //            XContactTemp.SetAttributeNode(cCellAtt);


        //            XmlAttribute cJobAtt = XDoc.CreateAttribute("JobTitle");
        //            if (!string.IsNullOrEmpty(orderEntity.CompanyContact.JobTitle))
        //                cJobAtt.Value = orderEntity.CompanyContact.JobTitle;
        //            else
        //                cJobAtt.Value = "";
        //            XContactTemp.SetAttributeNode(cJobAtt);



        //            XmlAttribute cEmailAtt = XDoc.CreateAttribute("Email");
        //            cEmailAtt.Value = orderEntity.CompanyContact.Email;
        //            XContactTemp.SetAttributeNode(cEmailAtt);



        //            XmlAttribute contName = XDoc.CreateAttribute("Name");
        //            string FullName = orderEntity.CompanyContact.FirstName + " " + orderEntity.CompanyContact.LastName;
        //            contName.Value = FullName;
        //            XContactTemp.SetAttributeNode(contName);


        //            XElemRoot.AppendChild(XContactTemp);
        //            // att area contact

        //            XmlElement XAddressTemp = XDoc.CreateElement("ShippingAddress");

        //            //  orderEntity.tbl_contacts.tbl_addresses.pos
        //            Address contAddress = orderEntity.CompanyContact.Address;

        //            XmlAttribute CountAttr = XDoc.CreateAttribute("Country");
        //            if (orderEntity.CompanyContact.Address != null)
        //            {
        //                if (contAddress.Country != null)
        //                    CountAttr.Value = contAddress.Country.CountryName;
        //                else
        //                    CountAttr.Value = string.Empty;
        //            }
        //            else
        //            {
        //                CountAttr.Value = string.Empty;
        //            }
        //            XAddressTemp.SetAttributeNode(CountAttr);

        //            XmlAttribute StateAttr = XDoc.CreateAttribute("State");
        //            if (contAddress.State != null)
        //            {
        //                if (!string.IsNullOrEmpty(contAddress.State.StateName))
        //                    StateAttr.Value = contAddress.State.StateName;
        //                else
        //                    StateAttr.Value = string.Empty;
        //            }
        //            else
        //            {
        //                StateAttr.Value = string.Empty;
        //            }
        //            XAddressTemp.SetAttributeNode(StateAttr);


        //            XmlAttribute CityAttr = XDoc.CreateAttribute("City");
        //            if (contAddress != null)
        //            {
        //                if (!string.IsNullOrEmpty(contAddress.City))
        //                    CityAttr.Value = contAddress.City;
        //                else
        //                    CityAttr.Value = string.Empty;
        //            }
        //            else
        //            {
        //                CityAttr.Value = string.Empty;
        //            }
        //            XAddressTemp.SetAttributeNode(CityAttr);

        //            XmlAttribute PostAttr = XDoc.CreateAttribute("PostCode");
        //            if (contAddress != null)
        //            {
        //                if (!string.IsNullOrEmpty(contAddress.PostCode))
        //                    PostAttr.Value = contAddress.PostCode;
        //                else
        //                    PostAttr.Value = string.Empty;
        //            }
        //            else
        //            {
        //                PostAttr.Value = string.Empty;
        //            }
        //            XAddressTemp.SetAttributeNode(PostAttr);

        //            XmlAttribute AddName = XDoc.CreateAttribute("Address");
        //            if (contAddress != null)
        //                AddName.Value = contAddress.Address1 + " " + contAddress.Address2;
        //            XAddressTemp.SetAttributeNode(AddName);

        //            XElemRoot.AppendChild(XAddressTemp);
        //            // att area address

        //            if (orderEntity.BillingAddressId != null)
        //            {
        //                XmlElement XBillingAddressTemp = XDoc.CreateElement("BillingAddress");

        //                Address adress = db.Addesses.Where(a => a.AddressId == orderEntity.BillingAddressId).FirstOrDefault();

        //                XmlAttribute CountaAttr = XDoc.CreateAttribute("Country");
        //                if (adress != null)
        //                {
        //                    if (adress.Country != null)
        //                        CountaAttr.Value = adress.Country.CountryName;
        //                    else
        //                        CountaAttr.Value = string.Empty;
        //                }
        //                else
        //                {
        //                    CountaAttr.Value = string.Empty;
        //                }
        //                XBillingAddressTemp.SetAttributeNode(CountaAttr);


        //                XmlAttribute StateaAttr = XDoc.CreateAttribute("State");
        //                if (adress != null)
        //                {
        //                    if (adress.State != null)
        //                        StateaAttr.Value = adress.State.StateName;
        //                    else
        //                        StateaAttr.Value = string.Empty;
        //                }
        //                else
        //                {
        //                    StateaAttr.Value = string.Empty;
        //                }
        //                XBillingAddressTemp.SetAttributeNode(StateaAttr);


        //                XmlAttribute CityaAttr = XDoc.CreateAttribute("City");
        //                if (adress != null)
        //                {
        //                    if (!string.IsNullOrEmpty(adress.City))
        //                        CityaAttr.Value = adress.City;
        //                    else
        //                        CityaAttr.Value = string.Empty;
        //                }
        //                else
        //                {
        //                    CityaAttr.Value = string.Empty;
        //                }
        //                XBillingAddressTemp.SetAttributeNode(CityaAttr);



        //                XmlAttribute PostaAttr = XDoc.CreateAttribute("PostCode");
        //                if (adress != null)
        //                {
        //                    if (!string.IsNullOrEmpty(adress.PostCode))
        //                        PostaAttr.Value = adress.PostCode;
        //                    else
        //                        PostaAttr.Value = string.Empty;
        //                }
        //                else
        //                {
        //                    PostaAttr.Value = string.Empty;
        //                }
        //                XBillingAddressTemp.SetAttributeNode(PostaAttr);

        //                XmlAttribute AddaName = XDoc.CreateAttribute("Address");
        //                if (adress != null)
        //                    AddaName.Value = adress.Address1 + " " + adress.Address2;
        //                else
        //                    AddaName.Value = string.Empty;
        //                XBillingAddressTemp.SetAttributeNode(AddaName);

        //                XElemRoot.AppendChild(XBillingAddressTemp);
        //            }


        //            XmlElement ItemElemRoot = XDoc.CreateElement("Items");
        //            XElemRoot.AppendChild(ItemElemRoot);

        //            foreach (Item items in orderEntity.Items)
        //            {
        //                if (items.ItemType != 2)
        //                {

        //                    //  string ItemCount = "ItemNo " + itemsCount;
        //                    XmlElement ItemNoElemRoot = XDoc.CreateElement("Item");

        //                    string qtyMarkup = string.Empty;
        //                    if (items.Qty1MarkUp1Value != null)
        //                    {
        //                        qtyMarkup = Convert.ToString(items.Qty1MarkUp1Value);
        //                        if (qtyMarkup.StartsWith("-"))
        //                        {
        //                            XmlAttribute DiscAttr = XDoc.CreateAttribute("Discount");

        //                            DiscAttr.Value = Convert.ToString(items.Qty1MarkUp1Value);
        //                            ItemNoElemRoot.SetAttributeNode(DiscAttr);
        //                        }
        //                    }

        //                    XmlAttribute GrandAttr = XDoc.CreateAttribute("GrandTotal");
        //                    string grandtotal = string.Empty;
        //                    if (items.Qty1GrossTotal != null)
        //                        grandtotal = CurrencySymbol + Convert.ToString(items.Qty1GrossTotal);
        //                    GrandAttr.Value = grandtotal;
        //                    ItemNoElemRoot.SetAttributeNode(GrandAttr);

        //                    XmlAttribute VatAttr = XDoc.CreateAttribute("VAT");
        //                    string VAT = string.Empty;
        //                    if (items.Qty1Tax1Value != null)
        //                        VAT = CurrencySymbol + Convert.ToString(items.Qty1Tax1Value);
        //                    VatAttr.Value = VAT;
        //                    ItemNoElemRoot.SetAttributeNode(VatAttr);

        //                    XmlAttribute netAttr = XDoc.CreateAttribute("NetTotal");
        //                    string nettotal = string.Empty;
        //                    if (items.Qty1NetTotal != null)
        //                        nettotal = CurrencySymbol + Convert.ToString(items.Qty1NetTotal);
        //                    netAttr.Value = nettotal;
        //                    ItemNoElemRoot.SetAttributeNode(netAttr);


        //                    XmlAttribute qtyAttr = XDoc.CreateAttribute("Quantity");
        //                    string qty = string.Empty;
        //                    if (items.Qty1 != null)
        //                        qty = Convert.ToString(items.Qty1);
        //                    qtyAttr.Value = qty;
        //                    ItemNoElemRoot.SetAttributeNode(qtyAttr);


        //                    if (items.Status != null)
        //                    {
        //                        if (items.Status.StatusId != 17)
        //                        {

        //                            XmlAttribute JoAttr = XDoc.CreateAttribute("JobCode");
        //                            JoAttr.Value = items.JobCode;
        //                            ItemNoElemRoot.SetAttributeNode(JoAttr);
        //                        }

        //                    }

        //                    XmlAttribute codeAttr = XDoc.CreateAttribute("ItemCode");
        //                    codeAttr.Value = items.ItemCode;
        //                    ItemNoElemRoot.SetAttributeNode(codeAttr);


        //                    string stat = string.Empty;
        //                    if (items.Status != null)
        //                        stat = items.Status.StatusName;
        //                    else
        //                        stat = "N/A";

        //                    XmlAttribute JobStatus = XDoc.CreateAttribute("Status");
        //                    JobStatus.Value = stat;
        //                    ItemNoElemRoot.SetAttributeNode(JobStatus);


        //                    string ProductFullName = items.ProductName;


        //                    XmlAttribute Prod = XDoc.CreateAttribute("ProductName");
        //                    Prod.Value = ProductFullName;
        //                    ItemNoElemRoot.SetAttributeNode(Prod);

        //                    // ItemXTemp.InnerText = ProductFullName;
        //                    ItemElemRoot.AppendChild(ItemNoElemRoot);
        //                    // attr area items


        //                    XmlElement ItemXTemp = XDoc.CreateElement("JobDescriptions1");
        //                    string JobDescription1 = string.Empty;
        //                    JobDescription1 = items.JobDescriptionTitle1 + ": " + items.JobDescription1;
        //                    if (JobDescription1 != ": ")
        //                        ItemXTemp.InnerText = JobDescription1;
        //                    else
        //                        ItemXTemp.InnerText = "N/A";
        //                    ItemNoElemRoot.AppendChild(ItemXTemp);

        //                    ItemXTemp = XDoc.CreateElement("JobDescriptions2");
        //                    string JobDescription2 = string.Empty;
        //                    JobDescription2 = items.JobDescriptionTitle2 + ": " + items.JobDescription2;
        //                    if (JobDescription2 != ": ")
        //                        ItemXTemp.InnerText = JobDescription2;
        //                    else
        //                        ItemXTemp.InnerText = "N/A";
        //                    ItemNoElemRoot.AppendChild(ItemXTemp);


        //                    ItemXTemp = XDoc.CreateElement("JobDescriptions3");
        //                    string JobDescription3 = string.Empty;
        //                    JobDescription3 = items.JobDescriptionTitle3 + ": " + items.JobDescription3;
        //                    if (JobDescription3 != ": ")
        //                        ItemXTemp.InnerText = JobDescription3;
        //                    else
        //                        ItemXTemp.InnerText = "N/A";
        //                    ItemNoElemRoot.AppendChild(ItemXTemp);



        //                    ItemXTemp = XDoc.CreateElement("JobDescriptions4");
        //                    string JobDescription4 = string.Empty;
        //                    JobDescription4 = items.JobDescriptionTitle4 + ": " + items.JobDescription4;
        //                    if (JobDescription4 != ": ")
        //                        ItemXTemp.InnerText = JobDescription4;
        //                    else
        //                        ItemXTemp.InnerText = "N/A"; ;
        //                    ItemNoElemRoot.AppendChild(ItemXTemp);

        //                    ItemXTemp = XDoc.CreateElement("JobDescriptions5");
        //                    string JobDescription5 = string.Empty;
        //                    JobDescription5 = items.JobDescriptionTitle5 + ": " + items.JobDescription5;
        //                    if (JobDescription5 != ": ")
        //                        ItemXTemp.InnerText = JobDescription5;
        //                    else
        //                        ItemXTemp.InnerText = "N/A"; ;
        //                    ItemNoElemRoot.AppendChild(ItemXTemp);

        //                    ItemXTemp = XDoc.CreateElement("JobDescriptions6");
        //                    string JobDescription6 = string.Empty;
        //                    JobDescription6 = items.JobDescriptionTitle6 + ": " + items.JobDescription6;
        //                    if (JobDescription6 != ": ")
        //                        ItemXTemp.InnerText = JobDescription6;
        //                    else
        //                        ItemXTemp.InnerText = "N/A"; ;
        //                    ItemNoElemRoot.AppendChild(ItemXTemp);

        //                    ItemXTemp = XDoc.CreateElement("JobDescriptions7");
        //                    string JobDescription7 = string.Empty;
        //                    JobDescription7 = items.JobDescriptionTitle7 + ": " + items.JobDescription7;
        //                    if (JobDescription7 != ": ")
        //                        ItemXTemp.InnerText = JobDescription7;
        //                    else
        //                        ItemXTemp.InnerText = "N/A"; ;
        //                    ItemNoElemRoot.AppendChild(ItemXTemp);


        //                    ItemXTemp = XDoc.CreateElement("InvoiceDescription");
        //                    string invDesc = string.Empty;
        //                    invDesc = items.InvoiceDescription;
        //                    if (!string.IsNullOrEmpty(invDesc))
        //                        ItemXTemp.InnerText = invDesc;
        //                    else
        //                        ItemXTemp.InnerText = "N/A";
        //                    ItemNoElemRoot.AppendChild(ItemXTemp);


        //                    if (items.ItemAttachments != null)
        //                    {
        //                        if (items.ItemAttachments.Count > 0)
        //                        {
        //                            XmlElement ItemXTempAttachments = XDoc.CreateElement("Attachments");
        //                            ItemNoElemRoot.AppendChild(ItemXTempAttachments);
        //                            foreach (var v in items.ItemAttachments)
        //                            {
        //                                XmlElement AttachmentXTemp = XDoc.CreateElement("Attachment");
        //                                XmlAttribute AttributeAtt = XDoc.CreateAttribute("Name");
        //                                AttributeAtt.Value = v.FileName;
        //                                AttachmentXTemp.SetAttributeNode(AttributeAtt);
        //                                ItemXTempAttachments.AppendChild(AttachmentXTemp);

        //                            }
        //                        }
        //                    }


        //                    XmlElement SectionsElemRoot = XDoc.CreateElement("ItemSections");
        //                    ItemNoElemRoot.AppendChild(SectionsElemRoot);

        //                    if (items.ItemSections != null)
        //                    {
        //                        if (items.ItemSections.Count > 0)
        //                        {
        //                            foreach (var s in items.ItemSections)
        //                            {
        //                                XmlElement SectionsXTemp = XDoc.CreateElement("Section");
        //                                SectionsElemRoot.AppendChild(SectionsXTemp);


        //                                XmlAttribute platesAttr = XDoc.CreateAttribute("Plates");
        //                                if (s.Side1PlateQty != null)
        //                                {
        //                                    platesAttr.Value = Convert.ToString(s.Side1PlateQty);

        //                                }
        //                                else
        //                                    platesAttr.Value = "N/A";
        //                                SectionsXTemp.SetAttributeNode(platesAttr);



        //                                XmlAttribute incPlate = XDoc.CreateAttribute("IsPlateSupplied");
        //                                if (s.IsPlateSupplied == true)
        //                                {
        //                                    incPlate.Value = "True";

        //                                }
        //                                else
        //                                    incPlate.Value = "False";
        //                                SectionsXTemp.SetAttributeNode(incPlate);



        //                                XmlAttribute incGutter = XDoc.CreateAttribute("IncludeGutter");
        //                                if (s.IncludeGutter == true)
        //                                {
        //                                    incGutter.Value = "True";

        //                                }
        //                                else
        //                                    incGutter.Value = "False";
        //                                SectionsXTemp.SetAttributeNode(incGutter);


        //                                XmlAttribute PaperSupplied = XDoc.CreateAttribute("IsPaperSupplied");
        //                                if (s.IsPaperSupplied == true)
        //                                {
        //                                    PaperSupplied.Value = "True";

        //                                }
        //                                else
        //                                    PaperSupplied.Value = "False";
        //                                SectionsXTemp.SetAttributeNode(PaperSupplied);


        //                                int InkID = 0;
        //                                if (s.PlateInkId != null)
        //                                    InkID = (int)s.PlateInkId;

        //                                string InkString = db.InkPlateSides.Where(x => x.PlateInkId == InkID).Select(a => a.InkTitle).FirstOrDefault();
        //                                XmlAttribute inkAttr = XDoc.CreateAttribute("Ink");
        //                                if (!string.IsNullOrEmpty(InkString))
        //                                    inkAttr.Value = InkString;
        //                                else
        //                                    inkAttr.Value = "N/A";
        //                                SectionsXTemp.SetAttributeNode(inkAttr);



        //                                bool iscustome = false;
        //                                bool isItemSizeCustom = false;

        //                                XmlAttribute itemSizAttr = XDoc.CreateAttribute("ItemSizeCustom");
        //                                if (s.IsItemSizeCustom == true)
        //                                {
        //                                    itemSizAttr.Value = "True";
        //                                    isItemSizeCustom = true;
        //                                }
        //                                else
        //                                    itemSizAttr.Value = "False";
        //                                SectionsXTemp.SetAttributeNode(itemSizAttr);


        //                                XmlAttribute SecSizAttr = XDoc.CreateAttribute("SectionSizeCustom");
        //                                if (s.IsSectionSizeCustom == true)
        //                                {
        //                                    SecSizAttr.Value = "True";
        //                                    iscustome = true;
        //                                }
        //                                else
        //                                    SecSizAttr.Value = "False";
        //                                SectionsXTemp.SetAttributeNode(SecSizAttr);


        //                                if (iscustome)
        //                                {

        //                                    XmlAttribute secHeightAttr = XDoc.CreateAttribute("SectionSizeHeight");
        //                                    if (s.SectionSizeHeight != null)
        //                                        secHeightAttr.Value = Convert.ToString(s.SectionSizeHeight);
        //                                    else
        //                                        secHeightAttr.Value = "";
        //                                    SectionsXTemp.SetAttributeNode(secHeightAttr);

        //                                    XmlAttribute secwidthAttr = XDoc.CreateAttribute("SectionSizeWidth");
        //                                    if (s.SectionSizeWidth != null)
        //                                        secwidthAttr.Value = Convert.ToString(s.SectionSizeWidth);
        //                                    else
        //                                        secwidthAttr.Value = "";
        //                                    SectionsXTemp.SetAttributeNode(secwidthAttr);

        //                                }
        //                                else
        //                                {
        //                                    string PaperString = string.Empty;
        //                                    int PSSID = 0;
        //                                    if (s.SectionSizeId != null)
        //                                        PSSID = (int)s.SectionSizeId;

        //                                    // N Chance

        //                                    var data = from p in db.PaperSizes
        //                                               where p.PaperSizeId == PSSID
        //                                               select new
        //                                               {
        //                                                   p.Name,
        //                                                   p.Height,
        //                                                   p.Width
        //                                               };

        //                                    if (data != null)
        //                                    {
        //                                        foreach (var v in data)
        //                                        {
        //                                            PaperString = v.Name + " " + v.Height + "x" + v.Width;
        //                                        }

        //                                    }
        //                                    XmlAttribute secwidthssAttr = XDoc.CreateAttribute("SectionSize");
        //                                    if (!string.IsNullOrEmpty(PaperString))
        //                                        secwidthssAttr.Value = PaperString;
        //                                    else
        //                                        secwidthssAttr.Value = "";
        //                                    SectionsXTemp.SetAttributeNode(secwidthssAttr);


        //                                }


        //                                if (isItemSizeCustom)
        //                                {
        //                                    XmlAttribute itemSizHeightAttr = XDoc.CreateAttribute("ItemSizeHeight");
        //                                    if (s.ItemSizeHeight != null)
        //                                        itemSizHeightAttr.Value = Convert.ToString(s.ItemSizeHeight);
        //                                    else
        //                                        itemSizHeightAttr.Value = "N/A";
        //                                    SectionsXTemp.SetAttributeNode(itemSizHeightAttr);

        //                                    XmlAttribute itemSizwidthAttr = XDoc.CreateAttribute("ItemSizeWidth");
        //                                    if (s.ItemSizeWidth != null)
        //                                        itemSizwidthAttr.Value = Convert.ToString(s.ItemSizeWidth);
        //                                    else
        //                                        itemSizwidthAttr.Value = "N/A";
        //                                    SectionsXTemp.SetAttributeNode(itemSizwidthAttr);

        //                                }
        //                                else
        //                                {


        //                                    string PaperSString = string.Empty;
        //                                    int PSID = 0;
        //                                    if (s.ItemSizeId != null)
        //                                        PSID = (int)s.ItemSizeId;

        //                                    var data2 = from p in db.PaperSizes
        //                                                where p.PaperSizeId == PSID
        //                                                select new
        //                                                {
        //                                                    p.Name,
        //                                                    p.Height,
        //                                                    p.Width
        //                                                };

        //                                    if (data2 != null)
        //                                    {
        //                                        foreach (var v in data2)
        //                                        {
        //                                            PaperSString = v.Name + " " + v.Height + "x" + v.Width;
        //                                        }

        //                                    }

        //                                    // N chance

        //                                    XmlAttribute itemSizeAttr = XDoc.CreateAttribute("ItemSize");
        //                                    if (!string.IsNullOrEmpty(PaperSString))
        //                                        itemSizeAttr.Value = PaperSString;
        //                                    else
        //                                        itemSizeAttr.Value = "N/A";
        //                                    SectionsXTemp.SetAttributeNode(itemSizeAttr);

        //                                }


        //                                XmlAttribute GuiloAttr = XDoc.CreateAttribute("Guillotin");

        //                                int GID = 0;
        //                                if (s.GuillotineId != null)
        //                                    GID = (int)s.GuillotineId;
        //                                string GuillotinName = db.Machines.Where(m => m.MachineId == GID).Select(z => z.MachineName).FirstOrDefault();
        //                                if (!string.IsNullOrEmpty(GuillotinName))
        //                                    GuiloAttr.Value = GuillotinName;
        //                                else
        //                                    GuiloAttr.Value = string.Empty;
        //                                SectionsXTemp.SetAttributeNode(GuiloAttr);




        //                                XmlAttribute StockAttr = XDoc.CreateAttribute("Stock");

        //                                int StockID = 0;
        //                                if (s.StockItemID1 != null)
        //                                    StockID = (int)s.StockItemID1;
        //                                string StockName = db.StockItems.Where(f => f.StockItemId == StockID).Select(i => i.ItemName).FirstOrDefault();
        //                                if (!string.IsNullOrEmpty(StockName))
        //                                    StockAttr.Value = StockName;
        //                                else
        //                                    StockAttr.Value = string.Empty;
        //                                SectionsXTemp.SetAttributeNode(StockAttr);


        //                                XmlAttribute PressAttr = XDoc.CreateAttribute("Press");
        //                                int PID = 0;
        //                                if (s.PressId != null)
        //                                    PID = (int)s.PressId;

        //                                string PressName = db.Machines.Where(m => m.MachineId == PID).Select(z => z.MachineName).FirstOrDefault();
        //                                if (!string.IsNullOrEmpty(PressName))
        //                                    PressAttr.Value = PressName;
        //                                else
        //                                    PressAttr.Value = string.Empty;
        //                                SectionsXTemp.SetAttributeNode(PressAttr);


        //                                XmlAttribute AttributeSec = XDoc.CreateAttribute("Name");
        //                                AttributeSec.Value = s.SectionName;
        //                                SectionsXTemp.SetAttributeNode(AttributeSec);

        //                                SectionsElemRoot.AppendChild(SectionsXTemp);

        //                                // attr secComes here


        //                                XmlElement SectXTemp = XDoc.CreateElement("Quantities");

        //                                XmlAttribute AttributeQty3 = XDoc.CreateAttribute("Quantity3");
        //                                if (s.Qty3 != null)
        //                                    AttributeQty3.Value = Convert.ToString(s.Qty3);
        //                                else
        //                                    AttributeQty3.Value = "0";
        //                                SectXTemp.SetAttributeNode(AttributeQty3);


        //                                XmlAttribute AttributeQty2 = XDoc.CreateAttribute("Quantity2");
        //                                if (s.Qty2 != null)
        //                                    AttributeQty2.Value = Convert.ToString(s.Qty2);
        //                                else
        //                                    AttributeQty2.Value = "0";
        //                                SectXTemp.SetAttributeNode(AttributeQty2);

        //                                XmlAttribute AttributeQty1 = XDoc.CreateAttribute("Quantity1");
        //                                if (s.Qty1 != null)
        //                                    AttributeQty1.Value = Convert.ToString(s.Qty1);
        //                                else
        //                                    AttributeQty1.Value = "0";
        //                                SectXTemp.SetAttributeNode(AttributeQty1);

        //                                SectionsXTemp.AppendChild(SectXTemp);


        //                                SectXTemp = XDoc.CreateElement("CostCenterTotals");

        //                                double Qty3Total = s.SectionCostcentres.Sum(c => c.Qty3NetTotal ?? 0);
        //                                XmlAttribute AttributeCS3 = XDoc.CreateAttribute("Quantity3");
        //                                AttributeCS3.Value = CurrencySymbol + Convert.ToString(Qty3Total);
        //                                SectXTemp.SetAttributeNode(AttributeCS3);


        //                                double Qty2Total = s.SectionCostcentres.Sum(c => c.Qty2NetTotal ?? 0);
        //                                XmlAttribute AttributeCS2 = XDoc.CreateAttribute("Quantity2");
        //                                AttributeCS2.Value = CurrencySymbol + Convert.ToString(Qty2Total);
        //                                SectXTemp.SetAttributeNode(AttributeCS2);


        //                                double Qty1Total = s.SectionCostcentres.Sum(c => c.Qty1NetTotal ?? 0);
        //                                XmlAttribute AttributeCS1 = XDoc.CreateAttribute("Quantity1");

        //                                AttributeCS1.Value = CurrencySymbol + Convert.ToString(Qty1Total);
        //                                SectXTemp.SetAttributeNode(AttributeCS1);

        //                                SectionsXTemp.AppendChild(SectXTemp);

        //                                SectXTemp = XDoc.CreateElement("Signatures");

        //                                double Sig3 = 0;
        //                                if (s.SimilarSections != null)
        //                                    Sig3 = (double)s.SimilarSections * Qty3Total;

        //                                XmlAttribute AttributeSS3 = XDoc.CreateAttribute("Quantity3");
        //                                AttributeSS3.Value = CurrencySymbol + Convert.ToString(Sig3);

        //                                SectXTemp.SetAttributeNode(AttributeSS3);


        //                                double Sig2 = 0;
        //                                if (s.SimilarSections != null)
        //                                    Sig2 = (double)s.SimilarSections * Qty2Total;
        //                                XmlAttribute AttributeSS2 = XDoc.CreateAttribute("Quantity2");
        //                                AttributeSS2.Value = CurrencySymbol + Convert.ToString(Sig2);
        //                                SectXTemp.SetAttributeNode(AttributeSS2);

        //                                double Sig1 = 0;
        //                                if (s.SimilarSections != null)
        //                                    Sig1 = (double)s.SimilarSections * Qty1Total;
        //                                XmlAttribute AttributeSS1 = XDoc.CreateAttribute("Quantity1");
        //                                AttributeSS1.Value = CurrencySymbol + Convert.ToString(Sig1);

        //                                SectXTemp.SetAttributeNode(AttributeSS1);


        //                                XmlAttribute SimilarSections = XDoc.CreateAttribute("SimilarSignature");
        //                                if (s.SimilarSections != null)
        //                                    SimilarSections.Value = Convert.ToString(s.SimilarSections);
        //                                else
        //                                    SimilarSections.Value = "0";
        //                                SectXTemp.SetAttributeNode(SimilarSections);

        //                                SectionsXTemp.AppendChild(SectXTemp);

        //                                SectXTemp = XDoc.CreateElement("Markups");

        //                                double Mrk1 = 0;
        //                                if (s.BaseCharge1 != null)
        //                                {
        //                                    double Add = Qty1Total + Sig1;
        //                                    Mrk1 = (double)s.BaseCharge1 - Add;
        //                                }

        //                                double Mrk2 = 0;
        //                                if (s.BaseCharge2 != null)
        //                                {
        //                                    double Add = Qty2Total + Sig2;
        //                                    Mrk2 = (double)s.BaseCharge2 - Add;
        //                                }
        //                                double Mrk3 = 0;
        //                                if (s.Basecharge3 != null)
        //                                {
        //                                    double Add = Qty3Total + Sig3;
        //                                    Mrk3 = (double)s.Basecharge3 - Add;
        //                                }


        //                                //string MarkupName3 = ObjectContext.tbl_markup.Where(m => m.MarkUpID == MarkUp3).Select(a => a.MarkUpName).FirstOrDefault();
        //                                XmlAttribute AttributeMarkup3 = XDoc.CreateAttribute("Quantity3");

        //                                AttributeMarkup3.Value = CurrencySymbol + Convert.ToString(Mrk3);

        //                                SectXTemp.SetAttributeNode(AttributeMarkup3);


        //                                XmlAttribute AttributeMarkup2 = XDoc.CreateAttribute("Quantity2");
        //                                AttributeMarkup2.Value = CurrencySymbol + Convert.ToString(Mrk2);

        //                                SectXTemp.SetAttributeNode(AttributeMarkup2);



        //                                XmlAttribute AttributeMarkup1 = XDoc.CreateAttribute("Quantity1");
        //                                AttributeMarkup1.Value = CurrencySymbol + Convert.ToString(Mrk1);

        //                                SectXTemp.SetAttributeNode(AttributeMarkup1);

        //                                SectionsXTemp.AppendChild(SectXTemp);

        //                                SectXTemp = XDoc.CreateElement("SectionSubTotal");


        //                                XmlAttribute AttributesubTotal3 = XDoc.CreateAttribute("Quantity3");
        //                                if (s.Basecharge3 != null)
        //                                    AttributesubTotal3.Value = CurrencySymbol + Convert.ToString(s.Basecharge3);
        //                                else
        //                                    AttributesubTotal3.Value = "0";

        //                                SectXTemp.SetAttributeNode(AttributesubTotal3);

        //                                XmlAttribute AttributesubTotal2 = XDoc.CreateAttribute("Quantity2");
        //                                if (s.BaseCharge2 != null)
        //                                    AttributesubTotal2.Value = CurrencySymbol + Convert.ToString(s.BaseCharge2);
        //                                else
        //                                    AttributesubTotal2.Value = "0";
        //                                SectXTemp.SetAttributeNode(AttributesubTotal2);

        //                                XmlAttribute AttributesubTotal1 = XDoc.CreateAttribute("Quantity1");
        //                                if (s.BaseCharge1 != null)
        //                                    AttributesubTotal1.Value = CurrencySymbol + Convert.ToString(s.BaseCharge1);
        //                                else
        //                                    AttributesubTotal1.Value = "0";

        //                                SectXTemp.SetAttributeNode(AttributesubTotal1);


        //                                SectionsXTemp.AppendChild(SectXTemp);



        //                                if (s.SectionCostcentres != null)
        //                                {
        //                                    List<long> CostCenterIDs = s.SectionCostcentres.Select(v => v.CostCentreId ?? 0).ToList();
        //                                    if (CostCenterIDs != null && CostCenterIDs.Count > 0)
        //                                    {
        //                                        var ProductData = from CC in db.CostCentres
        //                                                          where CostCenterIDs.Contains(CC.CostCentreId)
        //                                                          select new
        //                                                          {
        //                                                              CC.Name,
        //                                                              CC.PreferredSupplierId,
        //                                                              CC.CostCentreId
        //                                                          };


        //                                        XmlElement SectionCostCenterElemRoot = XDoc.CreateElement("SectionCostCenters");
        //                                        SectionsXTemp.AppendChild(SectionCostCenterElemRoot);

        //                                        foreach (var cc in ProductData)
        //                                        {

        //                                            XmlElement SCCNoXTemp = XDoc.CreateElement("CostCenter");
        //                                            // SCCXTemp.InnerText = SupplierName;
        //                                            SectionCostCenterElemRoot.AppendChild(SCCNoXTemp);


        //                                            double nettot = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1NetTotal ?? 0).FirstOrDefault();
        //                                            XmlAttribute NetAttr = XDoc.CreateAttribute("NetTotal");
        //                                            NetAttr.InnerText = CurrencySymbol + Convert.ToString(nettot);
        //                                            SCCNoXTemp.SetAttributeNode(NetAttr);

        //                                            double markupVal = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1MarkUpValue ?? 0).FirstOrDefault();
        //                                            XmlAttribute MarkUpValAttr = XDoc.CreateAttribute("MarkupValue");
        //                                            MarkUpValAttr.Value = CurrencySymbol + Convert.ToString(markupVal);
        //                                            SCCNoXTemp.SetAttributeNode(MarkUpValAttr);



        //                                            int MID = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1MarkUpID ?? 0).FirstOrDefault();
        //                                            string MarkName = db.Markups.Where(m => m.MarkUpId == MID).Select(e => e.MarkUpName).FirstOrDefault();
        //                                            XmlAttribute MarkUpAttr = XDoc.CreateAttribute("Markup");
        //                                            if (!string.IsNullOrEmpty(MarkName))
        //                                                MarkUpAttr.Value = MarkName;
        //                                            else
        //                                                MarkUpAttr.Value = "N/A";
        //                                            SCCNoXTemp.SetAttributeNode(MarkUpAttr);


        //                                            double QtyChrge = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1Charge ?? 0).FirstOrDefault();
        //                                            XmlAttribute PriceAttr = XDoc.CreateAttribute("Price");
        //                                            if (QtyChrge > 0)
        //                                                PriceAttr.Value = CurrencySymbol + Convert.ToString(QtyChrge);
        //                                            else
        //                                                PriceAttr.Value = "0";
        //                                            SCCNoXTemp.SetAttributeNode(PriceAttr);

        //                                            double EstimateTime = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1EstimatedTime).FirstOrDefault();
        //                                            XmlAttribute EstimAttr = XDoc.CreateAttribute("EstimateProductionTime");
        //                                            if (EstimateTime > 0)
        //                                                EstimAttr.Value = Convert.ToString(EstimateTime);
        //                                            else
        //                                                EstimAttr.Value = "0";
        //                                            SCCNoXTemp.SetAttributeNode(EstimAttr);

        //                                            string SupplierName = db.Companies.Where(c => c.CompanyId == cc.PreferredSupplierId).Select(x => x.Name).FirstOrDefault();
        //                                            XmlAttribute SuppNameAttr = XDoc.CreateAttribute("SupplierName");
        //                                            if (!string.IsNullOrEmpty(SupplierName))
        //                                                SuppNameAttr.Value = SupplierName;
        //                                            else
        //                                                SuppNameAttr.Value = "";
        //                                            SCCNoXTemp.SetAttributeNode(SuppNameAttr);


        //                                            XmlAttribute AttributeCostCenter = XDoc.CreateAttribute("Name");
        //                                            AttributeCostCenter.Value = cc.Name;
        //                                            SCCNoXTemp.SetAttributeNode(AttributeCostCenter);

        //                                            SectionCostCenterElemRoot.AppendChild(SCCNoXTemp);


        //                                            string WorkInstruction = s.SectionCostcentres.Where(q => q.CostCentreId == cc.CostCentreId).Select(w => w.Qty1WorkInstructions ?? "N/A").FirstOrDefault();
        //                                            XmlElement SCCXTemp = XDoc.CreateElement("WorkInstruction");
        //                                            SCCXTemp.InnerText = WorkInstruction;
        //                                            SCCNoXTemp.AppendChild(SCCXTemp);

        //                                        }
        //                                    }
        //                                }


        //                            }
        //                        }
        //                    }

        //                }
        //            }
        //            // end of att area items

        //            if (paymentsList != null && paymentsList.Count > 0)
        //            {

        //                XmlElement ItemElemRootPayment = XDoc.CreateElement("Payments");
        //                XElemRoot.AppendChild(ItemElemRootPayment);


        //                foreach (var payment in paymentsList)
        //                {
        //                    XmlElement PaymentXTemp = XDoc.CreateElement("Payment");


        //                    string reference = db.PayPalResponses.Where(o => o.OrderId == iRecordID).Select(p => p.TransactionId ?? "0").FirstOrDefault();
        //                    //select top 1 transactionid from tbl_paypalResponses where orderid = tbl_estimates.estimateid
        //                    XmlAttribute cRef = XDoc.CreateAttribute("Reference");
        //                    if (payment.PaymentMethodId == 1)
        //                        cRef.Value = reference;
        //                    else
        //                    {
        //                        if (payment.ReferenceCode != null)
        //                            cRef.Value = payment.ReferenceCode;
        //                        else
        //                            cRef.Value = "";
        //                    }

        //                    PaymentXTemp.SetAttributeNode(cRef);

        //                    XmlAttribute cAmount = XDoc.CreateAttribute("Amount");
        //                    if (payment.Amount != null)
        //                        cAmount.Value = CurrencySymbol + Convert.ToString(payment.Amount);
        //                    else
        //                        cAmount.Value = "";


        //                    PaymentXTemp.SetAttributeNode(cAmount);



        //                    DateTime dtPayment = new DateTime();
        //                    string paymentDate = string.Empty;
        //                    XmlAttribute cPDate = XDoc.CreateAttribute("Date");
        //                    if (payment.PaymentDate != null)
        //                    {
        //                        dtPayment = Convert.ToDateTime(payment.PaymentDate);
        //                        paymentDate = dtPayment.ToString("dd/MMM/yyyy");
        //                        cPDate.Value = paymentDate;
        //                    }
        //                    else
        //                        cPDate.Value = string.Empty;
        //                    PaymentXTemp.SetAttributeNode(cPDate);


        //                    XmlAttribute cPType = XDoc.CreateAttribute("Type");
        //                    if (payment.PaymentMethodId == 1)
        //                        cPType.Value = "Paypal";
        //                    else
        //                        cPType.Value = "On Account";
        //                    PaymentXTemp.SetAttributeNode(cPType);

        //                    ItemElemRootPayment.AppendChild(PaymentXTemp);


        //                }
        //            }

        //            // att area payment

        //            string sFileName = orderEntity.Order_Code + "_" + "OrderXML.xml";
        //            // FileNamesList.Add(sFileName);
        //            string Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Artworks/" + this.OrganisationId);
        //            if (!Directory.Exists(Path))
        //            {
        //                Directory.CreateDirectory(Path);
        //            }
        //            sFilePath = Path + "/" + sFileName;
        //            XDoc.Save(sFilePath);
        //            db.Configuration.LazyLoadingEnabled = false;
        //            db.Configuration.ProxyCreationEnabled = false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return sFilePath;
        //}




        //public bool MakeOrderArtworkProductionReady(Estimate oOrder)
        //{
        //    try
        //    {




        //        string sOrderID = oOrder.EstimateId.ToString();
        //        string sProductionFolderPath = "MPC_Content/Artworks/" + OrganisationId + "/Production";
        //        string sCustomerID = oOrder.CompanyId.ToString();

        //        return RegenerateTemplateAttachments(sOrderID, sCustomerID, sProductionFolderPath,oOrder);



        //        //clientserv.OpenReadCompleted += new OpenReadCompletedEventHandler(clientserv_OpenReadCompleted);
        //        //clientserv.OpenReadAsync(sParameterURl);


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

        public static string GetArchiveFileName(string OrderCode, string CustomerName, Int64 OrderID)
        {
            string FileName = DateTime.Now.Year.ToString() + "-" + OrderID + "-" + OrderCode + "-" + CustomerName.Replace("&", "").Trim() + ".zip";

            return FileName;
        }
        public static string MakeValidFileName(string name)
        {
            var builder = new StringBuilder();
            var invalid = System.IO.Path.GetInvalidFileNameChars();
            foreach (var cur in name)
            {
                if (!invalid.Contains(cur))
                {
                    builder.Append(cur);
                }
            }
            return builder.ToString();
        }



        #endregion


        /// <summary>
        /// Get Estimates For Item Job Status
        /// </summary>
        public IEnumerable<Estimate> GetEstimatesForItemJobStatus()
        {
            return
                DbSet.Where(
                    est => est.OrganisationId == OrganisationId && (est.StatusId == (int)OrderStatus.InProduction || est.StatusId == (int)OrderStatus.Completed_NotShipped))
                    .ToList();
        }

        /// <summary>
        /// check cookie order is the real login customer order
        /// </summary>
        public bool IsRealCustomerOrder(long orderId, long contactId, long companyId)
        {
            Estimate order = db.Estimates.Where(e => e.EstimateId == orderId).FirstOrDefault();
            if (order != null)
            {
                if (contactId > 0)
                {
                    if (order.CompanyId == companyId && order.ContactId == contactId)
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
                    return true; // me
                }
            }
            else
            {
                return false;
            }
        }
        //public bool RegenerateTemplateAttachments(string estimateId, string customerID, string productionFolderPath,Estimate oOrder)
        //{
        //    try
        //    {
        //        //  Web2Print.BLL.OrderManager orderManager = new Web2Print.BLL.OrderManager();
        //        //  Web2Print.BLL.ProductManager oProdManager = new Web2Print.BLL.ProductManager();
        //        long EstimateId = Convert.ToInt64(estimateId);
        //        long CustomerId = Convert.ToInt64(customerID);
        //        var Order = oOrder;// GetOrderByID(EstimateId);
        //        bool isaddcropMark = GetCropMark(CustomerId);
        //        double bleedsize = db.Organisations.Where(c => c.OrganisationId == OrganisationId).Select(c => c.BleedAreaSize ?? 0).FirstOrDefault();
        //        bool drawBleedArea = false;
        //        bool mutlipageMode = true;
        //        bool hasOverlayPdf = false;
        //        List<Item> OrderItems = GetOrderItems(EstimateId);
        //        if (OrderItems != null)
        //        {
        //            foreach (var i in OrderItems)
        //            {
        //                long TemplateID = i.TemplateId ?? 0;
        //                long ItemID = i.ItemId;
        //                long CustomerID = Convert.ToInt64(customerID);

        //                if (i.TemplateId > 0) // case of templates
        //                {
        //                    var Item = GetItemById(ItemID);
        //                    if (i.isMultipagePDF == true)
        //                    {
        //                        mutlipageMode = true;
        //                    }
        //                    if (i.drawBleedArea == true)
        //                    {
        //                        drawBleedArea = true;
        //                    }
        //                    if (i.printCropMarks == true)
        //                    {
        //                        isaddcropMark = true;
        //                    }

        //                    _TemplateRepository.regeneratePDFs(TemplateID, OrganisationId, isaddcropMark, mutlipageMode, drawBleedArea, bleedsize);
        //                    //LocalTemplateDesigner.TemplateSvcSPClient oLocSvc = new LocalTemplateDesigner.TemplateSvcSPClient();b
        //                    //oLocSvc.regeneratePDFs(TemplateID, isaddcropMark, drawBleedArea, mutlipageMode);



        //                    List<TemplatePage> oPages = new List<TemplatePage>();

        //                        oPages = _TemplatePageRepository.GetTemplatePages(TemplateID);
        //                    //List<TemplateDesignerModelTypesV2.TemplatePages> oPages = null;
        //                    //using (TemplateDesignerV2Entities db = new TemplateDesignerV2Entities())
        //                    //{
        //                    //    db.ContextOptions.LazyLoadingEnabled = false;
        //                    //    oPages = db.TemplatePages.Where(g => g.ProductID == TemplateID).ToList();
        //                    //}

        //                    List<ArtWorkAttatchment> oLstAttachments = _ItemAttachmentRepository.GetItemAttactchmentsForRegenerateTemplateAttachments(ItemID, ".pdf", UploadFileTypes.Artwork);

        //                   // string DesignerPath = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Templates/");
        //                    string DesignerPath = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationId + "/Templates/");

        //                    if (oLstAttachments.Count == 0)  //no attachments already exist, hence a new entry in attachments is required
        //                    {

        //                        //special working for attaching the PDF
        //                        List<ArtWorkAttatchment> uplodedArtWorkList = new List<ArtWorkAttatchment>();
        //                        ArtWorkAttatchment attatcment = null;
        //                        string folderPath = "MPC_Content/Attachments/" + OrganisationId;
        //                        string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/" + folderPath);
        //                        string VirtualFolderPath2 = System.Web.HttpContext.Current.Server.MapPath("~/" + productionFolderPath);


        //                        if (!System.IO.Directory.Exists(virtualFolderPth))
        //                            System.IO.Directory.CreateDirectory(virtualFolderPth);

        //                        if (!System.IO.Directory.Exists(VirtualFolderPath2))
        //                            System.IO.Directory.CreateDirectory(VirtualFolderPath2);

        //                        if (Item.isMultipagePDF == true)
        //                        {
        //                            string fileName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side1", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);
        //                            string overlayName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side1overlay", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);

        //                            string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
        //                            string fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);

        //                            string overlayCompleteAddress = System.IO.Path.Combine(virtualFolderPth, overlayName);
        //                            string overlayCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, overlayName);

        //                            //copying file from original location to attachments location
        //                            System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pages.pdf", fileCompleteAddress, true);
        //                            System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pages.pdf", fileCompleteAddress2, true);
        //                            foreach (var page in oPages)
        //                            {
        //                                if (page.hasOverlayObjects == true)
        //                                    hasOverlayPdf = true;
        //                            }
        //                            if (hasOverlayPdf)
        //                            {
        //                                System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pagesoverlay.pdf", overlayCompleteAddress, true);
        //                                System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pagesoverlay.pdf", overlayCompleteAddress2, true);
        //                                attatcment = new ArtWorkAttatchment();
        //                                attatcment.FileName = overlayName;
        //                                attatcment.FileExtention = ".pdf";
        //                                attatcment.FolderPath = folderPath;
        //                                attatcment.FileTitle = "Side1overlay";
        //                                uplodedArtWorkList.Add(attatcment);
        //                            }

        //                            //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
        //                            string ThumbnailPath = fileCompleteAddress;

        //                            attatcment = new ArtWorkAttatchment();
        //                            attatcment.FileName = fileName;
        //                            attatcment.FileExtention = ".pdf";
        //                            attatcment.FolderPath = folderPath;
        //                            attatcment.FileTitle = "Side1";
        //                            uplodedArtWorkList.Add(attatcment);
        //                        }
        //                        else
        //                        {
        //                            if (Item.isMultipagePDF == true)
        //                            {
        //                                foreach (var page in oPages)
        //                                {
        //                                    if (page.hasOverlayObjects == true)
        //                                        hasOverlayPdf = true;
        //                                }
        //                                string fileName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side1", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);
        //                                string overlayName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side1overlay", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);

        //                                string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
        //                                string fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);

        //                                string overlayCompleteAddress = System.IO.Path.Combine(virtualFolderPth, overlayName);
        //                                string overlayCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, overlayName);

        //                                //copying file from original location to attachments location
        //                                System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pages.pdf", fileCompleteAddress, true);
        //                                System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pages.pdf", fileCompleteAddress2, true);

        //                                if (hasOverlayPdf)
        //                                {
        //                                    System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pagesoverlay.pdf", overlayCompleteAddress, true);
        //                                    System.IO.File.Copy(DesignerPath + TemplateID.ToString() + "/pagesoverlay.pdf", overlayCompleteAddress2, true);
        //                                    attatcment = new ArtWorkAttatchment();
        //                                    attatcment.FileName = overlayName;
        //                                    attatcment.FileExtention = ".pdf";
        //                                    attatcment.FolderPath = folderPath;
        //                                    attatcment.FileTitle = "Side1overlay";
        //                                    uplodedArtWorkList.Add(attatcment);
        //                                }

        //                                //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
        //                                string ThumbnailPath = fileCompleteAddress;

        //                                attatcment = new ArtWorkAttatchment();
        //                                attatcment.FileName = fileName;
        //                                attatcment.FileExtention = ".pdf";
        //                                attatcment.FolderPath = folderPath;
        //                                attatcment.FileTitle = "Side1";
        //                                uplodedArtWorkList.Add(attatcment);
        //                            }
        //                            else
        //                            {
        //                                foreach (var item in oPages)
        //                                {
        //                                    //saving Page1  or Side 1 
        //                                    //string fileName = ItemID.ToString() + " Side" + item.PageNo + ".pdf";

        //                                    string fileName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side" + item.PageNo.ToString(), virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);
        //                                    string overlayName = GetAttachmentFileName(i.ProductCode, Order.Order_Code, i.ItemCode, "Side" + item.PageNo.ToString() + "overlay", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);

        //                                    string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
        //                                    string fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);

        //                                    string overlayCompleteAddress = System.IO.Path.Combine(virtualFolderPth, overlayName);
        //                                    string overlayCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, overlayName);

        //                                    //copying file from original location to attachments location
        //                                    System.IO.File.Copy(DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + ".pdf", fileCompleteAddress, true);
        //                                    System.IO.File.Copy(DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + ".pdf", fileCompleteAddress2, true);

        //                                    if (item.hasOverlayObjects == true)
        //                                    {
        //                                        System.IO.File.Copy(DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + "overlay.pdf", overlayCompleteAddress, true);
        //                                        System.IO.File.Copy(DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + "overlay.pdf", overlayCompleteAddress2, true);
        //                                        attatcment = new ArtWorkAttatchment();
        //                                        attatcment.FileName = overlayName;
        //                                        attatcment.FileExtention = ".pdf";
        //                                        attatcment.FolderPath = folderPath;
        //                                        attatcment.FileTitle = "Side" + item.PageNo.ToString() + "overlay";
        //                                        uplodedArtWorkList.Add(attatcment);
        //                                    }

        //                                    //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
        //                                    string ThumbnailPath = fileCompleteAddress;

        //                                    attatcment = new ArtWorkAttatchment();
        //                                    attatcment.FileName = fileName;
        //                                    attatcment.FileExtention = ".pdf";
        //                                    attatcment.FolderPath = folderPath;
        //                                    attatcment.FileTitle = "Side" + item.PageNo.ToString();
        //                                    uplodedArtWorkList.Add(attatcment);
        //                                    //ProductManager.GenerateThumbnailForPdf(ThumbnailPath, true);
        //                                }
        //                            }

        //                        }
        //                        //creating the attachment the attachment for the first time.
        //                        bool result = CreateUploadYourArtWork(ItemID, CustomerID, uplodedArtWorkList);


        //                        //updating the item with templateID /design
        //                         _ItemRepository.UpdateItem(ItemID, TemplateID);

        //                    }
        //                    else// attachment alredy exists hence we need to updat the existing artwork.
        //                    {
        //                        string folderPath = "MPC_Content/Attachments/" + OrganisationId;
        //                        string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/" + folderPath);
        //                        string VirtualFolderPath2 = System.Web.HttpContext.Current.Server.MapPath("~/" + productionFolderPath);


        //                        if (!System.IO.Directory.Exists(VirtualFolderPath2))
        //                        {
        //                            System.IO.Directory.CreateDirectory(VirtualFolderPath2);
        //                        }

        //                        int index = 0;
        //                        foreach (var oPage in oPages)
        //                        {
        //                            ArtWorkAttatchment oPage1Attachment = oLstAttachments[index];
        //                            index = index + 1;
        //                            //ArtWorkAttatchment oPage1Attachment = oLstAttachments.Where(g => g.FileTitle == oPage.PageName).Single();
        //                            if (oPage1Attachment != null)
        //                            {
        //                                string fileName = oPage1Attachment.FileName + oPage1Attachment.FileExtention;
        //                                string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
        //                                string fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);
        //                                string sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo + ".pdf";

        //                                if (fileName.Contains("overlay"))
        //                                {
        //                                    sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo + "overlay.pdf";

        //                                }

        //                                //System.IO.File.Copy(fileCompleteAddress, fileCompleteAddress2);
        //                                if (File.Exists(sourcePath))
        //                                {
        //                                    System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
        //                                    System.IO.File.Copy(sourcePath, fileCompleteAddress2, true);
        //                                }



        //                                if (oPage.hasOverlayObjects == true)
        //                                {
        //                                    oPage1Attachment = oLstAttachments[index];
        //                                    index = index + 1;
        //                                    if (oPage1Attachment != null)
        //                                    {
        //                                        fileName = oPage1Attachment.FileName;
        //                                        fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
        //                                        fileCompleteAddress2 = System.IO.Path.Combine(VirtualFolderPath2, fileName);
        //                                        sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo + ".pdf";

        //                                        if (fileName.Contains("overlay"))
        //                                        {
        //                                            sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo + "overlay.pdf";

        //                                        }

        //                                        //System.IO.File.Copy(fileCompleteAddress, fileCompleteAddress2);
        //                                        if (File.Exists(sourcePath))
        //                                        {
        //                                            System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
        //                                            System.IO.File.Copy(sourcePath, fileCompleteAddress2, true);
        //                                        }
        //                                    }
        //                                }
        //                                //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
        //                                //string ThumbnailPath = fileCompleteAddress;
        //                                //System.IO.File.WriteAllBytes( System.Web.HttpContext.Current.Server.MapPath(  System.IO.Path.Combine(Web2Print.UI.Common.Utils.GetAppBasePath() +  oPage1Attachment.FolderPath, oPage1Attachment.FileName)), PDFSide1HighRes);
        //                                //ProductManager.GenerateThumbnailForPdf(ThumbnailPath, true);
        //                            }

        //                        }
        //                    }
        //                }
        //                else // case of uplaod images
        //                {
        //                    List<ItemAttachment> ListOfAttachments = _ItemAttachmentRepository.GetItemAttactchments(ItemID);

        //                    string folderPath = "Attachments/" + OrganisationId;// Web2Print.UI.Components.ImagePathConstants.ProductImagesPath + "Attachments/";
        //                    string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("~/" + productionFolderPath);
        //                    string fileSourcePath = System.Web.HttpContext.Current.Server.MapPath("~/" + folderPath);

        //                    if (!System.IO.Directory.Exists(virtualFolderPth))
        //                    {
        //                        System.IO.Directory.CreateDirectory(virtualFolderPth);
        //                    }
        //                    if (!System.IO.Directory.Exists(fileSourcePath))
        //                    {
        //                        System.IO.Directory.CreateDirectory(fileSourcePath);
        //                    }

        //                    //foreach (var oPage in ListOfAttachments)
        //                    //{
        //                    //    string fileName = oPage.FileName;
        //                    //    string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
        //                    //    string sourceFileAdd = System.IO.Path.Combine(fileSourcePath, fileName);
        //                    //    System.IO.File.Copy(sourceFileAdd, fileCompleteAddress, true);

        //                    //}
        //                }
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //return false;
        //        throw ex;
        //    }
        //}

        //public bool GetCropMark(long CustomerID)
        //{

        //    db.Configuration.LazyLoadingEnabled = false;
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var Rec = db.Companies.Where(c => c.CompanyId == CustomerID).FirstOrDefault();

        //    if (Rec != null)
        //    {
        //        return Rec.isAddCropMarks ?? false;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        public Item GetItemById(long itemID)
        {

            return db.Items.Include("itemSections.sectioncostcentres").Where(i => i.ItemId == itemID).FirstOrDefault();

        }

        public bool CreateUploadYourArtWork(long itemID, long customerID, List<ArtWorkAttatchment> yourDesignList)
        {
            bool result = false;

            //CustomerManager customerMgr = new CustomerManager();
            Company customer = GetCustomer(customerID);
            ItemAttachment tblAttatch = null;
            long? contactID = null;
            CompanyContact contact = null;

            try
            {
                if (yourDesignList.Count > 0)
                {
                    if (customer != null && customer.CompanyContacts.Count > 0)
                    {
                        contact = customer.CompanyContacts.ToList()[0];
                        contactID = contact.ContactId;
                    }


                    string folderPath = string.Empty;
                    //Create Additional cost Centeres
                    foreach (ArtWorkAttatchment attatchment in yourDesignList)
                    {
                        folderPath = attatchment.FolderPath.Replace("\\", "//").Replace("//", "/");

                        tblAttatch = PopulueTblItemAttachment(itemID, customerID, contactID, attatchment.FileTitle, attatchment.FileName, attatchment.UploadFileType, attatchment.FileExtention, folderPath);
                        db.ItemAttachments.Add(tblAttatch);
                    }

                    db.SaveChanges();
                    result = true;

                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public Company GetCustomer(long CustomerID)
        {
            try
            {
                //Create Customer
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                return db.Companies.Include("addresses").Include("companycontacts").Where(customer => customer.CompanyId == CustomerID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Estimate GetOrderByIdforXml(long RecordID)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = true;
                db.Configuration.ProxyCreationEnabled = true;
                return db.Estimates.Where(e => e.EstimateId == RecordID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Estimate GetOrderByOrderCode(string Code)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = true;
                db.Configuration.ProxyCreationEnabled = true;
                return db.Estimates.Where(e => e.Order_Code == Code).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private ItemAttachment PopulueTblItemAttachment(long itemID, long customerID, long? contactId, string fileTitle, string fileName, UploadFileTypes type, string fileExtention, string folderPath)
        {
            ItemAttachment attchment = new ItemAttachment
            {
                ItemId = itemID,
                FileTitle = fileTitle,
                FileType = fileExtention,
                Type = type.ToString(),
                FileName = fileName,
                FolderPath = folderPath,
                CompanyId = customerID,
                ContactId = contactId,
                IsApproved = 1,
                UploadDate = DateTime.Now,
                UploadTime = DateTime.Now,
                isFromCustomer = 1

            };

            return attchment;

        }


        #region MISSpeicificFunctions

        //public void regeneratePDFs(long productID, long OrganisationID, bool printCuttingMargins, bool isMultipageProduct, bool drawBleedArea, double bleedAreaSize)
        //{
        //    if (drawBleedArea == false)
        //    {
        //        bleedAreaSize = 0;
        //    }
        //    GenerateTemplatePdf(productID, OrganisationID, printCuttingMargins, false, false, false, bleedAreaSize, isMultipageProduct);

        //}

        //// generate template pdf file called from MIS and webstore 
        //private bool GenerateTemplatePdf(long productID, long OrganisationID, bool printCropMarks, bool printWaterMarks, bool isroundCorners, bool isDrawHiddenObjs, double bleedareaSize, bool isMultipageProduct)
        //{
        //    bool result = false;
        //    try
        //    {

        //        string drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/");
        //        string fontsUrl = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/");//"~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/WebFonts/"
        //        if (!Directory.Exists(drURL + productID))
        //        {
        //            Directory.CreateDirectory(drURL + productID);
        //        }
        //        List<TemplatePage> oTemplatePages = new List<TemplatePage>();
        //        List<TemplateObject> oTemplateObjects = new List<TemplateObject>();
        //        Template objProduct = GetTemplate(productID, out oTemplatePages, out oTemplateObjects);
        //        if (isMultipageProduct)
        //        {
        //            bool hasOverlayObject = false;
        //            byte[] PDFFile = generatePDF(objProduct, oTemplatePages, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, false, OrganisationID, bleedareaSize, true);
        //            //writing the PDF to FS
        //            System.IO.File.WriteAllBytes(drURL + productID + "/pages.pdf", PDFFile);
        //            //gernating 
        //            generatePagePreviewMultiplage(PDFFile, drURL + productID + "/", objProduct.CuttingMargin.Value, 150, isroundCorners);
        //            if (hasOverlayObject)
        //            {
        //                byte[] overlayPDFFile = generatePDF(objProduct, oTemplatePages, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, true, OrganisationID, bleedareaSize, true); ;// generatePDF(objProduct, oTemplatePages, targetFolder, System.Web.Hosting.HostingEnvironment.MapPath("~/Designer/"), false, true, objSettings.printCropMarks, false, out hasOverlayObject, true, true);
        //                System.IO.File.WriteAllBytes(drURL + productID + "/pagesoverlay.pdf", PDFFile);
        //                generatePagePreviewMultiplage(overlayPDFFile, drURL + productID + "/", objProduct.CuttingMargin.Value, 150, isroundCorners);
        //            }
        //            result = true;
        //        }
        //        else
        //        {


        //            foreach (TemplatePage objPage in oTemplatePages)
        //            {
        //                bool hasOverlayObject = false;
        //                byte[] PDFFile = generatePDF(objProduct, objPage, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, false, OrganisationID, bleedareaSize, true);
        //                //writing the PDF to FS
        //                System.IO.File.WriteAllBytes(drURL + productID + "/p" + objPage.PageNo + ".pdf", PDFFile);
        //                //generate and write overlay image to FS 
        //                generatePagePreview(PDFFile, drURL, productID + "/p" + objPage.PageNo, objProduct.CuttingMargin.Value, 150, isroundCorners);
        //                if (hasOverlayObject)
        //                {
        //                    // generate overlay PDF 
        //                    byte[] overlayPDFFile = generatePDF(objProduct, objPage, oTemplateObjects, drURL, fontsUrl, false, isDrawHiddenObjs, printCropMarks, printWaterMarks, out hasOverlayObject, true, OrganisationID, bleedareaSize, true);
        //                    // writing overlay pdf to FS 
        //                    System.IO.File.WriteAllBytes(drURL + productID + "/p" + objPage.PageNo + "overlay.pdf", overlayPDFFile);
        //                    // generate and write overlay image to FS 
        //                    generatePagePreview(overlayPDFFile, drURL, productID + "/p" + objPage.PageNo + "overlay", objProduct.CuttingMargin.Value, 150, isroundCorners);
        //                }
        //            }
        //            result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new MPCException(ex.ToString(), OrganisationID);
        //    }
        //    return result;
        //}


        //public Template GetTemplate(long productID, bool loadPages)
        //{
        //    db.Configuration.LazyLoadingEnabled = false;
        //    Template template = null;
        //    if (loadPages)
        //    {
        //        template = db.Templates.Include("TemplatePages").Where(g => g.ProductId == productID).SingleOrDefault();
        //    }
        //    else
        //    {
        //        template = db.Templates.Where(g => g.ProductId == productID).SingleOrDefault();
        //    }


        //    return template;

        //}
        //// generating multipage pdf 
        //private byte[] generatePDF(Template objProduct, List<TemplatePage> productPages, List<TemplateObject> listTemplateObjects, string ProductFolderPath, string fontPath, bool IsDrawBGText, bool IsDrawHiddenObjects, bool drawCuttingMargins, bool drawWaterMark, out bool hasOverlayObject, bool isoverLayMode, long OrganisationID, double bleedareaSize, bool drawBleedArea)
        //{
        //    hasOverlayObject = false;
        //    Doc doc = new Doc();
        //    try
        //    {
        //        var FontsList = GetFontList();
        //        doc.TopDown = true;
        //        foreach (var objProductPage in productPages)
        //        {
        //            try
        //            {

        //                if (!isoverLayMode)
        //                {
        //                    if (objProductPage.BackGroundType == 1)  //PDF background
        //                    {
        //                        if (File.Exists(ProductFolderPath + objProductPage.BackgroundFileName))
        //                        {
        //                            using (var cPage = new Doc())
        //                            {
        //                                try
        //                                {
        //                                    cPage.Read(ProductFolderPath + objProductPage.BackgroundFileName);
        //                                    doc.Append(cPage);
        //                                    doc.PageNumber = objProductPage.PageNo.Value;
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    throw new Exception("Appedning", ex);
        //                                }
        //                                finally
        //                                {
        //                                    cPage.Dispose();
        //                                }

        //                            }

        //                        }
        //                    }
        //                    else if (objProductPage.BackGroundType == 2) //background color
        //                    {
        //                        //if (objProductPage.Orientation == 1) //standard 
        //                        //{
        //                        doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
        //                        doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

        //                        //}
        //                        //else
        //                        //{
        //                        //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
        //                        //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

        //                        //}
        //                        doc.AddPage();
        //                        doc.PageNumber = objProductPage.PageNo.Value;
        //                        LoadBackColor(ref doc, objProductPage);
        //                    }
        //                    else if (objProductPage.BackGroundType == 3) //background Image
        //                    {

        //                        //if (objProductPage.Orientation == 1) //standard 
        //                        //{
        //                        doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
        //                        doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

        //                        //}
        //                        //else
        //                        //{
        //                        //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
        //                        //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

        //                        //}
        //                        doc.AddPage();
        //                        doc.PageNumber = objProductPage.PageNo.Value;
        //                        LoadBackGroundImage(ref doc, objProductPage, ProductFolderPath);
        //                    }
        //                }
        //                else
        //                {
        //                    //if (objProductPage.Orientation == 1) //standard 
        //                    //{
        //                    doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
        //                    doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;

        //                    //}
        //                    //else
        //                    //{
        //                    //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
        //                    //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

        //                    //}
        //                    doc.AddPage();
        //                    doc.PageNumber = objProductPage.PageNo.Value;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new MPCException(ex.ToString(), OrganisationID);
        //            }


        //            double YFactor = 0;
        //            double XFactor = 0;
        //            // int RowCount = 0;




        //            List<TemplateObject> oParentObjects = null;

        //            if (IsDrawHiddenObjects)
        //            {
        //                if (isoverLayMode == true)
        //                {
        //                    oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
        //                }
        //                else
        //                {
        //                    oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
        //                }
        //            }
        //            else
        //            {
        //                if (isoverLayMode == true)
        //                {
        //                    oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
        //                }
        //                else
        //                {
        //                    oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
        //                }
        //            }
        //            int count = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true)).Count();

        //            if (count > 0)
        //            {
        //                hasOverlayObject = true;
        //            }
        //            foreach (var objObjects in oParentObjects)
        //            {

        //                if (XFactor != objObjects.PositionX)
        //                {
        //                    if (objObjects.ContentString == "")
        //                        YFactor = objObjects.PositionY.Value - 7;
        //                    else
        //                        YFactor = 0;
        //                    XFactor = objObjects.PositionX.Value;
        //                }



        //                if (objObjects.ObjectType == 1 || objObjects.ObjectType == 2 || objObjects.ObjectType == 4)   //|| objObjects.ObjectType == 5
        //                {


        //                    int VAlign = 1, HAlign = 1;

        //                    HAlign = objObjects.Allignment.Value;

        //                    VAlign = objObjects.VAllignment.Value;

        //                    double currentX = objObjects.PositionX.Value, currentY = objObjects.PositionY.Value;


        //                    if (VAlign == 1 || VAlign == 2)
        //                        currentY = objObjects.PositionY.Value + objObjects.MaxHeight.Value;
        //                    bool isTemplateSpot = false;
        //                    if (objProduct.isSpotTemplate.HasValue == true && objProduct.isSpotTemplate.Value == true)
        //                        isTemplateSpot = true;

        //                    AddTextObject(objObjects, objProductPage.PageNo.Value, FontsList, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth.Value, objObjects.MaxHeight.Value, isTemplateSpot, OrganisationID);



        //                }
        //                // object type 13 real state property image 

        //                else if (objObjects.ObjectType == 3 || objObjects.ObjectType == 8 || objObjects.ObjectType == 12 || objObjects.ObjectType == 13) //3 = image and (8 ) =  Company Logo   12  = contactperson logo 13 = real state image placeholders 
        //                {
        //                    //if (objObjects.ObjectType == 8 || objObjects.ObjectType == 12)
        //                    // {
        //                    if (!objObjects.ContentString.Contains("assets/Imageplaceholder") && !objObjects.ContentString.Contains("{{"))
        //                    {
        //                        if (objObjects.ClippedInfo == null)
        //                        {
        //                            if (objObjects.ContentString.Contains(".svg") && !objObjects.ContentString.Contains("{{"))
        //                            {
        //                                GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //                            }
        //                            else
        //                            {
        //                                LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            LoadCroppedImg(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //                        }
        //                    }
        //                    //  }
        //                    //  else
        //                    // {
        //                    //     LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //                    // }
        //                }
        //                else if (objObjects.ObjectType == 5)    //line vector
        //                {
        //                    DrawVectorLine(ref doc, objObjects, objProductPage.PageNo.Value);
        //                }
        //                else if (objObjects.ObjectType == 6)    //line vector
        //                {
        //                    DrawVectorRectangle(ref doc, objObjects, objProductPage.PageNo.Value);
        //                }
        //                else if (objObjects.ObjectType == 7)    //line vector
        //                {
        //                    DrawVectorEllipse(ref doc, objObjects, objProductPage.PageNo.Value);
        //                }
        //                else if (objObjects.ObjectType == 9)    //svg Path
        //                {
        //                    GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //                    //DrawSVGVectorPath(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //                }

        //            }
        //            double TrimBoxSize = 5;
        //            double BleedBoxSize = 0;
        //            if (drawBleedArea)
        //            {
        //                if (System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"] != null) // sytem.web.confiurationmanager
        //                {
        //                    TrimBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"]);
        //                }
        //                doc.SetInfo(doc.Page, "/TrimBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(TrimBoxSize)).ToString());
        //                if (System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"] != null)
        //                {
        //                    double ArtBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"]);
        //                    doc.SetInfo(doc.Page, "/ArtBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(ArtBoxSize)).ToString());

        //                }

        //                if (System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"] != null)
        //                {
        //                    BleedBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"]);
        //                    doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(BleedBoxSize)).ToString());
        //                }
        //                if (bleedareaSize != 0)
        //                {

        //                    doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(bleedareaSize)).ToString());
        //                }
        //            }
        //            //crop marks or margins
        //            if (objProduct.CuttingMargin != null && objProduct.CuttingMargin != 0 && drawCuttingMargins)
        //            {
        //                //doc.CropBox.Height = doc.MediaBox.Height;
        //                //doc.CropBox.Width = doc.MediaBox.Width;


        //                bool isWaterMarkText = objProduct.isWatermarkText ?? true;

        //                int FontID = 0;
        //                var pFont = FontsList.Where(g => g.FontName == "Arial Black").FirstOrDefault();
        //                if (pFont != null)
        //                {
        //                    string path = "";
        //                    if (pFont.FontPath == null)
        //                    {
        //                        // mpc designers fonts or system fonts 
        //                        path = "Organisation" + OrganisationID + "/WebFonts/";//"PrivateFonts/FontFace/";//+ objFont.FontFile; at the root of MPC_content/Webfont
        //                    }
        //                    else
        //                    {  // customer fonts 
        //                        path = pFont.FontPath;
        //                    }
        //                    if (System.IO.File.Exists(fontPath + path + pFont.FontFile + ".ttf"))
        //                        FontID = doc.EmbedFont(fontPath + path + pFont.FontFile + ".ttf");
        //                }

        //                doc.Font = FontID;
        //                double trimboxSizeCuttingLines = 0;
        //                if (TrimBoxSize != 5)
        //                    trimboxSizeCuttingLines = TrimBoxSize;
        //                DrawCuttingLines(ref doc, objProduct.CuttingMargin.Value, 1, objProductPage.PageName, objProduct.TempString, drawCuttingMargins, drawWaterMark, isWaterMarkText, objProduct.PDFTemplateHeight.Value, objProduct.PDFTemplateWidth.Value, TrimBoxSize, BleedBoxSize);
        //            }

        //            if (IsDrawBGText == true)
        //            {
        //                DrawBackgrounText(ref doc);
        //            }
        //            int res = 300;
        //            if (System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"] != null)
        //            {
        //                res = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"]);
        //            }
        //            doc.Rendering.DotsPerInch = res;

        //            //if (ShowHighResPDF == false)
        //            //    opage.Session["PDFFile"] = doc.GetData();
        //            //OpenPage(opage, "Admin/Products/ViewPdf.aspx");
        //        }
        //        return doc.GetData();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ShowPDF", ex);
        //    }
        //    finally
        //    {
        //        doc.Dispose();
        //    }

        //}

        //public List<TemplateFont> GetFontList()
        //{
        //    db.Configuration.LazyLoadingEnabled = false;
        //    return db.TemplateFonts.ToList();

        //}
        //private void LoadBackGroundImage(ref Doc oPdf, TemplatePage oTemplate, string imgPath)
        //{

        //    try
        //    {
        //        oPdf.Rect.Left = oPdf.MediaBox.Left;
        //        oPdf.Rect.Top = oPdf.MediaBox.Top;
        //        oPdf.Rect.Right = oPdf.MediaBox.Right;
        //        oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;
        //        oPdf.PageNumber = oTemplate.PageNo.Value;
        //        oPdf.Layer = oPdf.LayerCount + 1;
        //        XImage oImg = new XImage();
        //        bool bFileExists = false;
        //        string FilePath = imgPath + oTemplate.BackgroundFileName;
        //        bFileExists = System.IO.File.Exists(FilePath);
        //        if (bFileExists)
        //        {
        //            oImg.SetFile(FilePath);
        //            oPdf.AddImageObject(oImg, true);
        //            oPdf.Transform.Reset();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("LoadBackGroundArtWork", ex);
        //    }

        //}
        //private void LoadBackColor(ref Doc oPdf, TemplatePage oTemplate)
        //{
        //    try
        //    {
        //        oPdf.Rect.Left = oPdf.MediaBox.Left;
        //        oPdf.Rect.Top = oPdf.MediaBox.Top;
        //        oPdf.Rect.Right = oPdf.MediaBox.Right;
        //        oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;

        //        oPdf.PageNumber = oTemplate.PageNo.Value;
        //        oPdf.Layer = oPdf.LayerCount + 1;
        //        oPdf.Color.Cyan = oTemplate.ColorC.Value;
        //        oPdf.Color.Magenta = oTemplate.ColorM.Value;
        //        oPdf.Color.Yellow = oTemplate.ColorY.Value;
        //        oPdf.Color.Black = oTemplate.ColorK.Value;
        //        oPdf.FillRect();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("LoadBackColor", ex);
        //    }

        //}

        //private void AddTextObject(TemplateObject ooBject, int PageNo, List<TemplateFont> oFonts, ref Doc oPdf, string Font, double OPosX, double OPosY, double OWidth, double OHeight, bool isTemplateSpot, long organisationID)
        //{

        //    try
        //    {
        //        oPdf.TextStyle.Outline = 0;
        //        oPdf.TextStyle.Strike = false;
        //        oPdf.TextStyle.Bold = false;
        //        oPdf.TextStyle.Italic = false;
        //        oPdf.TextStyle.CharSpacing = 0;
        //        oPdf.PageNumber = PageNo;
        //        if (ooBject.CharSpacing != null)
        //        {
        //            oPdf.TextStyle.CharSpacing = Convert.ToDouble(ooBject.CharSpacing.Value);
        //        }

        //        //    OPosY  =OPosY - (ooBject.FontSize.Value / 21);
        //        double yRPos = 0;
        //        if (oPdf.TopDown == true)
        //            yRPos = oPdf.MediaBox.Height - ooBject.PositionY.Value;
        //        if (ooBject.ColorType.Value == 3)
        //        {
        //            if (isTemplateSpot)
        //            {
        //                if (ooBject.IsSpotColor.HasValue == true && ooBject.IsSpotColor.Value == true)
        //                {
        //                    oPdf.ColorSpace = oPdf.AddColorSpaceSpot(ooBject.SpotColorName, ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString());
        //                    oPdf.Color.Gray = 255;
        //                }
        //                else
        //                {
        //                    oPdf.Color.String = ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString();
        //                }
        //            }
        //            else
        //            {
        //                oPdf.Color.String = ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString();
        //            }

        //            //if (!ooBject.IsColumnNull("Tint"))
        //            if (ooBject.Tint.HasValue)
        //            {
        //                oPdf.Color.Alpha = Convert.ToInt32((100 - ooBject.Tint) * 2.5);
        //            }
        //        }
        //        else if (ooBject.ColorType.Value == 4) // For RGB Colors
        //        {
        //            oPdf.Color.String = ooBject.RColor.ToString() + " " + ooBject.GColor.ToString() + " " + ooBject.BColor.ToString();
        //        }

        //        //for commented code see change book or commit before 3rd march
        //        int FontID = 0;
        //        var pFont = oFonts.Where(g => g.FontName == ooBject.FontName).FirstOrDefault();
        //        string path = "";
        //        if (pFont != null)
        //        {

        //            if (pFont.FontPath == null)
        //            {
        //                // mpc designers fonts or system fonts 
        //                path = "Organisation" + organisationID + "/WebFonts/";//"PrivateFonts/FontFace/";//+ objFont.FontFile; at the root of MPC_content/Webfont
        //            }
        //            else
        //            {  // customer fonts 
        //                path = pFont.FontPath;
        //            }
        //            if (System.IO.File.Exists(Font + path + pFont.FontFile + ".ttf"))
        //                FontID = oPdf.EmbedFont(Font + path + pFont.FontFile + ".ttf");
        //        }

        //        oPdf.Font = FontID;
        //        oPdf.TextStyle.Size = ooBject.FontSize.Value;
        //        if (ooBject.IsUnderlinedText.HasValue)
        //            oPdf.TextStyle.Underline = ooBject.IsUnderlinedText.Value;
        //        oPdf.TextStyle.Bold = ooBject.IsBold.Value;

        //        oPdf.TextStyle.Italic = ooBject.IsItalic.Value;
        //        double linespacing = ooBject.LineSpacing.Value - 1;
        //        linespacing = (linespacing * ooBject.FontSize.Value);
        //        oPdf.TextStyle.LineSpacing = linespacing;
        //        if (ooBject.Allignment == 1)
        //        {
        //            oPdf.HPos = 0.0;
        //        }
        //        else if (ooBject.Allignment == 2)
        //        {
        //            oPdf.HPos = 0.5;
        //        }
        //        else if (ooBject.Allignment == 3)
        //        {
        //            oPdf.HPos = 1.0;
        //        }

        //        if (ooBject.RotationAngle != 0)
        //        {

        //            oPdf.Transform.Reset();
        //            oPdf.Transform.Rotate(360 - ooBject.RotationAngle.Value, OPosX + (OWidth / 2), oPdf.MediaBox.Height - OPosY + (OHeight / 2));
        //        }
        //        List<objTextStyles> styles = new List<objTextStyles>();
        //        if (ooBject.textStyles != null)
        //        {
        //            styles = JsonConvert.DeserializeObject<List<objTextStyles>>(ooBject.textStyles);
        //        }
        //        string StyledHtml = "<p>";
        //        if (styles.Count != 0)
        //        {
        //            styles = styles.OrderBy(g => g.characterIndex).ToList();
        //            for (int i = 0; i < ooBject.ContentString.Length; i++)
        //            {
        //                objTextStyles objStyle = GetStyleByCharIndex(i, styles);
        //                if (objStyle != null && ooBject.ContentString[i] != '\n')
        //                {
        //                    if (objStyle.fontName == null && objStyle.fontSize == null && objStyle.fontStyle == null && objStyle.fontWeight == null && objStyle.textColor == null)
        //                    {
        //                        StyledHtml += ooBject.ContentString[i];
        //                    }
        //                    else
        //                    {
        //                        string toApplyStyle = ooBject.ContentString[i].ToString();
        //                        string fontTag = "<font";
        //                        string fontSize = "";
        //                        string pid = "";
        //                        if (objStyle.fontName != null)
        //                        {
        //                            var oFont = oFonts.Where(g => g.FontName == objStyle.fontName).FirstOrDefault();
        //                            if (System.IO.File.Exists(Font + path + oFont.FontFile + ".ttf"))
        //                                FontID = oPdf.EmbedFont(Font + path + oFont.FontFile + ".ttf");
        //                            // fontTag += " face='" + objStyle.fontName + "' embed= "+ FontID+" ";
        //                            pid = "pid ='" + FontID.ToString() + "' ";
        //                        }
        //                        string lineSpacingString = "";
        //                        if (ooBject.LineSpacing != null)
        //                        {
        //                            lineSpacingString = " linespacing= " + (ooBject.LineSpacing * ooBject.FontSize.Value) + " ";
        //                        }

        //                        if (objStyle.fontSize != null)
        //                        {
        //                            lineSpacingString = " linespacing= " + (ooBject.LineSpacing * Convert.ToInt32(DesignerUtils.PixelToPoint(Convert.ToDouble(objStyle.fontSize)))) + " ";
        //                            fontSize += "<StyleRun fontsize='" + Convert.ToInt32(DesignerUtils.PixelToPoint(Convert.ToDouble(objStyle.fontSize))) + "' " + pid + lineSpacingString + ">";
        //                            fontTag += " fontsize='" + Convert.ToInt32(DesignerUtils.PixelToPoint(Convert.ToDouble(objStyle.fontSize))) + "' " + lineSpacingString + " ";
        //                        }
        //                        if (objStyle.fontStyle != null)
        //                        {
        //                            fontTag += " font-style='" + objStyle.fontStyle + "'";
        //                        }
        //                        if (objStyle.fontWeight != null)
        //                        {
        //                            fontTag += " font-weight='" + objStyle.fontWeight + "'";
        //                        }
        //                        if (objStyle.textColor != null)
        //                        {
        //                            if (objStyle.textCMYK != null)
        //                            {
        //                                string[] vals = objStyle.textCMYK.Split(' ');
        //                                string hexC = Convert.ToInt32(vals[0]).ToString("X");
        //                                if (hexC.Length == 1)
        //                                    hexC = "0" + hexC;
        //                                string hexM = Convert.ToInt32(vals[1]).ToString("X");
        //                                if (hexM.Length == 1)
        //                                    hexM = "0" + hexM;
        //                                string hexY = Convert.ToInt32(vals[2]).ToString("X");
        //                                if (hexY.Length == 1)
        //                                    hexY = "0" + hexY;
        //                                string hexK = Convert.ToInt32(vals[3]).ToString("X");
        //                                if (hexK.Length == 1)
        //                                    hexK = "0" + hexK;
        //                                string hex = "#" + hexC + hexM + hexY + hexK;
        //                                // int csInlineID = oPdf.AddColorSpaceSpot("InlineStyle" + i.ToString(), objStyle.textCMYK);
        //                                //oPdf.Color.Gray = 255;
        //                                // fontTag += " color='#FF' csid=" + csInlineID;
        //                                fontTag += " color='" + hex + "' ";
        //                            }
        //                            else
        //                            {
        //                                fontTag += " color='" + objStyle.textColor + "'";
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (objStyle.fontName != null)
        //                            {
        //                                //   Utilities.CMYKtoRGBConverter objCData = new Utilities.CMYKtoRGBConverter();
        //                                // string colorHex = objCData.getColorHex();
        //                                // int csInlineID = oPdf.AddColorSpaceSpot("InlineStyle" + i.ToString(), ooBject.ColorC.ToString() + " " + ooBject.ColorM.ToString() + " " + ooBject.ColorY.ToString() + " " + ooBject.ColorK.ToString());
        //                                string hexC = Convert.ToInt32(ooBject.ColorC).ToString("X");
        //                                if (hexC.Length == 1)
        //                                    hexC = "0" + hexC;
        //                                string hexM = Convert.ToInt32(ooBject.ColorM).ToString("X");
        //                                if (hexM.Length == 1)
        //                                    hexM = "0" + hexM;
        //                                string hexY = Convert.ToInt32(ooBject.ColorY).ToString("X");
        //                                if (hexY.Length == 1)
        //                                    hexY = "0" + hexY;
        //                                string hexK = Convert.ToInt32(ooBject.ColorK).ToString("X");
        //                                if (hexK.Length == 1)
        //                                    hexK = "0" + hexK;
        //                                string hex = "#" + hexC + hexM + hexY + hexK;

        //                                fontTag += " color='" + hex + "' ";
        //                            }
        //                            //fontTag += " color='" + objStyle.textColor + "'";
        //                        }
        //                        if (fontSize != "")
        //                        {
        //                            toApplyStyle = fontTag + " >" + fontSize + toApplyStyle + "</StyleRun></font>";
        //                        }
        //                        else
        //                        {
        //                            if (objStyle.fontName != null)
        //                            {
        //                                fontSize += "<StyleRun " + pid + lineSpacingString + ">";
        //                                toApplyStyle = fontTag + " >" + fontSize + toApplyStyle + "</StyleRun></font>";
        //                            }
        //                            else
        //                            {
        //                                toApplyStyle = fontTag + " >" + toApplyStyle + "</font>";
        //                            }

        //                        }
        //                        StyledHtml += toApplyStyle;
        //                    }
        //                }
        //                else
        //                {
        //                    StyledHtml += ooBject.ContentString[i];
        //                }
        //            }

        //        }
        //        else
        //        {
        //            StyledHtml += ooBject.ContentString;
        //        }
        //        StyledHtml += "</p>";
        //        string sNewLineNormalized = Regex.Replace(StyledHtml, @"\r(?!\n)|(?<!\r)\n", "<BR>");
        //        sNewLineNormalized = sNewLineNormalized.Replace("  ", "&nbsp;&nbsp;");

        //        if (ooBject.AutoShrinkText == true)
        //        {
        //            oPdf.Rect.Position(OPosX, OPosY);
        //            oPdf.Rect.Resize(OWidth, OHeight);
        //            int theID = oPdf.AddHtml(sNewLineNormalized);
        //            while (oPdf.Chainable(theID))
        //            {
        //                oPdf.Delete(theID);
        //                oPdf.FontSize--;
        //                oPdf.Rect.String = oPdf.Rect.String; // reset Doc.Pos without having to save its initial value
        //                theID = oPdf.AddHtml(sNewLineNormalized);
        //            }
        //        }
        //        else
        //        {
        //            oPdf.Rect.Position(OPosX, OPosY);
        //            oPdf.Rect.Resize(OWidth, OHeight);
        //            oPdf.AddHtml(sNewLineNormalized);
        //        }
        //        oPdf.Transform.Reset();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ADDSingleLineText", ex);
        //    }

        //}

        //private objTextStyles GetStyleByCharIndex(int index, List<objTextStyles> objStyles)
        //{
        //    foreach (var obj in objStyles)
        //    {
        //        if (obj.characterIndex == index.ToString())
        //        {
        //            return obj;
        //        }
        //    }
        //    return null;
        //}

        //private void LoadImage(ref Doc oPdf, TemplateObject oObject, string logoPath, int PageNo)
        //{
        //    logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content");
        //    XImage oImg = new XImage();
        //    Bitmap img = null;
        //    try
        //    {
        //        oPdf.PageNumber = PageNo;


        //        bool bFileExists = false;
        //        string FilePath = string.Empty;
        //        if (oObject.ObjectType == 8 || oObject.ObjectType == 12)
        //        {
        //            logoPath = ""; //since path is already in filenm
        //            string[] vals;
        //            FilePath = "";
        //            if (oObject.ContentString.ToLower().Contains("/mpc_content/"))
        //            {
        //                vals = oObject.ContentString.ToLower().Split(new string[] { "/mpc_content/" }, StringSplitOptions.None);
        //                FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/" + vals[vals.Length - 1]);
        //                //FilePath = logoPath + oObject.ContentString;
        //                bFileExists = System.IO.File.Exists((FilePath));
        //            }
        //            else
        //            {
        //                // dont show any thing becuase path will contain dummy placeholder image
        //            }


        //        }
        //        else
        //        {
        //            if (oObject.ContentString != "")
        //                FilePath = oObject.ContentString;
        //            FilePath = logoPath + "/" + FilePath;
        //            bFileExists = System.IO.File.Exists(FilePath);
        //        }
        //        //  else
        //        //     filNm = oobject.LogoName;

        //        if (bFileExists)
        //        {

        //            //  oImg.SetFile(FilePath);

        //            var posY = oObject.PositionY + oObject.MaxHeight;

        //            oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
        //            oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);

        //            if (oObject.RotationAngle != null)
        //            {


        //                if (oObject.RotationAngle != 0)
        //                {
        //                    oPdf.Transform.Reset();
        //                    oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);


        //                }


        //            }
        //            if (oObject.Opacity != null && oObject.Opacity != 1)
        //            {
        //                img = new Bitmap(System.Drawing.Image.FromFile(FilePath, true));

        //                if (oObject.Opacity != null)
        //                {
        //                    // float opacity =float.Parse( oObject.Tint.ToString()) /100;
        //                    if (oObject.Opacity != 1)
        //                    {
        //                        img = DesignerUtils.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
        //                    }
        //                }
        //                oImg.SetData(DesignerSvgParser.ImageToByteArraybyImageConverter(img));
        //                int id = oPdf.AddImageObject(oImg, true);
        //            }
        //            else
        //            {
        //                // XImage oImgx = new XImage();
        //                oImg.SetFile(FilePath);
        //                oPdf.AddImageObject(oImg, true);
        //            }
        //            //oPdf.FrameRect();


        //            //if (oObject.Tint != null)
        //            //{
        //            //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
        //            //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
        //            //}

        //            oPdf.Transform.Reset();
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("LoadImage", ex);
        //    }
        //    finally
        //    {
        //        oImg.Dispose();
        //        if (img != null)
        //            img.Dispose();
        //    }
        //}

        //private void LoadCroppedImg(ref Doc oPdf, TemplateObject oObject, string logoPath, int PageNo)
        //{


        //    logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content");
        //    XImage oImg = new XImage();
        //    Bitmap img = null;
        //    try
        //    {
        //        oPdf.PageNumber = PageNo;
        //        bool bFileExists = false;
        //        string FilePath = string.Empty;
        //        if (oObject.ObjectType == 8 || oObject.ObjectType == 12)
        //        {
        //            logoPath = ""; //since path is already in filenm
        //            string[] vals;
        //            FilePath = "";
        //            if (oObject.ContentString.ToLower().Contains("/mpc_content/"))
        //            {
        //                vals = oObject.ContentString.ToLower().Split(new string[] { "/mpc_content/" }, StringSplitOptions.None);
        //                FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/" + vals[vals.Length - 1]);
        //                //   FilePath = logoPath + oObject.ContentString;
        //                bFileExists = System.IO.File.Exists((FilePath));
        //            }
        //            else
        //            {
        //                // dont show any thing becuase path will contain dummy placeholder image
        //            }

        //        }
        //        else
        //        {
        //            if (oObject.ContentString != "")
        //                FilePath = oObject.ContentString;
        //            FilePath = logoPath + "/" + FilePath;
        //            bFileExists = System.IO.File.Exists(FilePath);
        //        }
        //        if (bFileExists)
        //        {


        //            var posY = oObject.PositionY + oObject.MaxHeight;
        //            oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
        //            oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);

        //            if (oObject.RotationAngle != null)
        //            {
        //                if (oObject.RotationAngle != 0)
        //                {
        //                    oPdf.Transform.Reset();
        //                    oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);
        //                }
        //            }
        //            XmlDocument xmlDoc = new XmlDocument();
        //            xmlDoc.LoadXml(oObject.ClippedInfo);
        //            string xpath = "Cropped";
        //            var nodes = xmlDoc.SelectNodes(xpath);
        //            double sx, sy, swidth, sheight;
        //            foreach (XmlNode childrenNode in nodes)
        //            {
        //                sx = Convert.ToDouble(childrenNode.SelectSingleNode("sx").InnerText);
        //                sy = Convert.ToDouble(childrenNode.SelectSingleNode("sy").InnerText);
        //                swidth = Convert.ToDouble(childrenNode.SelectSingleNode("swidth").InnerText);
        //                sheight = Convert.ToDouble(childrenNode.SelectSingleNode("sheight").InnerText);
        //                oImg.Selection.Inset(sx, sy);
        //                oImg.Selection.Height = sheight;
        //                oImg.Selection.Width = swidth;
        //            }
        //            if (oObject.Opacity != null && oObject.Opacity != 1)
        //            {
        //                img = new Bitmap(System.Drawing.Image.FromFile(FilePath, true));
        //                if (oObject.Opacity != null)
        //                {
        //                    if (oObject.Opacity != 1)
        //                    {
        //                        img = DesignerUtils.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
        //                    }
        //                }
        //                oImg.SetData(DesignerSvgParser.ImageToByteArraybyImageConverter(img));
        //                foreach (XmlNode childrenNode in nodes)
        //                {
        //                    sx = Convert.ToDouble(childrenNode.SelectSingleNode("sx").InnerText);
        //                    sy = Convert.ToDouble(childrenNode.SelectSingleNode("sy").InnerText);
        //                    swidth = Convert.ToDouble(childrenNode.SelectSingleNode("swidth").InnerText);
        //                    sheight = Convert.ToDouble(childrenNode.SelectSingleNode("sheight").InnerText);
        //                    oImg.Selection.Inset(sx, sy);
        //                    oImg.Selection.Height = sheight;
        //                    oImg.Selection.Width = swidth;
        //                }
        //                int id = oPdf.AddImageObject(oImg, true);
        //            }
        //            else
        //            {
        //                if (System.IO.Path.GetExtension(FilePath).ToLower().Contains(".tif"))
        //                {
        //                    oPdf.AddImageFile(FilePath);
        //                }
        //                else
        //                {
        //                    oImg.SetFile(FilePath);
        //                    foreach (XmlNode childrenNode in nodes)
        //                    {
        //                        sx = Convert.ToDouble(childrenNode.SelectSingleNode("sx").InnerText);
        //                        sy = Convert.ToDouble(childrenNode.SelectSingleNode("sy").InnerText);
        //                        swidth = Convert.ToDouble(childrenNode.SelectSingleNode("swidth").InnerText);
        //                        sheight = Convert.ToDouble(childrenNode.SelectSingleNode("sheight").InnerText);
        //                        oImg.Selection.Inset(sx, sy);
        //                        oImg.Selection.Height = sheight;
        //                        oImg.Selection.Width = swidth;
        //                    }
        //                    oPdf.AddImageObject(oImg, true);
        //                }
        //            }

        //            oPdf.Transform.Reset();
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("LoadImage", ex);
        //    }
        //    finally
        //    {
        //        oImg.Dispose();
        //        if (img != null)
        //            img.Dispose();
        //    }
        //}

        //private void DrawVectorLine(ref Doc oPdf, TemplateObject oObject, int PageNo)
        //{

        //    try
        //    {
        //        oPdf.PageNumber = PageNo;

        //        if (oObject.ColorType == 3)
        //        {
        //            if (oObject.IsSpotColor == true)
        //            {
        //                oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
        //            }
        //            oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();
        //            //if (!ooBject.IsColumnNull("Tint"))
        //            oPdf.Color.Alpha = Convert.ToInt32((oObject.Tint) * 2.55);
        //        }
        //        else if (oObject.ColorType == 4) // For RGB Colors
        //        {
        //            oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
        //        }


        //        oPdf.Width = oObject.MaxHeight.Value;
        //        oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value);
        //        oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


        //        if (oObject.RotationAngle != null)
        //        {

        //            if (oObject.RotationAngle != 0)
        //            {
        //                oPdf.Transform.Reset();
        //                oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value);
        //            }
        //        }

        //        // oPdf.AddImageObject(oImg,false);
        //        //oPdf.AddImage ((oImg);
        //        oPdf.AddLine(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value / 2, oObject.PositionX.Value + oObject.MaxWidth.Value, oObject.PositionY.Value + oObject.MaxHeight.Value / 2);
        //        oPdf.Transform.Reset();

        //    }

        //    catch (Exception ex)
        //    {
        //        throw new Exception("DrawVectorLine", ex);
        //    }

        //}

        ////vector rectangle drawing in PDF
        //private void DrawVectorRectangle(ref Doc oPdf, TemplateObject oObject, int PageNo)
        //{

        //    try
        //    {
        //        oPdf.PageNumber = PageNo;

        //        if (oObject.ColorType == 3)
        //        {
        //            if (oObject.IsSpotColor == true)
        //            {
        //                oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
        //            }
        //            oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();
        //            if (oObject.Opacity != null)
        //                oPdf.Color.Alpha = Convert.ToInt32((100 * oObject.Opacity) * 2.55);
        //            //if (!ooBject.IsColumnNull("Tint"))
        //            //oPdf.Color.Alpha = 0;//Convert.ToInt32((100 - oObject.Tint) * 2.5);
        //        }
        //        else if (oObject.ColorType == 4) // For RGB Colors
        //        {
        //            oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
        //        }


        //        //oPdf.Width = oobject.MaxHeight;
        //        oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value);
        //        oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


        //        if (oObject.RotationAngle != null)
        //        {

        //            if (oObject.RotationAngle != 0)
        //            {
        //                oPdf.Transform.Reset();
        //                oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value - oObject.MaxHeight.Value / 2);
        //            }


        //        }

        //        int id = oPdf.FillRect();
        //        //if (oObject.Tint != null)
        //        //{
        //        //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
        //        //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
        //        //}
        //        //oPdf.Addre(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight / 2);
        //        oPdf.Transform.Reset();

        //    }

        //    catch (Exception ex)
        //    {
        //        throw new Exception("DrawVectorRectangle", ex);
        //    }

        //}

        ////vector Ellipse drawing in PDF
        //private void DrawVectorEllipse(ref Doc oPdf, TemplateObject oObject, int PageNo)
        //{

        //    try
        //    {
        //        oPdf.PageNumber = PageNo;

        //        if (oObject.ColorType == 3)
        //        {
        //            if (oObject.IsSpotColor == true)
        //            {
        //                oPdf.ColorSpace = oPdf.AddColorSpaceSpot(oObject.SpotColorName, oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString());
        //            }
        //            oPdf.Color.String = oObject.ColorC.ToString() + " " + oObject.ColorM.ToString() + " " + oObject.ColorY.ToString() + " " + oObject.ColorK.ToString();

        //            if (oObject.Opacity != null)
        //                oPdf.Color.Alpha = Convert.ToInt32((100 * oObject.Opacity) * 2.5);
        //        }
        //        else if (oObject.ColorType == 4) // For RGB Colors
        //        {
        //            oPdf.Color.String = oObject.RColor.ToString() + " " + oObject.GColor.ToString() + " " + oObject.BColor.ToString();
        //        }




        //        //oPdf.Width = oobject.MaxHeight;
        //        oPdf.Rect.Position(oObject.PositionX.Value, oObject.PositionY.Value + oObject.MaxHeight.Value);
        //        oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);


        //        if (oObject.RotationAngle != null)
        //        {

        //            if (oObject.RotationAngle != 0)
        //            {
        //                oPdf.Transform.Reset();
        //                oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - oObject.PositionY.Value - oObject.MaxHeight.Value / 2);
        //            }


        //        }

        //        int id = oPdf.FillRect(oObject.MaxWidth.Value / 2, oObject.MaxHeight.Value / 2);
        //        //if (oObject.Tint != null)
        //        //{
        //        //    ImageLayer im = (ImageLayer)oPdf.ObjectSoup[id];
        //        //    im.PixMap.SetAlpha(Convert.ToInt32(( oObject.Tint) * 2.5));
        //        //}
        //        //oPdf.Addre(oobject.PositionX, oobject.PositionY + oobject.MaxHeight / 2, oobject.PositionX + oobject.MaxWidth, oobject.PositionY + oobject.MaxHeight / 2);
        //        oPdf.Transform.Reset();

        //    }

        //    catch (Exception ex)
        //    {
        //        throw new Exception("DrawVectorEllipse", ex);
        //    }

        //}

        //private void GetSVGAndDraw(ref Doc oPdf, TemplateObject oObject, string logoPath, int PageNo)
        //{
        //    logoPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content");
        //    //XImage oImg = new XImage();
        //    //Bitmap img = null;
        //    try
        //    {
        //        oPdf.PageNumber = PageNo;
        //        bool bFileExists = false;
        //        string FilePath = string.Empty;
        //        if (oObject.ContentString != "")
        //            FilePath = oObject.ContentString;
        //        FilePath = logoPath + "/" + FilePath;
        //        bFileExists = System.IO.File.Exists(FilePath);
        //        if (bFileExists)
        //        {
        //            //DesignerSvgParser.MaximumSize = new Size(Convert.ToInt32(oObject.MaxWidth), Convert.ToInt32(oObject.MaxHeight));
        //            // img = DesignerSvgParser.GetBitmapFromSVG(FilePath, oObject.ColorHex);
        //            //if (oObject.Opacity != null)
        //            //{
        //            //    // float opacity =float.Parse( oObject.Tint.ToString()) /100;
        //            //    if (oObject.Opacity != 1)
        //            //    {
        //            //        img = DesignerUtils.ChangeOpacity(img, float.Parse(oObject.Opacity.ToString()));
        //            //    }
        //            //}
        //            //oImg.SetData(DesignerSvgParser.ImageToByteArraybyImageConverter(img));

        //            var posY = oObject.PositionY + oObject.MaxHeight;

        //            oPdf.Rect.Position(oObject.PositionX.Value, posY.Value);
        //            oPdf.Rect.Resize(oObject.MaxWidth.Value, oObject.MaxHeight.Value);

        //            if (oObject.RotationAngle != null)
        //            {
        //                if (oObject.RotationAngle != 0)
        //                {
        //                    oPdf.Transform.Reset();
        //                    oPdf.Transform.Rotate(360 - oObject.RotationAngle.Value, oObject.PositionX.Value + oObject.MaxWidth.Value / 2, oPdf.MediaBox.Height - posY.Value + oObject.MaxHeight.Value / 2);
        //                }
        //            }

        //            // }


        //            //  oBook = new tblBook();
        //            // oBook.bookid = Guid.Empty;

        //            //  oBook.book_title = BookNode.Attributes["book_title"].Value;


        //            //int id = oPdf.AddImageObject(oImg, true);
        //            //oPdf.Transform.Reset();
        //            oPdf.HtmlOptions.HideBackground = true;
        //            oPdf.HtmlOptions.Engine = EngineType.Gecko;

        //            float width = (float)oObject.MaxWidth.Value, height = (float)oObject.MaxHeight.Value;
        //            // string URl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/MPC_Content" + oObject.ContentString;
        //            ////int id = oPdf.AddImageUrl(URl);
        //            List<svgColorData> styles = new List<svgColorData>();
        //            if (oObject.textStyles != null)
        //            {
        //                styles = JsonConvert.DeserializeObject<List<svgColorData>>(oObject.textStyles);
        //            }
        //            string file = DesignerSvgParser.UpdateSvg(FilePath, height, width, styles);//
        //            string html = File.ReadAllText(file);
        //            html = "<html><head><style>html, body { margin:0; padding:0; overflow:hidden } svg { position:fixed; top:0; left:0; height:100%; width:100% }</style></head><body  style='  padding: 0px 0px 0px 0px;margin: 0px 0px 0px 0px;'>" + html + "</body></html>";
        //            oPdf.AddImageHtml(html);
        //            oPdf.Transform.Reset();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("LoadSvg", ex);
        //    }
        //    finally
        //    {
        //        //  oImg.Dispose();
        //        // if (img != null)
        //        //  img.Dispose();
        //    }
        //}


        //private void DrawCuttingLines(ref Doc oPdf, double mrg, int PageNo, string pageName, string waterMarkTxt, bool drawCuttingMargins, bool drawWatermark, bool isWaterMarkText, double pdfTemplateHeight, double pdfTemplateWidth, double trimBoxSize, double bleedOffset)
        //{
        //    try
        //    {
        //        oPdf.Color.String = "100 100 100 100";

        //        if (trimBoxSize != 0) // for digital central 
        //        {
        //            mrg = DesignerUtils.MMToPoint(trimBoxSize);
        //        }
        //        double offset = 0;
        //        if (bleedOffset != 0) // for digital central 
        //        {
        //            offset = DesignerUtils.MMToPoint(bleedOffset);
        //        }
        //        //mrg = 5.98110236159; // global change on request of digital central to make crop marks 2.11 mm
        //        oPdf.Layer = oPdf.LayerCount - 1;
        //        oPdf.PageNumber = PageNo;
        //        //oPdf.Width = 0.5;
        //        oPdf.Width = 0.25;
        //        oPdf.Rect.Left = oPdf.MediaBox.Left;
        //        oPdf.Rect.Top = oPdf.MediaBox.Top;
        //        oPdf.Rect.Right = oPdf.MediaBox.Right;
        //        oPdf.Rect.Bottom = oPdf.MediaBox.Bottom;
        //        double pgWidth = oPdf.MediaBox.Width;
        //        double pgHeight = oPdf.MediaBox.Height;
        //        for (int i = 1; i <= oPdf.PageCount; i++)
        //        {
        //            oPdf.PageNumber = i;
        //            if (drawCuttingMargins)
        //            {
        //                oPdf.Layer = 1;
        //                oPdf.AddLine(mrg, 0, mrg, mrg - offset);
        //                oPdf.AddLine(0, mrg, mrg - offset, mrg);
        //                oPdf.AddLine(oPdf.MediaBox.Width - mrg, 0, oPdf.MediaBox.Width - mrg, mrg - offset);
        //                oPdf.AddLine(oPdf.MediaBox.Width - mrg + offset, mrg, oPdf.MediaBox.Width, mrg);
        //                oPdf.AddLine(0, oPdf.MediaBox.Height - mrg, mrg - offset, oPdf.MediaBox.Height - mrg);
        //                oPdf.AddLine(mrg, oPdf.MediaBox.Height - mrg + offset, mrg, oPdf.MediaBox.Height);
        //                oPdf.AddLine(oPdf.MediaBox.Width - mrg + offset, oPdf.MediaBox.Height - mrg, oPdf.MediaBox.Width, oPdf.MediaBox.Height - mrg); //----
        //                oPdf.AddLine(oPdf.MediaBox.Width - mrg, oPdf.MediaBox.Height - mrg + offset, oPdf.MediaBox.Width - mrg, oPdf.MediaBox.Height); //|

        //                // adding date time stamp
        //                oPdf.Layer = 1;
        //                oPdf.TextStyle.Outline = 0;
        //                oPdf.TextStyle.Strike = false;
        //                // oPdf.TextStyle.Bold = true;
        //                oPdf.TextStyle.Italic = false;
        //                oPdf.TextStyle.CharSpacing = 0;
        //                oPdf.TextStyle.Size = 6;
        //                oPdf.Rect.Position(((pgWidth / 2) - 20), pgHeight + 5);
        //                oPdf.Rect.Resize(200, 10);
        //                oPdf.AddHtml("" + pageName + " " + DateTime.Now.ToString());
        //                oPdf.Transform.Reset();
        //            }
        //            // water mark 
        //            if (drawWatermark)
        //            {
        //                if (waterMarkTxt != null && waterMarkTxt != "")
        //                {
        //                    if (isWaterMarkText)
        //                    {
        //                        oPdf.Color.String = "16 12 13 0";
        //                        oPdf.Color.Alpha = 220;
        //                        oPdf.TextStyle.Size = 30;
        //                        oPdf.Layer = 1;
        //                        oPdf.HPos = 0.5;
        //                        oPdf.VPos = 0.5;
        //                        oPdf.TextStyle.Outline = 2;
        //                        oPdf.Rect.Position(0, oPdf.MediaBox.Height);
        //                        oPdf.Rect.Resize(oPdf.MediaBox.Width, oPdf.MediaBox.Height);
        //                        // oPdf.FrameRect();
        //                        oPdf.Transform.Reset();
        //                        oPdf.Transform.Rotate(45, oPdf.MediaBox.Width / 2, oPdf.MediaBox.Height / 2);
        //                        oPdf.AddHtml(waterMarkTxt);
        //                        oPdf.Transform.Reset();
        //                    }
        //                    else
        //                    {
        //                        string FilePath = string.Empty;
        //                        XImage oImg = new XImage();
        //                        System.Drawing.Image objImage = null;
        //                        try
        //                        {
        //                            oPdf.PageNumber = i;


        //                            bool bFileExists = false;
        //                            FilePath = waterMarkTxt;
        //                            bFileExists = System.IO.File.Exists(FilePath);

        //                            if (bFileExists)
        //                            {
        //                                objImage = System.Drawing.Image.FromFile(FilePath);
        //                                oImg.SetFile(FilePath);
        //                                double height = DesignerUtils.PixelToPoint(objImage.Height);
        //                                double width = DesignerUtils.PixelToPoint(objImage.Width);
        //                                if (height > pdfTemplateHeight)
        //                                {
        //                                    height = pdfTemplateHeight;
        //                                }
        //                                if (width > pdfTemplateWidth)
        //                                {
        //                                    width = pdfTemplateWidth;
        //                                }

        //                                double posX = (oPdf.MediaBox.Width - width) / 2;
        //                                double posY = (oPdf.MediaBox.Height - height) / 2 + height;


        //                                oPdf.Layer = 1;
        //                                oPdf.Rect.Position(posX, posY);
        //                                oPdf.Rect.Resize(width, height);
        //                                oPdf.Transform.Rotate(45, oPdf.MediaBox.Width / 2, oPdf.MediaBox.Height / 2);

        //                                oPdf.AddImageObject(oImg, true);
        //                                oPdf.Transform.Reset();
        //                            }


        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            throw new Exception("LoadWaterMarkImage", ex);
        //                        }
        //                        finally
        //                        {
        //                            oImg.Dispose();
        //                            if (objImage != null)
        //                                objImage.Dispose();
        //                        }
        //                        // image
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("DrawCuttingLine", ex);
        //    }
        //}

        //private void DrawBackgrounText(ref Doc oPdf)
        //{
        //    int FontID = oPdf.AddFont("Arial");
        //    for (int i = 1; i <= oPdf.PageCount; i++)
        //    {
        //        oPdf.PageNumber = i;
        //        oPdf.Color.String = "211 211 211";
        //        //oPdf.Color.Alpha = 60;
        //        oPdf.Font = FontID;
        //        oPdf.TextStyle.Size = 40;
        //        //oPdf.TextStyle.CharSpacing = 2;
        //        //oPdf.TextStyle.Bold = true;
        //        //oPdf.TextStyle.Italic = false;
        //        oPdf.HPos = 0.5;
        //        oPdf.VPos = 0.5;
        //        oPdf.TextStyle.Outline = 2;
        //        oPdf.Rect.Position(0, oPdf.MediaBox.Height);
        //        oPdf.Rect.Resize(oPdf.MediaBox.Width, oPdf.MediaBox.Height);
        //        // oPdf.FrameRect();
        //        oPdf.Transform.Reset();
        //        oPdf.Transform.Rotate(45, oPdf.MediaBox.Width / 2, oPdf.MediaBox.Height / 2);
        //        oPdf.AddHtml("MPC Systems");
        //    }
        //    oPdf.HPos = 0;
        //    oPdf.VPos = 0;
        //    oPdf.TextStyle.Outline = 0;
        //    oPdf.TextStyle.Strike = false;
        //    oPdf.TextStyle.Bold = false;
        //    oPdf.TextStyle.Italic = false;
        //    oPdf.TextStyle.CharSpacing = 0;
        //    oPdf.Transform.Reset();
        //    oPdf.Transform.Rotate(0, 0, 0);
        //    oPdf.Transform.Reset();
        //}

        //public bool generatePagePreviewMultiplage(byte[] PDFDoc, string savePath, double CuttingMargin, int DPI, bool RoundCorners)
        //{

        //    CuttingMargin = DesignerUtils.PixelToPoint(CuttingMargin); // as when we get template back from Designer it contains cutting margin in pixels
        //    //XSettings.License = "810-031-225-276-0715-601";
        //    using (Doc theDoc = new Doc())
        //    {

        //        try
        //        {
        //            theDoc.Read(PDFDoc);
        //            for (int i = 1; i <= theDoc.PageCount; i++)
        //            {


        //                theDoc.PageNumber = i;
        //                theDoc.Rect.String = theDoc.CropBox.String;
        //                theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

        //                if (System.IO.Directory.Exists(savePath) == false)
        //                {
        //                    System.IO.Directory.CreateDirectory(savePath);
        //                }

        //                theDoc.Rendering.DotsPerInch = DPI;
        //                string fileName = "p" + i + ".png";
        //                //if (RoundCorners)
        //                //{
        //                //    Stream str = new MemoryStream();

        //                //    theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName), str);
        //                //    generateRoundCorners(System.IO.Path.Combine(savePath, fileName), System.IO.Path.Combine(savePath, fileName), str);

        //                //}
        //                //else
        //                //{
        //                //    theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName));
        //                //}
        //                theDoc.Rendering.Save(System.IO.Path.Combine(savePath, fileName));
        //            }

        //            theDoc.Dispose();

        //            return true;



        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("generatePagePreview", ex);
        //        }
        //        finally
        //        {
        //            if (theDoc != null)
        //                theDoc.Dispose();
        //        }
        //    }
        //}

        ////generate pdf function
        //private byte[] generatePDF(Template objProduct, TemplatePage objProductPage, List<TemplateObject> listTemplateObjects, string ProductFolderPath, string fontPath, bool IsDrawBGText, bool IsDrawHiddenObjects, bool drawCuttingMargins, bool drawWaterMark, out bool hasOverlayObject, bool isoverLayMode, long OrganisationID, double bleedareaSize, bool drawBleedArea)
        //{
        //    Doc doc = new Doc();
        //    try
        //    {
        //        var FontsList = GetFontList();
        //        doc.TopDown = true;

        //        try
        //        {

        //            if (!isoverLayMode)
        //            {
        //                if (objProductPage.BackGroundType == 1)  //PDF background
        //                {
        //                    if (File.Exists(ProductFolderPath + objProductPage.BackgroundFileName))
        //                    {
        //                        doc.Read(ProductFolderPath + objProductPage.BackgroundFileName);

        //                    }
        //                }
        //                else if (objProductPage.BackGroundType == 2) //background color
        //                {
        //                    //  if (objProductPage.Orientation == 1) //standard 
        //                    //  {
        //                    if (objProductPage.Height.HasValue)
        //                    {
        //                        doc.MediaBox.Height = objProductPage.Height.Value;
        //                    }
        //                    else
        //                    {
        //                        doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
        //                    }
        //                    if (objProductPage.Width.HasValue)
        //                    {
        //                        doc.MediaBox.Width = objProductPage.Width.Value;
        //                    }
        //                    else
        //                    {
        //                        doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
        //                    }



        //                    //}
        //                    //else
        //                    //{
        //                    //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
        //                    //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

        //                    //}
        //                    doc.AddPage();
        //                    LoadBackColor(ref doc, objProductPage);
        //                }
        //                else if (objProductPage.BackGroundType == 3) //background Image
        //                {

        //                    //  if (objProductPage.Orientation == 1) //standard 
        //                    //  {
        //                    if (objProductPage.Height.HasValue)
        //                    {
        //                        doc.MediaBox.Height = objProductPage.Height.Value;
        //                    }
        //                    else
        //                    {
        //                        doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
        //                    }
        //                    if (objProductPage.Width.HasValue)
        //                    {
        //                        doc.MediaBox.Width = objProductPage.Width.Value;
        //                    }
        //                    else
        //                    {
        //                        doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
        //                    }

        //                    //}
        //                    //else
        //                    //{
        //                    //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
        //                    //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

        //                    //}
        //                    doc.AddPage();
        //                    LoadBackGroundImage(ref doc, objProductPage, ProductFolderPath);
        //                }
        //            }
        //            else
        //            {
        //                //if (objProductPage.Orientation == 1) //standard 
        //                //{
        //                if (objProductPage.Height.HasValue)
        //                {
        //                    doc.MediaBox.Height = objProductPage.Height.Value;
        //                }
        //                else
        //                {
        //                    doc.MediaBox.Height = objProduct.PDFTemplateHeight.Value;
        //                }
        //                if (objProductPage.Width.HasValue)
        //                {
        //                    doc.MediaBox.Width = objProductPage.Width.Value;
        //                }
        //                else
        //                {
        //                    doc.MediaBox.Width = objProduct.PDFTemplateWidth.Value;
        //                }

        //                //}
        //                //else
        //                //{
        //                //    doc.MediaBox.Height = objProduct.PDFTemplateWidth.Value;
        //                //    doc.MediaBox.Width = objProduct.PDFTemplateHeight.Value;

        //                //}
        //                doc.AddPage();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new MPCException(ex.ToString(), OrganisationID);
        //        }


        //        double YFactor = 0;
        //        double XFactor = 0;
        //        // int RowCount = 0;




        //        List<TemplateObject> oParentObjects = null;

        //        if (IsDrawHiddenObjects)
        //        {
        //            if (isoverLayMode == true)
        //            {
        //                oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
        //            }
        //            else
        //            {
        //                oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
        //            }
        //        }
        //        else
        //        {
        //            if (isoverLayMode == true)
        //            {
        //                oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == true)).OrderBy(g => g.DisplayOrderPdf).ToList();
        //            }
        //            else
        //            {
        //                oParentObjects = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && g.IsHidden == IsDrawHiddenObjects && (g.IsOverlayObject == false || g.IsOverlayObject == null)).OrderBy(g => g.DisplayOrderPdf).ToList();
        //            }
        //        }
        //        int count = listTemplateObjects.Where(g => g.ProductId == objProduct.ProductId && g.ProductPageId == objProductPage.ProductPageId && (g.IsOverlayObject == true)).Count();
        //        hasOverlayObject = false;
        //        if (count > 0)
        //        {
        //            hasOverlayObject = true;
        //        }
        //        foreach (var objObjects in oParentObjects)
        //        {

        //            if (XFactor != objObjects.PositionX)
        //            {
        //                if (objObjects.ContentString == "")
        //                    YFactor = objObjects.PositionY.Value - 7;
        //                else
        //                    YFactor = 0;
        //                XFactor = objObjects.PositionX.Value;
        //            }



        //            if (objObjects.ObjectType == 1 || objObjects.ObjectType == 2 || objObjects.ObjectType == 4)   //|| objObjects.ObjectType == 5
        //            {


        //                int VAlign = 1, HAlign = 1;

        //                HAlign = objObjects.Allignment.Value;

        //                VAlign = objObjects.VAllignment.Value;

        //                double currentX = objObjects.PositionX.Value, currentY = objObjects.PositionY.Value;


        //                if (VAlign == 1 || VAlign == 2)
        //                    currentY = objObjects.PositionY.Value + objObjects.MaxHeight.Value;
        //                bool isTemplateSpot = false;
        //                if (objProduct.isSpotTemplate.HasValue == true && objProduct.isSpotTemplate.Value == true)
        //                    isTemplateSpot = true;

        //                AddTextObject(objObjects, objProductPage.PageNo.Value, FontsList, ref doc, fontPath, currentX, currentY, objObjects.MaxWidth.Value, objObjects.MaxHeight.Value, isTemplateSpot, OrganisationID);



        //            }
        //            // object type 13 real state property image 

        //            else if (objObjects.ObjectType == 3 || objObjects.ObjectType == 8 || objObjects.ObjectType == 12 || objObjects.ObjectType == 13) //3 = image and (8 ) =  Company Logo   12  = contactperson logo 13 = real state image placeholders 
        //            {
        //                //if (objObjects.ObjectType == 8 || objObjects.ObjectType == 12)
        //                // {
        //                if (!objObjects.ContentString.Contains("assets/Imageplaceholder") && !objObjects.ContentString.Contains("{{"))
        //                {
        //                    if (objObjects.ClippedInfo == null)
        //                    {
        //                        if (objObjects.ContentString.Contains(".svg") && !objObjects.ContentString.Contains("{{"))
        //                        {
        //                            GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //                        }
        //                        else
        //                        {
        //                            LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        LoadCroppedImg(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //                    }
        //                }
        //                //  }
        //                //  else
        //                // {
        //                //     LoadImage(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //                // }
        //            }
        //            else if (objObjects.ObjectType == 5)    //line vector
        //            {
        //                DrawVectorLine(ref doc, objObjects, objProductPage.PageNo.Value);
        //            }
        //            else if (objObjects.ObjectType == 6)    //line vector
        //            {
        //                DrawVectorRectangle(ref doc, objObjects, objProductPage.PageNo.Value);
        //            }
        //            else if (objObjects.ObjectType == 7)    //line vector
        //            {
        //                DrawVectorEllipse(ref doc, objObjects, objProductPage.PageNo.Value);
        //            }
        //            else if (objObjects.ObjectType == 9)    //svg Path
        //            {
        //                GetSVGAndDraw(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //                //DrawSVGVectorPath(ref doc, objObjects, ProductFolderPath, objProductPage.PageNo.Value);
        //            }

        //        }
        //        double TrimBoxSize = 5;
        //        double BleedBoxSize = 0;
        //        //crop marks or margins
        //        if (objProduct.CuttingMargin != null && objProduct.CuttingMargin != 0)
        //        {
        //            //doc.CropBox.Height = doc.MediaBox.Height;
        //            //doc.CropBox.Width = doc.MediaBox.Width;


        //            bool isWaterMarkText = objProduct.isWatermarkText ?? true;
        //            if (System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"] != null) // sytem.web.confiurationmanager
        //            {
        //                TrimBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TrimBoxSize"]);
        //            }
        //            doc.SetInfo(doc.Page, "/TrimBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(TrimBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(TrimBoxSize)).ToString());
        //            if (System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"] != null)
        //            {
        //                double ArtBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["ArtBoxSize"]);
        //                doc.SetInfo(doc.Page, "/ArtBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(ArtBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(ArtBoxSize)).ToString());

        //            }
        //            if (System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"] != null)
        //            {
        //                BleedBoxSize = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["BleedBoxSize"]);
        //                doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(BleedBoxSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(BleedBoxSize)).ToString());
        //            }
        //            if (bleedareaSize != 0)
        //            {

        //                doc.SetInfo(doc.Page, "/BleedBox:Rect", (doc.MediaBox.Left + DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Top + DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Width - DesignerUtils.MMToPoint(bleedareaSize)).ToString() + " " + (doc.MediaBox.Height - DesignerUtils.MMToPoint(bleedareaSize)).ToString());
        //            }
        //            int FontID = 0;
        //            var pFont = FontsList.Where(g => g.FontName == "Arial Black").FirstOrDefault();
        //            //if (pFont != null)
        //            //{
        //            //    string path = "";
        //            //    if (pFont.FontPath == null)
        //            //    {
        //            //        path = "";
        //            //    }
        //            //    else
        //            //    {  // customer fonts 

        //            //        path = pFont.FontPath;
        //            //    }
        //            //    if (System.IO.File.Exists(fontPath + path + pFont.FontFile + ".ttf"))
        //            //        FontID = doc.EmbedFont(fontPath + path + pFont.FontFile + ".ttf");


        //            //}
        //            if (pFont != null)
        //            {
        //                string path = "";
        //                if (pFont.FontPath == null)
        //                {
        //                    // mpc designers fonts or system fonts 
        //                    path = "Organisation" + OrganisationID + "/WebFonts/";//"PrivateFonts/FontFace/";//+ objFont.FontFile; at the root of MPC_content/Webfont
        //                }
        //                else
        //                {  // customer fonts 
        //                    path = pFont.FontPath;
        //                }
        //                if (System.IO.File.Exists(fontPath + path + pFont.FontFile + ".ttf"))
        //                    FontID = doc.EmbedFont(fontPath + path + pFont.FontFile + ".ttf");
        //            }
        //            doc.Font = FontID;
        //            double trimboxSizeCuttingLines = 0;
        //            if (TrimBoxSize != 5)
        //                trimboxSizeCuttingLines = TrimBoxSize;
        //            DrawCuttingLines(ref doc, objProduct.CuttingMargin.Value, 1, objProductPage.PageName, objProduct.TempString, drawCuttingMargins, drawWaterMark, isWaterMarkText, objProduct.PDFTemplateHeight.Value, objProduct.PDFTemplateWidth.Value, TrimBoxSize, BleedBoxSize);
        //        }

        //        if (IsDrawBGText == true)
        //        {
        //            DrawBackgrounText(ref doc);
        //        }
        //        int res = 300;
        //        if (System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"] != null)
        //        {
        //            res = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PDFRenderingDotsPerInch"]);
        //        }
        //        doc.Rendering.DotsPerInch = res;

        //        //if (ShowHighResPDF == false)
        //        //    opage.Session["PDFFile"] = doc.GetData();
        //        //OpenPage(opage, "Admin/Products/ViewPdf.aspx");

        //        return doc.GetData();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ShowPDF", ex);
        //    }
        //    finally
        //    {
        //        doc.Dispose();
        //    }
        //}

        //// generate low res proof image from pdf file 
        //private string generatePagePreview(byte[] PDFDoc, string savePath, string PreviewFileName, double CuttingMargin, int DPI, bool RoundCorners)
        //{
        //    CuttingMargin = DesignerUtils.PixelToPoint(CuttingMargin);
        //    using (Doc theDoc = new Doc())
        //    {
        //        Stream str = null;
        //        try
        //        {
        //            theDoc.Read(PDFDoc);
        //            theDoc.PageNumber = 1;
        //            theDoc.Rect.String = theDoc.CropBox.String;
        //            theDoc.Rect.Inset(CuttingMargin, CuttingMargin);

        //            if (System.IO.Directory.Exists(savePath) == false)
        //            {
        //                System.IO.Directory.CreateDirectory(savePath);
        //            }
        //            string filePath = savePath + PreviewFileName + ".png";
        //            theDoc.Rendering.DotsPerInch = DPI;
        //            theDoc.Rendering.Save(filePath);
        //            theDoc.Dispose();
        //            //if (RoundCorners)
        //            //{
        //            //    generateRoundCorners(filePath, filePath,str);
        //            //}



        //            return PreviewFileName + ".png";



        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("generatePagePreview", ex);
        //        }
        //        finally
        //        {
        //            if (theDoc != null)
        //                theDoc.Dispose();
        //            if (str != null)
        //                str.Dispose();
        //        }
        //    }

        //}


        //// returns list of pages and objects along with template called while generating template pdf;
        //public Template GetTemplate(long productID, out List<TemplatePage> listPages, out List<TemplateObject> listTemplateObjs)
        //{
        //    db.Configuration.LazyLoadingEnabled = false;
        //    var template = db.Templates.Where(g => g.ProductId == productID).SingleOrDefault();
        //    listPages = null;
        //    listTemplateObjs = null;
        //    if (template != null)
        //    {
        //        listPages = db.TemplatePages.Where(g => g.ProductId == productID).ToList();
        //        listTemplateObjs = db.TemplateObjects.Where(g => g.ProductId == productID).ToList();
        //    }
        //    // add default cutting margin if not available 
        //    if (template.CuttingMargin.HasValue)
        //        template.CuttingMargin = DesignerUtils.PointToPixel(template.CuttingMargin.Value);
        //    else
        //        template.CuttingMargin = DesignerUtils.PointToPixel(14.173228345);

        //    return template;
        //}
        #endregion


        /// <summary>
        /// This function return the delivery item also
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public List<Item> GetAllOrderItems(long OrderId)
        {
            try
            {
                return db.Items.Where(i => i.EstimateId == OrderId && i.IsOrderedItem == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Search Estimates For Live Jobs
        /// </summary>
        public LiveJobsSearchResponse GetEstimatesForLiveJobs(LiveJobsRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<Estimate, bool>> query =
                estimate =>
                    (string.IsNullOrEmpty(request.SearchString) || (estimate.Company.Name.Contains(request.SearchString)) ||
                     (estimate.Estimate_Name.Contains(request.SearchString))) && (
                         (estimate.OrganisationId == OrganisationId && estimate.StatusId == (int)OrderStatus.InProduction));

            IQueryable<Item> estimates = DbSet.Where(query).SelectMany(est => est.Items).Where(item => item.ItemType != 2);

            List<Item> items = estimates.OrderBy(est => est.EstimateId)
           .Skip(fromRow)
            .Take(toRow)
            .ToList();
            return new LiveJobsSearchResponse { Items = items, TotalCount = estimates.Count() };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public long GetStoreIdByOrderId(long OrderId)
        {
            try
            {
                long StoreId = 0;
                Estimate order = db.Estimates.Where(e => e.EstimateId == OrderId).FirstOrDefault();
                if (order != null)
                {
                    Company oCompany = db.Companies.Where(c => c.CompanyId == order.CompanyId).FirstOrDefault();
                    if (oCompany != null)
                    {
                        if (oCompany.IsCustomer == 1 && oCompany.StoreId != null)
                        {
                            StoreId = oCompany.StoreId ?? 0;
                        }
                        else if (oCompany.IsCustomer == 3)
                        {
                            StoreId = oCompany.CompanyId;
                        }
                    }
                }
                return StoreId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool UpdateItemAttachmentPath(List<Item> items)
        {
            try
            {
                string ProductionPath = "MPC_Content/Artworks/" + OrganisationId + "/Production";
                if (items != null)
                {
                    foreach (var itm in items)
                    {
                        if (itm.ItemAttachments != null)
                        {
                            foreach (var iAttchm in itm.ItemAttachments)
                            {
                                iAttchm.FolderPath = ProductionPath;
                            }
                        }
                    }
                    db.SaveChanges();

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;

            }

        }

        public List<Item> GetOrderItemsIncludingDelivery(long OrderId, int OrderStatus)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return (from r in db.Items
                        where r.EstimateId == OrderId && r.IsOrderedItem == true 
                        
                        select r).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public double? GetOrderTotalById(long OrderId)
        {
                return db.Estimates.Where(e => e.EstimateId == OrderId).Select(t => t.Estimate_Total).FirstOrDefault();
        }

        public List<long> GetOrdersForBillingCycle(DateTime billingDate, bool isDirectOrder)
        {
            List<long> ordersList = DbSet.Where(
                    o =>
                        o.isEstimate == false && o.isDirectSale == isDirectOrder && o.StatusId != 3 &&
                        o.CreationDate >= billingDate.AddMonths(-1) && o.CreationDate <= billingDate).OrderByDescending(o => o.CreationDate).Select(o => o.EstimateId).ToList();
            
            return ordersList;
        }

        public bool IsExtradOrderForBillingCycle(DateTime billingDate, bool isDirectOrder, int licensedCount, long orderId, long organisationId)
        {
            DateTime lastMonth = billingDate.AddMonths(-1);
            List<long> ordersList = DbSet.Where(
                    o =>
                        o.isEstimate == false && o.isDirectSale == isDirectOrder && o.StatusId != 3 && o.OrganisationId == organisationId &&
                        o.CreationDate >= lastMonth && o.CreationDate <= billingDate).OrderBy(o => o.CreationDate).Select(o => o.EstimateId).Take(licensedCount).ToList();

            if (ordersList.Count < licensedCount)
            {
                return false;
            }
            else if (ordersList.Contains(orderId))
            {
                return false;
            }
            else
            {
                return true;
            }
           
                
            
        }

        public void UpdateOrderForDel(Estimate Order)
        {
            db.Estimates.Attach(Order);

            db.Entry(Order).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteOrderById(long OrderId)
        {
            try
            {
                db.Database.CommandTimeout = 1080;
                db.usp_DeleteOrderByID(OrderId);
            }
            catch(Exception ex)
            {
                 throw ex;
            }
        }


        public bool UpdateOderStatus(Estimate Estimate)
        {

            bool Result = false;

            Estimate GetOrder = db.Estimates.Where(i => i.EstimateId == Estimate.EstimateId).FirstOrDefault();

            GetOrder.StatusId = (int)OrderStatus.ShoppingCart;

            db.Estimates.Attach(GetOrder);

            db.Entry(GetOrder).State = EntityState.Modified;

            if (db.SaveChanges() > 0)
            {
                Result = true;
            }
            return Result;
        }

        public bool UpdateOrderAndItemsForRejectOrder(long OrderId,long CartOrderId)
        {
            bool Result = false;
            List<Item> ItemList = db.Items.Where(i => i.EstimateId == CartOrderId).ToList();
            if (ItemList.Count > 0)
            {
                foreach (Item item in ItemList)
                {
                    item.EstimateId = OrderId;

                    db.Items.Attach(item);

                    db.Entry(item).State = EntityState.Modified;
                    
                }

                if (db.SaveChanges() > 0)
                {
                    Result = true;

                    DeleteOrderById(CartOrderId);
                }
            }
            return Result;
        }

        public List<Item> GetTemplateItemsByOrderID(long orderId)
        {
            //db.Configuration.LazyLoadingEnabled = false;
            return db.Items.Include("Templates").Where(i => i.EstimateId == orderId && i.TemplateId > 0 && i.IsOrderedItem == true && i.ProductType == 1).ToList();
            
        }

       
    }
}






