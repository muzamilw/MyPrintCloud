using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
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
        // GET: ProductPendingOrders

        public ProductPendingOrdersController(IWebstoreClaimsHelperService _myClaimHelper, IStatusService _StatusService, IOrderService _orderService, ICompanyService _CompanyService, IUserManagerService _usermanagerService, ICampaignService _campaignService)
        {
            this._myClaimHelper = _myClaimHelper;
            this._StatusService = _StatusService;
            this._orderService = _orderService;
            this._CompanyService = _CompanyService;
            this._usermanagerService = _usermanagerService;
            this._campaignService = _campaignService;
        }
        public ActionResult Index()
        {
            if (_myClaimHelper.isUserLoggedIn())
            {
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
            }
            return View("PartialViews/ProductPendingOrders");
        }

        public void BindGrid(bool ApproveOrders,long ContactID,CompanyContact LoginContact)
        {
            List<Order> ordersList = null;
            List<Order> ManagerordersList = new List<Order>();
            ordersList = _CompanyService.GetPendingApprovelOrdersList(ContactID, ApproveOrders);
            if (ordersList == null || ordersList.Count == 0)
            {
                // do nothing
            }
            else {
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
                        
                    }
                    else
                    {
                        ViewBag.OrderList = ManagerordersList;
                    }
                }
                else
                {
                    ViewBag.OrderList= ordersList;
                }
            }
        }
        
        private void approveOrRejectEmailToUser(long userID, long orderID,int Event)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;

            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];
            CompanyContact userRec = _CompanyService.GetContactByID(userID);
            MPC.Models.DomainModels.Company loginUserCompany = _CompanyService.GetCompanyByCompanyID(_myClaimHelper.loginContactCompanyID());
            SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
            CompanyContact UserContact = _CompanyService.GetContactByID(_myClaimHelper.loginContactID());
           
            CampaignEmailParams CPE = new CampaignEmailParams();
            CPE.CompanySiteID = 1;
            CPE.CompanyId = _myClaimHelper.loginContactCompanyID();
            CPE.ContactId = userID;
            CPE.SalesManagerContactID = userID; // this is only dummy data these variables replaced with organization values 
            if ( UserCookieManager.StoreMode ==(int)StoreMode.Corp)
            {
                CPE.StoreID = _myClaimHelper.loginContactCompanyID();
            }
            else
            {
                CPE.StoreID = UserCookieManager.StoreId;
            }
            CPE.AddressID = _myClaimHelper.loginContactCompanyID();
            CPE.EstimateID = orderID;
            CPE.ApprovarID =(int) _myClaimHelper.loginContactID();
            Campaign RegistrationCampaign = _campaignService.GetCampaignRecordByEmailEvent(Event, StoreBaseResopnse.Company.OrganisationId ?? 0, UserCookieManager.StoreId);
            _campaignService.emailBodyGenerator(RegistrationCampaign, CPE, UserContact, StoreMode.Retail, (int)loginUserCompany.OrganisationId, "", "", "", EmailOFSM.Email, "", "", null, "");

        }
        [HttpPost]
        public void ApporRejectOrder(long OrderID)
        { 
             string CacheKeyName = "CompanyBaseResponse";
             ObjectCache cache = MemoryCache.Default;

             MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];
             long ID = _CompanyService.ApproveOrRejectOrder(OrderID, _myClaimHelper.loginContactID(), OrderStatus.RejectOrder, StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
             approveOrRejectEmailToUser(ID, OrderID, (int)Events.Order_Approval_By_Manager);
        
        }
        [HttpPost]
        public void Save(long OrderID, string PO)
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];
            SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
            long ContactID = _CompanyService.ApproveOrRejectOrder(OrderID, _myClaimHelper.loginContactID(), OrderStatus.PendingOrder, StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value,PO);
           
            if (UserCookieManager.StoreMode ==(int) StoreMode.Corp)
            {
                int ManagerID = (int) _CompanyService.GetContactIdByRole(_myClaimHelper.loginContactCompanyID(), (int)Roles.Manager);
                _campaignService.SendEmailToSalesManager((int)Events.NewQuoteToSalesManager,_myClaimHelper.loginContactID(),_myClaimHelper.loginContactCompanyID(), 0, UserCookieManager.OrganisationID, ManagerID, StoreMode.Corp, UserCookieManager.StoreId, EmailOFSM);
            }
             approveOrRejectEmailToUser(ContactID, OrderID, (int)Events.Order_Approval_By_Manager);
        }
    }
}