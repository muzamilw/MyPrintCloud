using System.Data;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Export.Pdf.Section;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ICompanyService = MPC.Interfaces.WebStoreServices.ICompanyService;
using IOrderService = MPC.Interfaces.WebStoreServices.IOrderService;

namespace MPC.Implementation.WebStoreServices
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _OrderRepository;
        public readonly IAddressRepository _AddressRepository;
        public readonly ICountryRepository _CountryRepository;
        public readonly IStateRepository _StateRepository;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly ICompanyService _myCompanyService;
        private readonly ICompanyContactRepository _myCompanyContact;
        private readonly IPrefixRepository _prefixRepository;
        private readonly IItemRepository _ItemRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IReportRepository _reportRepository;
        //private readonly IExportReportHelper _reportHelper;
        private readonly ICampaignRepository _campaignRepository;
       
          #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public OrderService(IOrderRepository OrderRepository, IWebstoreClaimsHelperService myClaimHelper, ICompanyService myCompanyService, ICompanyContactRepository myCompanyContact, IPrefixRepository prefixRepository, ICountryRepository CountryRepository,
            IStateRepository StateRepository, IAddressRepository AddressRepository, IItemRepository ItemRepository, IPurchaseRepository purchaseRepository,
            IReportRepository reportRepository, ICampaignRepository campaignRepository)
        {
            this._OrderRepository = OrderRepository;
            this._myClaimHelper = myClaimHelper;
            this._myCompanyService = myCompanyService;
            this._myCompanyContact = myCompanyContact;
            this._prefixRepository = prefixRepository;
            this._CountryRepository = CountryRepository;
            this._StateRepository = StateRepository;
            this._AddressRepository = AddressRepository;
            this._ItemRepository = ItemRepository;
            this._purchaseRepository = purchaseRepository;
            this._reportRepository = reportRepository;
            //this._reportHelper = reportHelper;
            this._campaignRepository = campaignRepository;
        }


        #endregion

        public int GetFirstItemIDByOrderId(int orderId)
        {
            return _OrderRepository.GetFirstItemIDByOrderId(orderId);

        }

        // if user order cookie is null the we process the order
        public long ProcessPublicUserOrder(string orderTitle, long OrganisationId, StoreMode storeMode, long CompanyId, long ContactId, ref long TemporaryRetailCompanyId)
        {
            long orderID = 0;
            if (!IsUserLoggedIn())
            {
                if (TemporaryRetailCompanyId == 0) // temporary customer doesn't exists in cookie
                {
                    if (storeMode ==  StoreMode.Retail) // retail
                    {
                        TemporaryRetailCompanyId = CreateTemporaryCustomer(OrganisationId, CompanyId);
                        long TemporaryContactId = _myCompanyContact.GetContactIdByCustomrID(TemporaryRetailCompanyId);
                        orderID = _OrderRepository.CreateNewOrder(TemporaryRetailCompanyId, TemporaryContactId, OrganisationId, orderTitle);
                    }
                }
                else
                {
                   // temporary customer exists in cookie
                    Company temporaryCompany = _myCompanyService.GetCompanyByCompanyID(TemporaryRetailCompanyId);
                    if (temporaryCompany == null)
                    {
                        TemporaryRetailCompanyId = CreateTemporaryCustomer(OrganisationId, CompanyId);
                    }

                    long TemporaryContactId = _myCompanyContact.GetContactIdByCustomrID(TemporaryRetailCompanyId);
                    orderID = _OrderRepository.GetOrderID(TemporaryRetailCompanyId, TemporaryContactId, orderTitle, OrganisationId);
                }
            }
            else
            {
                orderID = _OrderRepository.GetOrderID(_myClaimHelper.loginContactCompanyID(), _myClaimHelper.loginContactID(), orderTitle, OrganisationId);
                 
            }

            TemporaryRetailCompanyId = TemporaryRetailCompanyId;
            return orderID;
                 
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
     
        private long CreateTemporaryCustomer(long OrganisationId, long StoreId)
        {
            return _myCompanyService.CreateCustomer("Web Store Customer", true, false, CompanyTypes.TemporaryCustomer, "", OrganisationId, StoreId);
        }
        public long GetUserShopCartOrderID(int status)
        {
            return _OrderRepository.GetUserShopCartOrderID(status);
        }
        public ShoppingCart GetShopCartOrderAndDetails(long orderID,OrderStatus Orderstatus)
        {
            var cart = _OrderRepository.GetShopCartOrderAndDetails(orderID, Orderstatus);
            cart.IsDownloadOrder = _ItemRepository.IsDigitalDownloadOrder(orderID);
            return cart;
        }
        public DiscountVoucher GetVoucherRecord(int VId)
        {

            return _OrderRepository.GetVoucherRecord(VId); 
        }
        public Estimate GetOrderByID(long orderId)
        {
            try
            {
                return _OrderRepository.GetOrderByID(orderId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateOrderStatusAfterPrePayment(Estimate tblOrder, OrderStatus orderStatus, StoreMode mode)
        {
           return _OrderRepository.UpdateOrderStatusAfterPrePayment(tblOrder, orderStatus, mode);
        }

        //private void UpdateOrderedItems(OrderStatus orderStatus, Estimate tblOrder, ItemStatuses itemStatus, StoreMode Mode)
        //{

        //    tblOrder.Items.ToList().ForEach(item =>
        //    {
        //        if (item.IsOrderedItem.HasValue && item.IsOrderedItem.Value)
        //        {

        //            if (orderStatus != OrderStatus.ShoppingCart)
        //                item.StatusId = (short)itemStatus;
        //            _OrderRepository.updateStockAndSendNotification(item.RefItemId, Mode, tblOrder.CompanyId, Convert.ToInt32(item.Qty1), Convert.ToInt32(tblOrder.ContactId), Convert.ToInt32(item.ItemId), Convert.ToInt32(tblOrder.EstimateId), MgrIds, org);
                    

        //        }
        //        else
        //        {//Delete the non included items
        //            bool result = false;
        //            List<Model.ArtWorkAttatchment> itemAttatchments = null;
        //            Web2Print.DAL.Templates clonedTempldateFiles = null;

        //            result = ProductManager.RemoveCloneItem(item.ItemID, out itemAttatchments, out clonedTempldateFiles);
        //            if (result)
        //            {
        //                BLL.ProductManager.RemoveItemAttacmentPhysically(itemAttatchments); // file removing physicslly
        //                BLL.ProductManager.RemoveItemTemplateFilesPhysically(clonedTempldateFiles); // file removing
        //            }

        //            //dbContext.tbl_items.DeleteObject(item);
        //        }

        //    });
        //}

        public bool SetOrderCreationDateAndCode(long orderId, long OrganisationId)
        {
            return _OrderRepository.SetOrderCreationDateAndCode(orderId, OrganisationId);
        }
        //public bool IsVoucherValid(string voucherCode)
        //{
        //    try
        //    {
        //        return _OrderRepository.IsVoucherValid(voucherCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public Estimate CheckDiscountApplied(int orderId)
        //{
        //    try
        //    {
        //        return _OrderRepository.CheckDiscountApplied(orderId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        
        //public double SaveVoucherCodeAndRate(int orderId, string VCode)
        //{
        //    try
        //    {
        //        return _OrderRepository.SaveVoucherCodeAndRate(orderId, VCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public double PerformVoucherdiscountOnEachItem(int orderId, OrderStatus orderStatus, double StateTax, double VDiscountRate,StoreMode Mode)
        //{
        //    try
        //    {
        //        return _OrderRepository.PerformVoucherdiscountOnEachItem(orderId, orderStatus, StateTax, VDiscountRate, Mode);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
       //public bool ResetOrderVoucherCode(int orderId)
       //{
       //    try
       //    {
       //        return _OrderRepository.ResetOrderVoucherCode(orderId);
       //    }
       //    catch (Exception ex)
       //    {
       //        throw ex;
       //    }
       //}

        /// <summary>
        /// Get the OrderId by login User 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
       public long GetOrderIdByContactId(long contactId, long companyId)
       {
           try
           {
               return _OrderRepository.GetCartOrderId(contactId, companyId);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public bool UpdateOrderWithDetails(long orderID, long loggedInContactID, double? orderTotal, int deliveryEstimatedCompletionTime, StoreMode isCorpFlow, CompanyContact Contact)
       {
            try
            {
                return _OrderRepository.UpdateOrderWithDetails(orderID, loggedInContactID, orderTotal, deliveryEstimatedCompletionTime, isCorpFlow, Contact);
            }
            catch (Exception ex)
            {
                throw ex;
            }
       }
       public bool IsOrderBelongToCorporate(long orderID, out long customerID)
        {

           try
           {
               return _OrderRepository.IsOrderBelongToCorporate(orderID, out customerID);

           }
           catch (Exception ex)
           {
               throw ex;
           }

        }

       public bool ValidateOrderForCorporateLogin(long orderID, bool isPlaceOrder, int IsCustomer, bool isWebAccess,out long CustomerID)
       {

           if (this.IsOrderInCorporateScenario(orderID, out CustomerID, IsCustomer, isWebAccess) && _myClaimHelper.isUserLoggedIn() == false)
           {
              // this.CreateCorpLoginRedirect(CustomerID);
               return true;
           }
           else
           {
               return false;
           }
       }

       public bool IsOrderInCorporateScenario(long orderID, out long customerID, int IsCustomer, bool isWebAccess)
       {
           bool result = false;
           customerID = 0;

           if (this.IsUserCorporate(isWebAccess, IsCustomer) || this.IsOrderCorporate(orderID, out customerID))
               result = true;

           return result;
       }

       public bool IsOrderCorporate(long itemID, out long customerID)
       {
           customerID = 0;
           return _OrderRepository.IsOrderBelongToCorporate(itemID, out customerID);
       }

       public bool IsUserCorporate(bool IsWebAccess, int IsCustomer)
       {

           //check whether the logged in company is acorporate user or not. also check if someone is already logged in.
           bool result = Convert.ToBoolean(IsCustomer == (int)CustomerTypes.Corporate);

           //further check if logged in user has corporate access or not.
           result = result && (_myClaimHelper.loginContactID() > 0 && (IsWebAccess));

           return result;
       }
       public List<State> GetStates()
       {
           try
           {
               return _StateRepository.GetStates();
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }
         public OrderDetail GetOrderReceipt(long orderID)
       {
           var ordDetail = _OrderRepository.GetOrderReceipt(orderID);
           ordDetail.IsDownloadOrder = _ItemRepository.IsDigitalDownloadOrder(orderID);
           return ordDetail;
       }
       public List<Country> PopulateBillingCountryDropDown()
       {
           try
           {
               return _CountryRepository.PopulateBillingCountryDropDown();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        public Country GetCountryByID(long CountryID)
        {
            try
            {
                return _CountryRepository.GetCountryByID(CountryID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateTaxInCloneItemForServic(long orderId, double TaxValue, StoreMode Mode)
        {
            try
            {
                _OrderRepository.updateTaxInCloneItemForServic(orderId, TaxValue, Mode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateOrderWithDetailsToConfirmOrder(long orderID, long loggedInContactID, OrderStatus orderStatus, Address billingAdd, Address deliveryAdd, double grandOrderTotal,
                                             string yourReferenceNumber, string specialInsTel, string specialInsNotes, bool isCorpFlow, StoreMode CurrntStoreMde, long OrganisationId)
        {
            try
            {
                Estimate Objorder = _OrderRepository.GetOrderByID(orderID);
                if (Objorder != null)
                {
                    _AddressRepository.UpdateAddress(billingAdd, deliveryAdd, Objorder.CompanyId);
                    Prefix prefix = _prefixRepository.GetDefaultPrefix(OrganisationId);
                    return _OrderRepository.UpdateOrderWithDetailsToConfirmOrder(orderID, loggedInContactID, orderStatus, billingAdd, deliveryAdd, grandOrderTotal, yourReferenceNumber, specialInsTel, specialInsNotes, isCorpFlow, CurrntStoreMde, Objorder, prefix);
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
        public bool UpdateOrderAndCartStatus(long OrderID, OrderStatus orderStatus, StoreMode currentStoreMode, Organisation Org, List<Guid> ManagerIds, long StoreId)
        {
            bool isOrderUpdated = _OrderRepository.UpdateOrderAndCartStatus(OrderID, orderStatus, currentStoreMode, Org, ManagerIds, StoreId);
            //Code By Naveed 20160721 To Auto create and Push Purchase order based on flag
            //if (isOrderUpdated && Org.IsAutoPushPurchaseOrder == true)
            //{
            //    var isPoGenerated = _purchaseRepository.GeneratePO(OrderID, ManagerIds[0]);
            //    if (isPoGenerated)
            //        PurchaseOrderEmail(OrderID, Org);
            //}
            //--------------------------------------------
            return isOrderUpdated;
        }

        public bool IsPoGenerated(long orderId, Guid managerId)
        {
            return _purchaseRepository.GeneratePO(orderId, managerId);
        }

        public Dictionary<int, long> GetPurchasesListByOrderId(long orderId)
        {
            return _purchaseRepository.GetPurchasesList(orderId);
        }

        public Estimate GetOrderByIdWithCompany(long orderId)
        {
            return _OrderRepository.GetOrderByIdWithCompany(orderId);
        }
        public double UpdateORderGrandTotal(long OrderID)
        {
            try
            {
                return _OrderRepository.UpdateORderGrandTotal(OrderID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SaveDilveryCostCenter(long orderId, CostCentre ChangedCostCenter)
        {
            try
            {
                return _OrderRepository.SaveDilveryCostCenter(orderId, ChangedCostCenter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Estimate GetLastOrderByContactId(long ContactId)
        {
            try
            {
                return _OrderRepository.GetLastOrderByContactID(ContactId);
                
            }
            catch(Exception ex)
            {
                throw ex;
            }

         }
        public List<Order> GetOrdersListByContactID(long contactUserID, OrderStatus? orderStatus,string fromDate, string toDate, string orderRefNumber, int pageSize, int pageNumber) 
        {
            return _OrderRepository.GetOrdersListByContactID(contactUserID, orderStatus, fromDate, toDate, orderRefNumber, pageSize, pageNumber);
        
        }
       public List<Order> GetOrdersListExceptPendingOrdersByContactID(long contactUserID, OrderStatus? orderStatus, string fromDate, string toDate, string orderRefNumber, int pageSize, int pageNumber)
        {
            return _OrderRepository.GetOrdersListExceptPendingOrdersByContactID(contactUserID, orderStatus, fromDate, toDate, orderRefNumber, pageSize, pageNumber);
        
        }
       public Order GetOrderAndDetails(long orderID)
       {
           return _OrderRepository.GetOrderAndDetails(orderID);
       }

       public Address GetBillingAddress(long BillingAddressId)
       {

           return _OrderRepository.GetAddress(BillingAddressId);
       }

       public Address GetdeliveryAddress(long ShippingAddressId)
       {
           return _OrderRepository.GetAddress(ShippingAddressId);
       }
       //public long ReOrder(long ExistingOrderId, long loggedInContactID, double StatTaxVal, StoreMode mode, bool isIncludeTax, int TaxID, long OrganisationId, long StoreId)
       //{
       //  //  return _OrderRepository.ReOrder(ExistingOrderId, loggedInContactID, StatTaxVal, mode, isIncludeTax, TaxID, OrganisationId);

       //    Estimate ExistingOrder = null;
       //    Estimate shopCartOrder = null;
       //    bool result = false;
         
       //    List<Item> ClonedItems = new List<Item>();
       //    long OrderIdOfReorderItems = 0;
        
       //    try
       //    {
       //        ExistingOrder = _OrderRepository.GetOrderByID(ExistingOrderId); 

       //        if (ExistingOrder != null)
       //        {

       //            shopCartOrder = _OrderRepository.GetShoppingCartOrderByContactID(loggedInContactID, OrderStatus.ShoppingCart);
       //            //create a new cart
       //            if (shopCartOrder == null)
       //            {
       //                shopCartOrder = ExistingOrder;
       //                // Order status will be shopping cart
       //                shopCartOrder.StatusId = (int)OrderStatus.ShoppingCart;
       //                shopCartOrder.DeliveryCompletionTime = 0;
       //                shopCartOrder.DeliveryCost = 0;
       //                shopCartOrder.DeliveryCostCenterId = 0;
       //                shopCartOrder.StartDeliveryDate = null;
       //                Prefix prefix = _prefixRepository.GetDefaultPrefix();
       //                if (prefix != null)
       //                {
       //                    shopCartOrder.Order_Code = prefix.OrderPrefix + "-001-" + prefix.OrderNext.ToString();
       //                    prefix.OrderNext = prefix.OrderNext + 1;
       //                }
       //                shopCartOrder.Order_CompletionDate = null;
       //                shopCartOrder.Order_ConfirmationDate = null;
       //                shopCartOrder.Order_CreationDateTime = DateTime.Now;
       //                shopCartOrder.CustomerPO = null;

       //                _OrderRepository.Add(shopCartOrder);

       //                OrderIdOfReorderItems = shopCartOrder.EstimateId;
       //            }
       //            else
       //            {
       //                OrderIdOfReorderItems = shopCartOrder.EstimateId;
       //            }
       //            List<Item> esxistingOrderItems = _OrderRepository.GetAllOrderItems(ExistingOrderId); 
       //            //Clone items related to this order
       //            esxistingOrderItems.Where(i => i.ItemType != Convert.ToInt32(ItemTypes.Delivery)).ToList().ForEach(orderITem =>
       //            {
       //                Item item = _ItemRepository.CloneReOrderItem(OrderIdOfReorderItems, orderITem.ItemId, loggedInContactID, shopCartOrder.Order_Code, OrganisationId);
       //                ClonedItems.Add(item);
       //                _ItemService.CopyAttachments(orderITem.ItemId, item, shopCartOrder.Order_Code, false, shopCartOrder.CreationDate ?? DateTime.Now, OrganisationId, StoreId);

       //            });

       //            if (ExistingOrder.DiscountVoucherID.HasValue && ExistingOrder.VoucherDiscountRate > 0)
       //            {
       //                if (_OrderRepository.RollBackDiscountedItemsWithdbContext(ClonedItems, StatTaxVal))
       //                {
       //                    ExistingOrder.VoucherDiscountRate = null;
       //                    ExistingOrder.DiscountVoucherID = null;
       //                    shopCartOrder.VoucherDiscountRate = null;
       //                    shopCartOrder.DiscountVoucherID = null;
       //                }
       //            }
       //            else if (isIncludeTax)// apply the new state Tax Value to the cloned item 
       //            {
       //                _OrderRepository.ApplyCurrentTax(ClonedItems, StatTaxVal, TaxID);
       //            }
       //            result = true;
       //            _OrderRepository.SaveChanges();
       //        }
       //    }
       //    catch (Exception ex)
       //    {
       //        throw ex;
       //    }
     
       //    return shopCartOrder.EstimateId;
           
       //}

       public List<Order> GetAllCorpOrders(long ContactCompany, OrderStatus? orderStatus, string fromDate, string toDate, string orderRefNumber, bool IsManager, long TerritoryId)
       {
           return _OrderRepository.GetAllCorpOrders(ContactCompany, orderStatus, fromDate, toDate, orderRefNumber, IsManager, TerritoryId);
       
       }
       public long GetOrderID(long CompanyID,long ContactID,string orderTitle,long OrganisationId)
       {
           return _OrderRepository.GetOrderID(CompanyID, ContactID, orderTitle, OrganisationId);
       }
       public long CreateNewOrder(long CompanyId, long ContactId, long OrganisationId, string orderTitle = null)
       {

           return _OrderRepository.CreateNewOrder(CompanyId, ContactId, OrganisationId, orderTitle);
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
                return _OrderRepository.GetOrderIdByCompanyId(CompanyId, orderStatus);

            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }
       /// <summary>
       /// check cookie order is the real login customer order
       /// </summary>
       public bool IsRealCustomerOrder(long orderId, long contactId, long companyId)
       {
           return _OrderRepository.IsRealCustomerOrder(orderId,contactId,companyId);
       }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
       public long GetStoreIdByOrderId(long OrderId)
       {
           return _OrderRepository.GetStoreIdByOrderId(OrderId);
       }
       public Estimate GetOrderByOrderID(long OrderId)
       {
           return _OrderRepository.GetOrderByOrderID(OrderId);
       }
       public List<Item> GetOrderItemsIncludingDelivery(long OrderId, int OrderStatus)
       {
           return _OrderRepository.GetOrderItemsIncludingDelivery(OrderId, OrderStatus);
       }
       public void SaveOrUpdateOrder()
       {
           _OrderRepository.SaveChanges();
       }
       public void UpdateOrderForDel(Estimate Order)
       {
           _OrderRepository.UpdateOrderForDel(Order);
       }
       public List<Item> GetOrderItems(long OrderId)
       {
           return _OrderRepository.GetOrderItems(OrderId);
       }
       public void PurchaseOrderEmail(long orderId, long companyId, long supplierContactId, Company objCompany, int purchaseId, long organisationId, string fileName)
       {

           string salesManagerFile = "/" + ImagePathConstants.ReportPath + organisationId + "/" + purchaseId + "PurchaseReport.pdf";
           _campaignRepository.POEmailToSalesManager(orderId, companyId, 0, 250, supplierContactId, salesManagerFile, objCompany);

           string sourceFile = fileName;
           string destinationFileSupplier = "/" + ImagePathConstants.ReportPath + organisationId + "/" + supplierContactId + "/" + purchaseId + "_PurchaseOrder.pdf";

           string oDirectory = HttpContext.Current.Server.MapPath("~/" + ImagePathConstants.ReportPath + organisationId + "/" + supplierContactId);

           string destinationPhysicalFileSupplier = HttpContext.Current.Server.MapPath("~/" + destinationFileSupplier);

           if (!Directory.Exists(oDirectory))
           {
               Directory.CreateDirectory(oDirectory);
           }

           if (File.Exists(sourceFile))
           {
               File.Copy(sourceFile, destinationPhysicalFileSupplier);}

           _campaignRepository.POEmailToSupplier(orderId, objCompany.CompanyId, 0, 250, supplierContactId, destinationFileSupplier, objCompany, false);
           
           
       }

       
    }
}
