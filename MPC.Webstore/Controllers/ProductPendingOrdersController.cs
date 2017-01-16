using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
   
    public class ProductPendingOrdersController : Controller
    {
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly IStatusService _StatusService;
        private readonly IOrderService _orderService;
        private readonly ICompanyService _CompanyService;
        private readonly IUserManagerService _usermanagerService;
        private readonly ICampaignService _campaignService;
        private readonly MPC.Interfaces.MISServices.IOrderService _MISOrderService;
        // GET: ProductPendingOrders

        public ProductPendingOrdersController(IWebstoreClaimsHelperService _myClaimHelper, IStatusService _StatusService, IOrderService _orderService, ICompanyService _CompanyService,
            IUserManagerService _usermanagerService, ICampaignService _campaignService, MPC.Interfaces.MISServices.IOrderService MISOrderService)
        {
            this._myClaimHelper = _myClaimHelper;
            this._StatusService = _StatusService;
            this._orderService = _orderService;
            this._CompanyService = _CompanyService;
            this._usermanagerService = _usermanagerService;
            this._campaignService = _campaignService;
            this._MISOrderService = MISOrderService;
        }
        public ActionResult Index()
        {
            try 
            {
                if (_myClaimHelper.isUserLoggedIn())
                {
                    MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                    bool ApproveOrders = false;
                    CompanyContact LoginContact = _CompanyService.GetContactByID(_myClaimHelper.loginContactID());
                    if (LoginContact != null)
                    {
                        if (LoginContact.ContactRoleId == Convert.ToInt32(Roles.Adminstrator) || LoginContact.ContactRoleId == Convert.ToInt32(Roles.Manager))
                        {
                            ApproveOrders = true;
                        }
                        else
                        {
                            ApproveOrders = false;
                        }
                    }
                    BindGrid(ApproveOrders, _myClaimHelper.loginContactID(), LoginContact);
                    ViewBag.IsShowPrices = _CompanyService.ShowPricesOnStore(UserCookieManager.WEBStoreMode, StoreBaseResopnse.Company.ShowPrices ?? false, _myClaimHelper.loginContactID(), UserCookieManager.ShowPriceOnWebstore);
                }
                
            }
            catch(Exception ex)
            {
                throw ex;

            }
            return View("PartialViews/ProductPendingOrders");
        }

        public void BindGrid(bool ApproveOrders,long ContactID,CompanyContact LoginContact)
        {
            try 
            {
                
                List<Order> ordersList = null;
                List<Order> ManagerordersList = new List<Order>();
                ordersList = _CompanyService.GetPendingApprovelOrdersList(ContactID, ApproveOrders, UserCookieManager.WBStoreId);
                if (ordersList == null || ordersList.Count == 0)
                {
                    ViewBag.OrderList = ordersList;
                    ViewBag.TotalOrders = ordersList.Count;

                    TempData["Status"] = Utils.GetKeyValueFromResourceFile("ltrlNoRecFound", UserCookieManager.WBStoreId, "No Records Found");
                     TempData["HeaderStatus"] = false;
                }
                else
                {

                    
                    TempData["HeaderStatus"] = true;

                    if (LoginContact.ContactRoleId == Convert.ToInt32(Roles.Manager))
                    {
                        foreach (var o in ordersList)
                        {
                            if (o.ContactTerritoryID == LoginContact.TerritoryId)
                            {
                                ManagerordersList.Add(o);
                            }
                        }
                        if (ManagerordersList == null || ManagerordersList.Count == 0)
                        {
                            ViewBag.OrderList = new List<Order>(); 
                        }
                        else
                        {
                            ViewBag.OrderList = ManagerordersList;
                            ViewBag.TotalOrders = ManagerordersList.Count;
                        }
                    }
                    else
                    {
                        ViewBag.OrderList = ordersList;
                        ViewBag.TotalOrders = ordersList.Count;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        private void approveOrRejectEmailToUser(long userID, long orderID,int Event)
        {
           
            MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            CompanyContact userRec = _CompanyService.GetContactByID(userID);
            MPC.Models.DomainModels.Company loginUserCompany = _CompanyService.GetCompanyByCompanyID(_myClaimHelper.loginContactCompanyID());
            SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
            if (EmailOFSM != null) 
            {
              
                CampaignEmailParams CPE = new CampaignEmailParams();
                CPE.OrganisationId = UserCookieManager.WEBOrganisationID;
                CPE.CompanyId = _myClaimHelper.loginContactCompanyID();
                CPE.ContactId = userID;
                CPE.SalesManagerContactID = userID; // this is only dummy data these variables replaced with organization values 

                CPE.StoreId = UserCookieManager.WBStoreId;

                CPE.AddressId = _myClaimHelper.loginContactCompanyID();
                CPE.EstimateId = orderID;
                CPE.ApprovarID = (int)_myClaimHelper.loginContactID();
                CPE.SystemUserId = EmailOFSM.SystemUserId;

                Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent(Event, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                _campaignService.emailBodyGenerator(RegistrationCampaign, CPE, userRec, StoreMode.Retail, (int)loginUserCompany.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");

            }
           
        }
        [HttpPost]
        public void ApporRejectOrder(long OrderID,string Rejectionreason)
        { 
          
             MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

             long ID = _CompanyService.ApproveOrRejectOrder(OrderID, _myClaimHelper.loginContactID(), OrderStatus.RejectOrder, StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value, Rejectionreason);
             approveOrRejectEmailToUser(ID, OrderID, (int)Events.RejectOrder);
             ViewBag.IsShowPrices = _CompanyService.ShowPricesOnStore(UserCookieManager.WEBStoreMode, StoreBaseResopnse.Company.ShowPrices ?? false, _myClaimHelper.loginContactID(), UserCookieManager.ShowPriceOnWebstore);
        }
        [HttpPost]
        public void Save(long OrderID, string PO)
        {
          
            MyCompanyDomainBaseReponse StoreBaseResopnse = _CompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
            long ContactID = _CompanyService.ApproveOrRejectOrder(OrderID, _myClaimHelper.loginContactID(), OrderStatus.PendingOrder, StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value,PO);
           
            if (UserCookieManager.WEBStoreMode ==(int) StoreMode.Corp)
            {
                int ManagerID = (int) _CompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager);
                _campaignService.SendEmailToSalesManager((int)Events.NewOrderToSalesManager, _myClaimHelper.loginContactID(), _myClaimHelper.loginContactCompanyID(), OrderID, UserCookieManager.WEBOrganisationID, ManagerID, StoreMode.Corp, UserCookieManager.WBStoreId, EmailOFSM);
            }
             approveOrRejectEmailToUser(ContactID, OrderID, (int)Events.Order_Approval_By_Manager);
             ViewBag.IsShowPrices = _CompanyService.ShowPricesOnStore(UserCookieManager.WEBStoreMode, StoreBaseResopnse.Company.ShowPrices ?? false, _myClaimHelper.loginContactID(), UserCookieManager.ShowPriceOnWebstore);
             if (ContactID > 0 && StoreBaseResopnse.Organisation.IsAutoPushPurchaseOrder == true)
             {
                 PushAutoPurchaseOrder(OrderID, StoreBaseResopnse.Organisation.OrganisationId, EmailOFSM.SystemUserId);
             }
        }
        private void PushAutoPurchaseOrder(long orderId, long organisationId, Guid managerId)
        {
            var isPo = _orderService.IsPoGenerated(orderId, managerId);
            if (isPo)
            {
                var objOrder = _orderService.GetOrderByIdWithCompany(orderId);
                _MISOrderService.ExportPoPdfForWebStore(orderId, objOrder.CompanyId, objOrder.ContactId ?? 0, organisationId);
               
            }

        }
    }
}