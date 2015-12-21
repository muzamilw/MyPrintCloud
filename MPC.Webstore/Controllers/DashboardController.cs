using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.ViewModels;
namespace MPC.Webstore.Controllers
{
    public class DashboardController : Controller
    {
        #region Private

        private readonly IWebstoreClaimsHelperService _webstoreclaimHelper;
        private readonly ICompanyService _myCompanyService;
        private readonly IStatusService _StatusService;
        private readonly IOrderService _orderservice;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DashboardController(IWebstoreClaimsHelperService webstoreClaimHelper, ICompanyService myCompanyService, IStatusService _StatusService, IOrderService _orderservice)
        {

            if (webstoreClaimHelper == null)
            {
                throw new ArgumentNullException("webstoreClaimHelper");
            }
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._webstoreclaimHelper = webstoreClaimHelper;
            this._StatusService = _StatusService;
            this._orderservice = _orderservice;
        }

        #endregion
        // GET: Dashboard
        public ActionResult Index()
        {
            long contactId = _webstoreclaimHelper.loginContactID();
            List<DashboardViewModel> BCDashBordItems = new List<DashboardViewModel>();
            if (_webstoreclaimHelper.isUserLoggedIn())
            {
                //List<DashboardViewModel> DashBordItems = new List<DashboardViewModel>();
                DashboardViewModel Detail = new DashboardViewModel(1);

                // Contact Details
                Detail.Name = Utils.GetKeyValueFromResourceFile("myprofile", UserCookieManager.WBStoreId, "My Profile"); //(string)GetGlobalResourceObject("MyResource", "ltrlcontactdetails");
                Detail.Description = Utils.GetKeyValueFromResourceFile("ltrlchngepic", UserCookieManager.WBStoreId, "Change your profile picture and settings"); //(string)GetGlobalResourceObject("MyResource", "ltrlupdateurcontactdeatails");
                Detail.ImageURL = "<i class='fa fa-user'></i>";
                Detail.PageNavigateURl = "/ContactDetail";
                Detail.IsChangePassword = false;
                BCDashBordItems.Add(Detail);


                Detail = new DashboardViewModel(6);
                // Reset Password
                Detail.Name = Utils.GetKeyValueFromResourceFile("SpnChangePass", UserCookieManager.WBStoreId, "Change Password");//(string)GetGlobalResourceObject("MyResource", "ltrlchangepassword");
                Detail.Description =
                  Utils.GetKeyValueFromResourceFile("ltrlchngecurrentpass", UserCookieManager.WBStoreId, "Change your current password")
; //(string)GetGlobalResourceObject("MyResource", "ltrlresetnchangeaccpassword");
                Detail.ImageURL = "<i class='fa fa-key'></i>";
                Detail.PageNavigateURl = "";
                Detail.IsChangePassword = true;
                BCDashBordItems.Add(Detail);


                    // Shooping Cart details
                    Detail = new DashboardViewModel(7);
                    Detail.Name = Utils.GetKeyValueFromResourceFile("ShoppingCartlbl", UserCookieManager.WBStoreId, "Cart"); //(string)GetGlobalResourceObject("MyResource", "ltrlshoppingcart");
                    Detail.Description = Utils.GetKeyValueFromResourceFile("ltrlabc", UserCookieManager.WBStoreId, "View or edit your cart details");// (string)GetGlobalResourceObject("MyResource", "ltrlviewitemnshppngcart");
                    Detail.ImageURL = "<i class='fa fa-shopping-cart'></i>";
                    Detail.IsChangePassword = false;

                    Detail.PageNavigateURl = "/ShopCart";

                    BCDashBordItems.Add(Detail);
                    // Saved Desgn
                    Detail = new DashboardViewModel(5);
                    Detail.Name = Utils.GetKeyValueFromResourceFile("ltrlDashsaved", UserCookieManager.WBStoreId, "My Saved Designs");// (string)GetGlobalResourceObject("MyResource", "ltrlsavedesign") + UpdateSavedDesignCount();
                    Detail.Description = Utils.GetKeyValueFromResourceFile("ltrlldashsav", UserCookieManager.WBStoreId, "View or reorder your saved designs"); //(string)GetGlobalResourceObject("MyResource", "ltrlmanagenviewsd");
                    Detail.ImageURL = "<i class='fa fa-pencil-square-o'></i>";
                    Detail.PageNavigateURl = "/SavedDesigns";
                    Detail.IsChangePassword = false;
                    BCDashBordItems.Add(Detail);

                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail || (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.User)))
                {
                    Detail = new DashboardViewModel(4);
                    // Address Details
                    Detail.Name = Utils.GetKeyValueFromResourceFile("ltrldash", UserCookieManager.WBStoreId, "Address Manager");// (string)GetGlobalResourceObject("MyResource", "anchorAddressMgr");
                    Detail.Description = "Create and modify your default addresses";
                    Detail.ImageURL = "<i class='fa fa-rocket'></i>";
                    Detail.PageNavigateURl = "/BillingShippingAddressManager";
                    Detail.IsChangePassword = false;
                    BCDashBordItems.Add(Detail);

                }


                // ViewData["rptRetailDashboardItem"] = DashBordItems.OrderBy(g => g.SortOrder).ToList();



                /*************************************************Broker / Corporate Orders  ***********************************/

                DashboardViewModel BCDetail = new DashboardViewModel(1);
                if (((UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)) || (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.User))
                {

                    //CorpDiv.Visible = false;
                    // My Order History
                    BCDetail = new DashboardViewModel(1);
                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                    {
                        BCDetail.Name = Utils.GetKeyValueFromResourceFile("ltrldashMyorders", UserCookieManager.WBStoreId, "My Orders") + "(" + MyOrders() + ")"; //(string)GetGlobalResourceObject("MyResource", "lblORderTracking") + " (" + MyOrders() + ")";
                        BCDetail.Description = Utils.GetKeyValueFromResourceFile("ltrlvieworderdetailsatt", UserCookieManager.WBStoreId, "View order details and attachments");
                    }
                    else
                    {
                        BCDetail.Name = Utils.GetKeyValueFromResourceFile("ltrldashMyorders", UserCookieManager.WBStoreId, "My Orders") + " (" + MyOrders() + ")";//(string)GetGlobalResourceObject("MyResource", "lblOrderList") + " (" + MyOrders() + ")";
                        BCDetail.Description = Utils.GetKeyValueFromResourceFile("ltrlvieworderdetailsatt", UserCookieManager.WBStoreId, "View order details and attachments"); //(string)GetGlobalResourceObject("MyResource", "ltrlviewrocompletedo");
                    }
                    BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                    BCDetail.PageNavigateURl = "/ProductOrderHistory";
                    BCDashBordItems.Add(BCDetail);

                    
                }

                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager))
                {
                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {
                        //BCDetail = new DashboardViewModel(2);
                        //// All Order History

                        //BCDetail.Name = "All Orders" + AllCorpOrdersCount(); // (string)GetGlobalResourceObject("MyResource", "lblAllOrderss");

                        //BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "ltrlviewrocompletedo");
                        //BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        //BCDetail.PageNavigateURl = "/ProductOrderHistory";
                        //BCDetail.IsChangePassword = false;
                        //BCDashBordItems.Add(BCDetail);
                        //// Pending Approvals
                        BCDetail = new DashboardViewModel(3);

                        BCDetail.Description = Utils.GetKeyValueFromResourceFile("lblPendingApprovalsBtn", UserCookieManager.WBStoreId, "Orders Pending Approval")
; //(string)GetGlobalResourceObject("MyResource", "lblOrderApprovalDesc");
                        BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                       // if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
                        //{
                        //    BCDetail.Name = "Orders Pending Approval" + CorpCustomerPendingOrdersCountForManagers(); //(string)GetGlobalResourceObject("MyResource", "lblPendingApprovalsBtn") + CorpCustomerPendingOrdersCountForManagers();
                       // }
                       // else
                        //{
                        BCDetail.Name = Utils.GetKeyValueFromResourceFile("lblPendingApprovalsBtn", UserCookieManager.WBStoreId, "Orders Pending Approval") + CorpCustomerPendingOrdersCount(); // (string)GetGlobalResourceObject("MyResource", "lblPendingApprovalsBtn") + CorpCustomerPendingOrdersCount();
                      //  }
                        BCDetail.PageNavigateURl = "/ProductPendingOrders";
                        BCDetail.IsChangePassword = false;
                        BCDashBordItems.Add(BCDetail);
                        BCDetail = new DashboardViewModel(3);
                        // Order In production
                        BCDetail.Name = Utils.GetKeyValueFromResourceFile("ltrlproductordhis", UserCookieManager.WBStoreId, "Products Order History") + UpdateOrdersInProductionCount(); // (string)GetGlobalResourceObject("MyResource", "lblOrderProductnBtn") + UpdateOrdersInProductionCount();
                        BCDetail.Description = Utils.GetKeyValueFromResourceFile("btnProductDetails", UserCookieManager.WBStoreId, "Description");//(string)GetGlobalResourceObject("MyResource", "lblViewCurOrderStatus");
                        BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        BCDetail.PageNavigateURl = "/ProductOrderHistory";
                        BCDetail.IsChangePassword = false;
                        BCDashBordItems.Add(BCDetail);
                        //BCDetail = new DashboardViewModel(4);
                        //BCDetail.Name = Utils.GetKeyValueFromResourceFile("ltrlsystemusermanger", UserCookieManager.WBStoreId, "System User Manager"); // (string)GetGlobalResourceObject("MyResource", "lblOrderProductnBtn") + UpdateOrdersInProductionCount();
                        //BCDetail.Description = Utils.GetKeyValueFromResourceFile("ltrlsystemmanger", UserCookieManager.WBStoreId, "System Manger");
                        ////(string)GetGlobalResourceObject("MyResource", "lblViewCurOrderStatus");
                        //BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        //BCDetail.PageNavigateURl = "/UserManager";
                        //BCDetail.IsChangePassword = false;
                        //BCDashBordItems.Add(BCDetail);
                        BCDetail = new DashboardViewModel(4);
                        // Order In production
                        BCDetail.Name = "Manage Assets"; // (string)GetGlobalResourceObject("MyResource", "lblOrderProductnBtn") + UpdateOrdersInProductionCount();
                        BCDetail.Description = Utils.GetKeyValueFromResourceFile("btnProductDetails", UserCookieManager.WBStoreId, "Description");//(string)GetGlobalResourceObject("MyResource", "lblViewCurOrderStatus");
                        BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        BCDetail.PageNavigateURl = "/ManageAssets";
                        BCDetail.IsChangePassword = false;
                        BCDashBordItems.Add(BCDetail);
                    }

                }
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                {

                    /*************************************************Store Preferneces ***********************************/


                    //List<DashboardViewModel> StorePrefDashBordItems = new List<DashboardViewModel>();


                    // Payment Preferences
                    //BCDetail = new DashboardViewModel(7);
                    //BCDetail.Name = "Payments Manager"; //(string)GetGlobalResourceObject("MyResource", "lblPayPalSet");
                    //BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblPaypalDesc");
                    //BCDetail.ImageURL = "<i class='fa fa-credit-card'></i>";
                    //BCDetail.PageNavigateURl = "/PaymentPreferences.aspx";
                    //StorePrefDashBordItems.Add(BCDetail);

                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {


                        //BCDetail = new DashboardViewModel(5);
                        //// Banner Manager
                        //BCDetail.Name = "Banner Manager";// (string)GetGlobalResourceObject("MyResource", "lblBannerMgr");
                        //BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblBannerDesc");
                        //BCDetail.ImageURL = "<i class='fa fa-tasks'></i>";
                        //BCDetail.PageNavigateURl = "/BrokerBannerWiget.aspx";
                        //StorePrefDashBordItems.Add(BCDetail);



                        //BCDetail = new DashboardViewModel(6);
                        //// Reports and Stats
                        //BCDetail.Name = "Secondary Page Manager";// (string)GetGlobalResourceObject("MyResource", "lblScendMgr");
                        //BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblSecondDesc");
                        //BCDetail.ImageURL = "<i class='fa fa-file-text'></i>";
                        //BCDetail.PageNavigateURl = "SecondaryPageManager.aspx";
                        //StorePrefDashBordItems.Add(BCDetail);

                        //BCDetail = new DashboardViewModel(8);

                        //// Reports and Stats
                        //BCDetail.Name = "Social Manager";// (string)GetGlobalResourceObject("MyResource", "lblSocialMgr");
                        //BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblSocialDesc");
                        //BCDetail.ImageURL = "<i class='fa fa-key'></i>";
                        //BCDetail.PageNavigateURl = "#";
                        //StorePrefDashBordItems.Add(BCDetail);


                        //BCDetail = new DashboardViewModel(9);

                        //BCDetail.Name = "Keyword MAnager";// (string)GetGlobalResourceObject("MyResource", "lblKeyWords");
                        //BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblKayWordDesc");
                        //BCDetail.ImageURL = "<i class='fa fa-file-text'></i>";
                        //BCDetail.PageNavigateURl = "#";
                        //StorePrefDashBordItems.Add(BCDetail);


                        //BCDetail = new DashboardViewModel(10);

                        //BCDetail.Name = "Google analytics"; //(string)GetGlobalResourceObject("MyResource", "lblGoogleAnalytics");
                        //BCDetail.Description = "Description"; //(string)GetGlobalResourceObject("MyResource", "lblGAnalyticDesc");
                        //BCDetail.ImageURL = "<i class='fa fa-file-text'></i>";
                        //BCDetail.PageNavigateURl = "#";
                        //StorePrefDashBordItems.Add(BCDetail);
                    }
                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {
                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                        {
                            BCDetail = new DashboardViewModel(2);
                            // User manger
                            BCDetail.Name = Utils.GetKeyValueFromResourceFile("ltrldash", UserCookieManager.WBStoreId, "Address Manager"); //(string)GetGlobalResourceObject("MyResource", "anchorAddressMgr");
                            BCDetail.Description = Utils.GetKeyValueFromResourceFile("ltrladdes", UserCookieManager.WBStoreId, "Create and modify your default addresses");
                            BCDetail.ImageURL = "<i class='fa fa-truck'></i>";
                            BCDetail.PageNavigateURl = "/BillingShippingAddressManager";
                            BCDashBordItems.Add(BCDetail);
                            BCDetail = new DashboardViewModel(10);
                        }

                      

                    }

                    //   ViewData["rptStorePreferences"] = StorePrefDashBordItems.OrderBy(g => g.SortOrder).ToList();

                }
                ViewData["rptBrokerCorpDasHBItems"] = BCDashBordItems.OrderBy(g => g.SortOrder).ToList();

                ViewBag.ErrorMes = 1;

                return View("PartialViews/Dashboard");
            }
            else
            {
                Response.Redirect("/");
                return null;
            }
        }

        //public string UpdateSavedDesignCount()
        //{
        //    return "(" + OrderManager.GetSavedDesignCountByContactId(UserCookieManager.ContactID).ToString() + ")";
        //}

        public string UpdateUsersCount()
        {
            return "(" + _myCompanyService.GetContactCountByCompanyId(_webstoreclaimHelper.loginContactCompanyID()).ToString() + ")";
        }


        public string UpdateFavDesignCount()
        {
            return "(" + _myCompanyService.GetFavDesignCountByContactId(_webstoreclaimHelper.loginContactID()).ToString() + ")";
        }

        public string UpdateOrdersInProductionCount()
        {
            SearchOrderViewModel SearchOrder = new SearchOrderViewModel();
            
               List<Status> statusList = _StatusService.GetStatusListByStatusTypeID(2);
               List<Status> list = new List<Status>();
               List<Order> ordersList = new List<Order>();
              SearchOrder.DDOderStatus = new SelectList(statusList, "StatusId", "StatusName");

              if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
              {
                  ordersList = _orderservice.GetAllCorpOrders(_webstoreclaimHelper.loginContactCompanyID(), 0, SearchOrder.FromData, SearchOrder.ToDate, SearchOrder.poSearch, false, _webstoreclaimHelper.loginContactTerritoryID());
              }
              else if(UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
              {
                  ordersList = _orderservice.GetAllCorpOrders(_webstoreclaimHelper.loginContactCompanyID(), 0, SearchOrder.FromData, SearchOrder.ToDate, SearchOrder.poSearch, true, _webstoreclaimHelper.loginContactTerritoryID());
              }
              else
              {
                  ordersList = _orderservice.GetOrdersListByContactID(_webstoreclaimHelper.loginContactID(), 0, SearchOrder.FromData, SearchOrder.ToDate, SearchOrder.poSearch, 0, 0);
              }

              return "(" + ordersList.Count+ ")";
        }


        public string CorpCustomerPendingOrdersCountForManagers()
        {
            return "(" + _myCompanyService.GetPendingOrdersCountByTerritory(_webstoreclaimHelper.loginContactID(), OrderStatus.PendingCorporateApprovel, Convert.ToInt32(_webstoreclaimHelper.loginContactTerritoryID())).ToString() + ")";
        }
        public string CorpCustomerPendingOrdersCount()
        {
            long TotalPendingOrders=0;
            if (_webstoreclaimHelper.isUserLoggedIn())
            {
                bool ApproveOrders = false;
                CompanyContact LoginContact = _myCompanyService.GetContactByID(_webstoreclaimHelper.loginContactID());
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
                 List<Order> ManagerordersList = new List<Order>();
                 List<Order> ordersList = _myCompanyService.GetPendingApprovelOrdersList(_webstoreclaimHelper.loginContactID(), ApproveOrders);
                 if (ordersList == null || ordersList.Count == 0)
                 {
                     // do nothing
                 }
                 else
                 {
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
                        
                        TotalPendingOrders = ManagerordersList.Count;
                    }
                   }
                     else
                     {
                        
                        TotalPendingOrders = ordersList.Count;
                     
                     }
                 }

             }
            return "(" + TotalPendingOrders + ")";
        }

            
        

        public string AllCorpOrdersCount()
        {
            return "(" + _myCompanyService.GetAllOrdersCount(_webstoreclaimHelper.loginContactID()).ToString() + ")";
        }


        public string MyOrders()
        {
            return _myCompanyService.AllOrders(_webstoreclaimHelper.loginContactID(), _webstoreclaimHelper.loginContactCompanyID()).ToString();
        }

        //[HttpPost]
        //public ActionResult Index(DashBoardViewModel viewModel)
        //{

        //    string password = _myCompanyService.GetPasswordByContactID(_webstoreclaimHelper.loginContactID());
        //    if (_myCompanyService.VerifyHashSha1(viewModel.CurrentPassword, password) == true)
        //    {
        //        if(_myCompanyService.SaveResetPassword(_webstoreclaimHelper.loginContactID(),viewModel.NewPassword))
        //        {
        //            // PassMatchEM.Style.Add("display", "block");
        //            // PassMatchEM.Text ="Password is changed successfully.";

        //            //hfErrorMes.Value = "2";
        //           // FireResetPasswordSaveEvent();
        //        }

        //    }

        //    viewModel.NewPassword = string.Empty;
        //    viewModel.ConfirmPassword = string.Empty;
        //    viewModel.CurrentPassword = string.Empty;
        //    return View("PartialViews/Dashboard", viewModel);
        //}
        public ActionResult ResetPassword(string CurrentPassword, string NewPassword)
        {
            string password = _myCompanyService.GetPasswordByContactID(_webstoreclaimHelper.loginContactID());
            if (_myCompanyService.VerifyHashSha1(CurrentPassword, password) == true)
            {
                if (_myCompanyService.SaveResetPassword(_webstoreclaimHelper.loginContactID(), NewPassword))
                {
                    // PassMatchEM.Style.Add("display", "block");
                    // PassMatchEM.Text ="Password is changed successfully.";
                    ViewBag.ErrorMes = 2;
                    Response.Redirect("/");
                    return null;
                    //hfErrorMes.Value = "2";
                    // FireResetPasswordSaveEvent();
                }
                else
                {
                    ViewBag.ErrorMes = 5;
                    return PartialView("PartialViews/Dashboard");
                }
            }
            else
            {
                //PopulateDashboard();
                ViewBag.ErrorMes = 5;
                return PartialView("PartialViews/Dashboard");
            }
            //return View("Index/");
        }
        [HttpPost]
        public JsonResult GetPassWord(string CurrentPassword)
        {
            string password = _myCompanyService.GetPasswordByContactID(_webstoreclaimHelper.loginContactID());
            if (_myCompanyService.VerifyHashSha1(CurrentPassword, password) == true)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        //public void PopulateDashboard()
        //{
        //    if (_webstoreclaimHelper.isUserLoggedIn())
        //    {
        //        List<DashboardViewModel> DashBordItems = new List<DashboardViewModel>();

        //        DashboardViewModel Detail = new DashboardViewModel(1);

        //        // Contact Details
        //        Detail.Name = "My Profile"; //(string)GetGlobalResourceObject("MyResource", "ltrlcontactdetails");
        //        Detail.Description = "Change your profile picture and settings"; //(string)GetGlobalResourceObject("MyResource", "ltrlupdateurcontactdeatails");
        //        Detail.ImageURL = "<i class='fa fa-user'></i>";
        //        Detail.PageNavigateURl = "/ContactDetail";
        //        Detail.IsChangePassword = false;
        //        DashBordItems.Add(Detail);


        //        Detail = new DashboardViewModel(5);
        //        // Reset Password
        //        Detail.Name = "Change Password";//(string)GetGlobalResourceObject("MyResource", "ltrlchangepassword"\);
        //        Detail.Description = "change your current password"; //(string)GetGlobalResourceObject("MyResource", "ltrlresetnchangeaccpassword");
        //        Detail.ImageURL = "<i class='fa fa-key'></i>";
        //        Detail.PageNavigateURl = "";
        //        Detail.IsChangePassword = true;
        //        DashBordItems.Add(Detail);


        //        Detail = new DashboardViewModel(6);
        //        Detail.Name = "Cart"; //(string)GetGlobalResourceObject("MyResource", "ltrlshoppingcart");
        //        Detail.Description = "View or edit your cart details";// (string)GetGlobalResourceObject("MyResource", "ltrlviewitemnshppngcart");
        //        Detail.ImageURL = "<i class='fa fa-shopping-cart'></i>";
        //        Detail.IsChangePassword = false;

        //        Detail.PageNavigateURl = "/ShopCart";

        //        DashBordItems.Add(Detail);
        //        // Saved Desgn
        //        Detail = new DashboardViewModel(4);
        //        Detail.Name = "Saved Design";// (string)GetGlobalResourceObject("MyResource", "ltrlsavedesign") + UpdateSavedDesignCount();
        //        Detail.Description = "View or reorder your saved design"; //(string)GetGlobalResourceObject("MyResource", "ltrlmanagenviewsd");
        //        Detail.ImageURL = "<i class='fa fa-pencil-square-o'></i>";
        //        Detail.PageNavigateURl = "/SavedDesigns";
        //        Detail.IsChangePassword = false;
        //        DashBordItems.Add(Detail);



        //        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail || (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.User)))
        //        {
        //            Detail = new DashboardViewModel(3);
        //            // Address Details
        //            Detail.Name = "Address Manager";// (string)GetGlobalResourceObject("MyResource", "anchorAddressMgr");
        //            Detail.Description = "Create and modify your default addresses";
        //            Detail.ImageURL = "<i class='fa fa-rocket'></i>";
        //            Detail.PageNavigateURl = "/BillingShippingAddressManager";
        //            Detail.IsChangePassword = false;
        //            DashBordItems.Add(Detail);

        //        }


               


        //        /*************************************************Broker / Corporate Orders  ***********************************/
        //        List<DashboardViewModel> BCDashBordItems = new List<DashboardViewModel>();
        //        DashboardViewModel BCDetail = new DashboardViewModel(1);
        //        if (((UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)) || (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.User))
        //        {

        //            //CorpDiv.Visible = false;
        //            // My Order History
        //            BCDetail = new DashboardViewModel(1);
        //            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
        //            {
        //                BCDetail.Name = "My Orders" + " (" + MyOrders() + ")"; //(string)GetGlobalResourceObject("MyResource", "lblORderTracking") + " (" + MyOrders() + ")";
        //                BCDetail.Description = "View order details and attachments";
        //            }
        //            else
        //            {
        //                BCDetail.Name = "My Orders" + " (" + MyOrders() + ")";//(string)GetGlobalResourceObject("MyResource", "lblOrderList") + " (" + MyOrders() + ")";
        //                BCDetail.Description = "View order details and attachments"; //(string)GetGlobalResourceObject("MyResource", "ltrlviewrocompletedo");
        //            }
        //            BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
        //            BCDetail.PageNavigateURl = "/ProductOrderHistory";
        //            BCDashBordItems.Add(BCDetail);

        //        }

        //        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager))
        //        {
        //            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
        //            {
        //                BCDetail = new DashboardViewModel(2);
        //                // All Order History

        //                BCDetail.Name = "All Orders" + AllCorpOrdersCount(); // (string)GetGlobalResourceObject("MyResource", "lblAllOrderss");

        //                BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "ltrlviewrocompletedo");
        //                BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
        //                BCDetail.PageNavigateURl = "/Orders.aspx?OrderStatus=All";
        //                BCDetail.IsChangePassword = false;
        //                BCDashBordItems.Add(BCDetail);
        //                //// Pending Approvals
        //                BCDetail = new DashboardViewModel(3);

        //                BCDetail.Description = "Orders Pending Approval"; //(string)GetGlobalResourceObject("MyResource", "lblOrderApprovalDesc");
        //                BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
        //                if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
        //                {
        //                    BCDetail.Name = "Orders Pending Approval" + CorpCustomerPendingOrdersCountForManagers(); //(string)GetGlobalResourceObject("MyResource", "lblPendingApprovalsBtn") + CorpCustomerPendingOrdersCountForManagers();
        //                }
        //                else
        //                {
        //                    BCDetail.Name = "Orders Pending Approval" + CorpCustomerPendingOrdersCount(); // (string)GetGlobalResourceObject("MyResource", "lblPendingApprovalsBtn") + CorpCustomerPendingOrdersCount();
        //                }

        //                BCDetail.PageNavigateURl = "#";

        //                BCDetail.IsChangePassword = false;
        //                BCDashBordItems.Add(BCDetail);
        //                BCDetail = new DashboardViewModel(3);
        //                // Order In production
        //                BCDetail.Name = "Products Order History" + UpdateOrdersInProductionCount(); // (string)GetGlobalResourceObject("MyResource", "lblOrderProductnBtn") + UpdateOrdersInProductionCount();
        //                BCDetail.Description = "Description";//(string)GetGlobalResourceObject("MyResource", "lblViewCurOrderStatus");
        //                BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
        //                BCDetail.PageNavigateURl = "/ProductsOrdersHistory.aspx?OrderStatus=In Production";
        //                BCDetail.IsChangePassword = false;
        //                BCDashBordItems.Add(BCDetail);
        //                if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
        //                {
        //                    BCDetail = new DashboardViewModel(4);
        //                    // User manger
        //                    BCDetail.Name = "User Manager";// (string)GetGlobalResourceObject("MyResource", "anchorUserMgr");
        //                    BCDetail.Description = "Create and modify your webstore admin and manager users";
        //                    BCDetail.ImageURL = "<i class='fa fa-users'></i>";
        //                    BCDetail.PageNavigateURl = "/UserManager.aspx";
        //                    BCDashBordItems.Add(BCDetail);
        //                }
        //            }

        //        }
        //        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
        //        {

        //            /*************************************************Store Preferneces ***********************************/


        //            List<DashboardViewModel> StorePrefDashBordItems = new List<DashboardViewModel>();


        //            // Payment Preferences
        //            BCDetail = new DashboardViewModel(7);
        //            BCDetail.Name = "Payments Manager"; //(string)GetGlobalResourceObject("MyResource", "lblPayPalSet");
        //            BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblPaypalDesc");
        //            BCDetail.ImageURL = "<i class='fa fa-credit-card'></i>";
        //            BCDetail.PageNavigateURl = "/PaymentPreferences.aspx";
        //            StorePrefDashBordItems.Add(BCDetail);

        //            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
        //            {


        //                BCDetail = new DashboardViewModel(5);
        //                // Banner Manager
        //                BCDetail.Name = "Banner Manager";// (string)GetGlobalResourceObject("MyResource", "lblBannerMgr");
        //                BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblBannerDesc");
        //                BCDetail.ImageURL = "<i class='fa fa-tasks'></i>";
        //                BCDetail.PageNavigateURl = "/BrokerBannerWiget.aspx";
        //                StorePrefDashBordItems.Add(BCDetail);



        //                BCDetail = new DashboardViewModel(6);
        //                // Reports and Stats
        //                BCDetail.Name = "Secondary Page Manager";// (string)GetGlobalResourceObject("MyResource", "lblScendMgr");
        //                BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblSecondDesc");
        //                BCDetail.ImageURL = "<i class='fa fa-file-text'></i>";
        //                BCDetail.PageNavigateURl = "SecondaryPageManager.aspx";
        //                StorePrefDashBordItems.Add(BCDetail);

        //                BCDetail = new DashboardViewModel(8);

        //                // Reports and Stats
        //                BCDetail.Name = "Social Manager";// (string)GetGlobalResourceObject("MyResource", "lblSocialMgr");
        //                BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblSocialDesc");
        //                BCDetail.ImageURL = "<i class='fa fa-key'></i>";
        //                BCDetail.PageNavigateURl = "#";
        //                StorePrefDashBordItems.Add(BCDetail);


        //                BCDetail = new DashboardViewModel(9);

        //                BCDetail.Name = "Keyword MAnager";// (string)GetGlobalResourceObject("MyResource", "lblKeyWords");
        //                BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblKayWordDesc");
        //                BCDetail.ImageURL = "<i class='fa fa-file-text'></i>";
        //                BCDetail.PageNavigateURl = "#";
        //                StorePrefDashBordItems.Add(BCDetail);


        //                BCDetail = new DashboardViewModel(10);

        //                BCDetail.Name = "Google analytics"; //(string)GetGlobalResourceObject("MyResource", "lblGoogleAnalytics");
        //                BCDetail.Description = "Description"; //(string)GetGlobalResourceObject("MyResource", "lblGAnalyticDesc");
        //                BCDetail.ImageURL = "<i class='fa fa-file-text'></i>";
        //                BCDetail.PageNavigateURl = "#";
        //                StorePrefDashBordItems.Add(BCDetail);
        //            }
        //            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
        //            {
        //                if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
        //                {
        //                    BCDetail = new DashboardViewModel(2);
        //                    // User manger
        //                    BCDetail.Name = "Shipping Address Manager"; //(string)GetGlobalResourceObject("MyResource", "anchorAddressMgr");
        //                    BCDetail.Description = "Create and modify your default addresses";
        //                    BCDetail.ImageURL = "<i class='fa fa-truck'></i>";
        //                    BCDetail.PageNavigateURl = "/BillingShippingAddressManager";
        //                    StorePrefDashBordItems.Add(BCDetail);
        //                    BCDetail = new DashboardViewModel(10);
        //                }

        //                if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
        //                {

        //                    // User manger
        //                    BCDetail.Name = "User Manager";// (string)GetGlobalResourceObject("MyResource", "anchorUserMgr");
        //                    BCDetail.Description = "Create and modify your webstore admin and manager users";
        //                    BCDetail.ImageURL = "<i class='fa fa-users'></i>";
        //                    BCDetail.PageNavigateURl = "/UserManager.aspx";
        //                    StorePrefDashBordItems.Add(BCDetail);
        //                }

        //            }

        //            ViewData["rptStorePreferences"] = StorePrefDashBordItems.OrderBy(g => g.SortOrder).ToList();

        //        }
        //        ViewData["rptBrokerCorpDasHBItems"] = BCDashBordItems.OrderBy(g => g.SortOrder).ToList();
        //        ViewData["rptRetailDashboardItem"] = DashBordItems.OrderBy(g => g.SortOrder).ToList();

        //    }
        //}
    }
}