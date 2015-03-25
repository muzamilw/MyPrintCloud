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

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DashboardController(IWebstoreClaimsHelperService webstoreClaimHelper, ICompanyService myCompanyService)
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
        }

        #endregion
        // GET: Dashboard
        public ActionResult Index()
        {
            if (_webstoreclaimHelper.isUserLoggedIn())
            {
                List<DashboardViewModel> DashBordItems = new List<DashboardViewModel>();
                DashboardViewModel Detail = new DashboardViewModel(1);

                // Contact Details
                Detail.Name = "Your Profile"; //(string)GetGlobalResourceObject("MyResource", "ltrlcontactdetails");
                Detail.Description = "Change your profile picture and settings"; //(string)GetGlobalResourceObject("MyResource", "ltrlupdateurcontactdeatails");
                Detail.ImageURL = "<i class='fa fa-user'></i>";
                Detail.PageNavigateURl = "/ContactDetail";
                Detail.IsChangePassword = false;
                DashBordItems.Add(Detail);


                Detail = new DashboardViewModel(3);
                // Reset Password
                Detail.Name = "Change Password";//(string)GetGlobalResourceObject("MyResource", "ltrlchangepassword");
                Detail.Description = "change your current password"; //(string)GetGlobalResourceObject("MyResource", "ltrlresetnchangeaccpassword");
                Detail.ImageURL = "<i class='fa fa-key'></i>";
                Detail.PageNavigateURl = "";
                Detail.IsChangePassword = true;
                DashBordItems.Add(Detail);


                if (UserCookieManager.StoreMode == (int)StoreMode.Retail || (UserCookieManager.StoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() != (int)Roles.Adminstrator))
                {
                    // Shooping Cart details
                    Detail = new DashboardViewModel(2);
                    Detail.Name = "Shopping Cart"; //(string)GetGlobalResourceObject("MyResource", "ltrlshoppingcart");
                    Detail.Description = "";// (string)GetGlobalResourceObject("MyResource", "ltrlviewitemnshppngcart");
                    Detail.ImageURL = "<i class='fa fa-shopping-cart'></i>";
                    Detail.IsChangePassword = false;

                    Detail.PageNavigateURl = "/ShopCart";

                    DashBordItems.Add(Detail);
                    // Saved Desgn
                    Detail = new DashboardViewModel(6);
                    Detail.Name = "Saved Design";// (string)GetGlobalResourceObject("MyResource", "ltrlsavedesign") + UpdateSavedDesignCount();
                    Detail.Description = "View or reorder your saved design"; //(string)GetGlobalResourceObject("MyResource", "ltrlmanagenviewsd");
                    Detail.ImageURL = "<i class='fa fa-pencil-square-o'></i>";
                    Detail.PageNavigateURl = "#";
                    Detail.IsChangePassword = false;
                    DashBordItems.Add(Detail);

                  
                }


                if (UserCookieManager.StoreMode == (int)StoreMode.Retail || (UserCookieManager.StoreMode == (int)StoreMode.Corp && (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.User)))
                {
                    Detail = new DashboardViewModel(5);
                    // Address Details
                    Detail.Name = "Address Manager";// (string)GetGlobalResourceObject("MyResource", "anchorAddressMgr");
                    Detail.Description = "Create and modify your default addresses";
                    Detail.ImageURL = "<i class='fa fa-rocket'></i>";
                    Detail.PageNavigateURl = "/BillingShippingAddressManager";
                    Detail.IsChangePassword = false;
                    DashBordItems.Add(Detail);

                }


              
       

                /*************************************************Broker / Corporate Orders  ***********************************/
               
                if (((UserCookieManager.StoreMode == (int)StoreMode.Retail)) || (UserCookieManager.StoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.User))
                {
                  
                    //CorpDiv.Visible = false;
                    // My Order History
                    Detail = new DashboardViewModel(1);
                    if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
                    {
                        Detail.Name = "My Orders" + " (" + MyOrders() + ")"; //(string)GetGlobalResourceObject("MyResource", "lblORderTracking") + " (" + MyOrders() + ")";
                        Detail.Description = "View order details and attachments";
                    }
                    else
                    {
                        Detail.Name = "My Orders" + " (" + MyOrders() + ")";//(string)GetGlobalResourceObject("MyResource", "lblOrderList") + " (" + MyOrders() + ")";
                        Detail.Description = "View order details and attachments"; //(string)GetGlobalResourceObject("MyResource", "ltrlviewrocompletedo");
                    }
                    Detail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                    Detail.PageNavigateURl = "/ProductOrderHistory";
                    DashBordItems.Add(Detail);

                }

                if (UserCookieManager.StoreMode == (int)StoreMode.Corp && (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager))
                {
                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        Detail = new DashboardViewModel(2);
                        // All Order History

                        Detail.Name = "All Orders" + AllCorpOrdersCount(); // (string)GetGlobalResourceObject("MyResource", "lblAllOrderss");

                        Detail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "ltrlviewrocompletedo");
                        Detail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        Detail.PageNavigateURl = "/Orders.aspx?OrderStatus=All";
                        Detail.IsChangePassword = false;
                        DashBordItems.Add(Detail);
                        //// Pending Approvals
                        Detail = new DashboardViewModel(3);

                        Detail.Description = "Orders Pending Approval"; //(string)GetGlobalResourceObject("MyResource", "lblOrderApprovalDesc");
                        Detail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
                        {
                            Detail.Name = "Orders Pending Approval" + CorpCustomerPendingOrdersCountForManagers(); //(string)GetGlobalResourceObject("MyResource", "lblPendingApprovalsBtn") + CorpCustomerPendingOrdersCountForManagers();
                        }
                        else
                        {
                            Detail.Name = "Orders Pending Approval" + CorpCustomerPendingOrdersCount(); // (string)GetGlobalResourceObject("MyResource", "lblPendingApprovalsBtn") + CorpCustomerPendingOrdersCount();
                        }

                        Detail.PageNavigateURl = "/ProductPendingOrders";

                        Detail.IsChangePassword = false;
                        DashBordItems.Add(Detail);
                        Detail = new DashboardViewModel(3);
                        // Order In production
                        Detail.Name = "Products Order History" + UpdateOrdersInProductionCount(); // (string)GetGlobalResourceObject("MyResource", "lblOrderProductnBtn") + UpdateOrdersInProductionCount();
                        Detail.Description = "Description";//(string)GetGlobalResourceObject("MyResource", "lblViewCurOrderStatus");
                        Detail.ImageURL= "<i class='fa fa-file-text-o'></i>";
                        Detail.PageNavigateURl = "/ProductsOrdersHistory.aspx?OrderStatus=In Production";
                        Detail.IsChangePassword = false;
                        DashBordItems.Add(Detail);
                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
                        {
                            Detail = new DashboardViewModel(4);
                            // User manger
                            Detail.Name = "User Manager";// (string)GetGlobalResourceObject("MyResource", "anchorUserMgr");
                            Detail.Description = "Create and modify your webstore admin and manager users";
                            Detail.ImageURL = "<i class='fa fa-users'></i>";
                            Detail.PageNavigateURl = "/UserManager.aspx";
                            DashBordItems.Add(Detail);
                        }
                    }

                }
                if (UserCookieManager.StoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                {

                    /*************************************************Store Preferneces ***********************************/

                  
                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                        {
                            Detail = new DashboardViewModel(2);
                            // User manger
                            Detail.Name = "Shipping Address Manager"; //(string)GetGlobalResourceObject("MyResource", "anchorAddressMgr");
                            Detail.Description = "Create and modify your default addresses";
                            Detail.ImageURL = "<i class='fa fa-truck'></i>";
                            Detail.PageNavigateURl = "/BillingShippingAddressManager";
                            DashBordItems.Add(Detail);
                            Detail = new DashboardViewModel(10);
                        }

                    }


                }

                ViewData["rptRetailDashboardItem"] = DashBordItems.OrderBy(g => g.SortOrder).ToList();


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
            return "(" + _myCompanyService.GetOrdersCountByStatus(_webstoreclaimHelper.loginContactID(), OrderStatus.InProduction).ToString() + ")";
        }
     
     
        public string CorpCustomerPendingOrdersCountForManagers()
        {
            return "(" + _myCompanyService.GetPendingOrdersCountByTerritory(_webstoreclaimHelper.loginContactID(), OrderStatus.PendingCorporateApprovel, Convert.ToInt32(_webstoreclaimHelper.loginContactTerritoryID())).ToString() + ")";
        }
        public string CorpCustomerPendingOrdersCount()
        {
            return "(" + _myCompanyService.GetAllPendingOrders(_webstoreclaimHelper.loginContactID(), OrderStatus.PendingCorporateApprovel).ToString() + ")";
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
        public ActionResult ResetPassword(string CurrentPassword,string NewPassword)
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
                PopulateDashboard();
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

        public void PopulateDashboard()
        {
            if (_webstoreclaimHelper.isUserLoggedIn())
            {
                List<DashboardViewModel> DashBordItems = new List<DashboardViewModel>();
                DashboardViewModel Detail = new DashboardViewModel(1);

                // Contact Details
                Detail.Name = "Your Profile"; //(string)GetGlobalResourceObject("MyResource", "ltrlcontactdetails");
                Detail.Description = "Change your profile picture and settings"; //(string)GetGlobalResourceObject("MyResource", "ltrlupdateurcontactdeatails");
                Detail.ImageURL = "<i class='fa fa-user'></i>";
                Detail.PageNavigateURl = "/ContactDetail";
                Detail.IsChangePassword = false;
                DashBordItems.Add(Detail);


                Detail = new DashboardViewModel(3);
                // Reset Password
                Detail.Name = "Change Password";//(string)GetGlobalResourceObject("MyResource", "ltrlchangepassword"\);
                Detail.Description = "change your current password"; //(string)GetGlobalResourceObject("MyResource", "ltrlresetnchangeaccpassword");
                Detail.ImageURL = "<i class='fa fa-key'></i>";
                Detail.PageNavigateURl = "";
                Detail.IsChangePassword = true;
                DashBordItems.Add(Detail);


                if (UserCookieManager.StoreMode == (int)StoreMode.Retail || (UserCookieManager.StoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() != (int)Roles.Adminstrator))
                {

                    //Detail = new DashboardViewModel(4);
                    //// Quick Text details
                    //Detail.Name = "Quick Text Info";//(string)GetGlobalResourceObject("MyResource", "ltrlimgliblogoqt");
                    //Detail.Description = "Description"; //(string)GetGlobalResourceObject("MyResource", "ltrleditnuploadurdd");
                    //Detail.ImageURL = "<i class='fa fa-file-text'></i>";
                    //Detail.PageNavigateURl = "/UserQuickTextInfo.aspx";
                    //Detail.IsChangePassword = false;
                    //DashBordItems.Add(Detail);
                    // Shooping Cart details
                    Detail = new DashboardViewModel(2);
                    Detail.Name = "Shopping Cart"; //(string)GetGlobalResourceObject("MyResource", "ltrlshoppingcart");
                    Detail.Description = "";// (string)GetGlobalResourceObject("MyResource", "ltrlviewitemnshppngcart");
                    Detail.ImageURL = "<i class='fa fa-shopping-cart'></i>";
                    Detail.IsChangePassword = false;

                    Detail.PageNavigateURl = "/ShopCart";

                    DashBordItems.Add(Detail);
                    // Saved Desgn
                    Detail = new DashboardViewModel(6);
                    Detail.Name = "Saved Design";// (string)GetGlobalResourceObject("MyResource", "ltrlsavedesign") + UpdateSavedDesignCount();
                    Detail.Description = "View or reorder your saved design"; //(string)GetGlobalResourceObject("MyResource", "ltrlmanagenviewsd");
                    Detail.ImageURL = "<i class='fa fa-pencil-square-o'></i>";
                    Detail.PageNavigateURl = "#";
                    Detail.IsChangePassword = false;
                    DashBordItems.Add(Detail);

                    //Detail = new DashboardViewModel(7);
                    //// My Favorites Details 
                    //Detail.Name = "Favorite Designs";// (string)GetGlobalResourceObject("MyResource", "ltrlmyfavd") + UpdateFavDesignCount();
                    //Detail.Description = "Description"; // (string)GetGlobalResourceObject("MyResource", "ltrlviewdttulike");
                    //Detail.ImageURL = "<i class='fa fa-heart'></i>";
                    //Detail.PageNavigateURl = "/FavContactDesigns.aspx";
                    //Detail.IsChangePassword = false;
                    //DashBordItems.Add(Detail);

                }


                if (UserCookieManager.StoreMode == (int)StoreMode.Retail || (UserCookieManager.StoreMode == (int)StoreMode.Corp && (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.User)))
                {
                    Detail = new DashboardViewModel(5);
                    // Address Details
                    Detail.Name = "Address Manager";// (string)GetGlobalResourceObject("MyResource", "anchorAddressMgr");
                    Detail.Description = "Create and modify your default addresses";
                    Detail.ImageURL = "<i class='fa fa-rocket'></i>";
                    Detail.PageNavigateURl = "/BillingShippingAddressManager";
                    Detail.IsChangePassword = false;
                    DashBordItems.Add(Detail);

                }


                ViewData["rptRetailDashboardItem"] = DashBordItems.OrderBy(g => g.SortOrder).ToList();



                /*************************************************Broker / Corporate Orders  ***********************************/
                //List<DashboardViewModel> DashBordItems = new List<DashboardViewModel>();
                //DashboardViewModel Detail = new DashboardViewModel(1);
                if (((UserCookieManager.StoreMode == (int)StoreMode.Retail)) || (UserCookieManager.StoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.User))
                {

                    //CorpDiv.Visible = false;
                    // My Order History
                    Detail = new DashboardViewModel(1);
                    if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
                    {
                        Detail.Name = "My Orders" + " (" + MyOrders() + ")"; //(string)GetGlobalResourceObject("MyResource", "lblORderTracking") + " (" + MyOrders() + ")";
                        Detail.Description = "View order details and attachments";
                    }
                    else
                    {
                        Detail.Name = "My Orders" + " (" + MyOrders() + ")";//(string)GetGlobalResourceObject("MyResource", "lblOrderList") + " (" + MyOrders() + ")";
                        Detail.Description = "View order details and attachments"; //(string)GetGlobalResourceObject("MyResource", "ltrlviewrocompletedo");
                    }
                    Detail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                    Detail.PageNavigateURl = "/ProductOrderHistory";
                    DashBordItems.Add(Detail);

                }

                if (UserCookieManager.StoreMode == (int)StoreMode.Corp && (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager))
                {
                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        Detail = new DashboardViewModel(2);
                        // All Order History

                        Detail.Name = "All Orders" + AllCorpOrdersCount(); // (string)GetGlobalResourceObject("MyResource", "lblAllOrderss");

                        Detail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "ltrlviewrocompletedo");
                        Detail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        Detail.PageNavigateURl = "/Orders.aspx?OrderStatus=All";
                        Detail.IsChangePassword = false;
                        DashBordItems.Add(Detail);
                        //// Pending Approvals
                        Detail = new DashboardViewModel(3);

                        Detail.Description = "Orders Pending Approval"; //(string)GetGlobalResourceObject("MyResource", "lblOrderApprovalDesc");
                        Detail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
                        {
                            Detail.Name = "Orders Pending Approval" + CorpCustomerPendingOrdersCountForManagers(); //(string)GetGlobalResourceObject("MyResource", "lblPendingApprovalsBtn") + CorpCustomerPendingOrdersCountForManagers();
                        }
                        else
                        {
                            Detail.Name = "Orders Pending Approval" + CorpCustomerPendingOrdersCount(); // (string)GetGlobalResourceObject("MyResource", "lblPendingApprovalsBtn") + CorpCustomerPendingOrdersCount();
                        }

                        Detail.PageNavigateURl = "#";

                        Detail.IsChangePassword = false;
                        DashBordItems.Add(Detail);
                        Detail = new DashboardViewModel(3);
                        // Order In production
                        Detail.Name = "Products Order History" + UpdateOrdersInProductionCount(); // (string)GetGlobalResourceObject("MyResource", "lblOrderProductnBtn") + UpdateOrdersInProductionCount();
                        Detail.Description = "Description";//(string)GetGlobalResourceObject("MyResource", "lblViewCurOrderStatus");
                        Detail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        Detail.PageNavigateURl = "/ProductsOrdersHistory.aspx?OrderStatus=In Production";
                        Detail.IsChangePassword = false;
                        DashBordItems.Add(Detail);
                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
                        {
                            Detail = new DashboardViewModel(4);
                            // User manger
                            Detail.Name = "User Manager";// (string)GetGlobalResourceObject("MyResource", "anchorUserMgr");
                            Detail.Description = "Create and modify your webstore admin and manager users";
                            Detail.ImageURL = "<i class='fa fa-users'></i>";
                            Detail.PageNavigateURl = "/UserManager.aspx";
                            DashBordItems.Add(Detail);
                        }
                    }

                }
                if (UserCookieManager.StoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                {

                    /*************************************************Store Preferneces ***********************************/

                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {

                        Detail = new DashboardViewModel(5);
                        // Banner Manager
                        Detail.Name = "Banner Manager";// (string)GetGlobalResourceObject("MyResource", "lblBannerMgr");
                        Detail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblBannerDesc");
                        Detail.ImageURL = "<i class='fa fa-tasks'></i>";
                        Detail.PageNavigateURl = "/BrokerBannerWiget.aspx";
                        DashBordItems.Add(Detail);

                    }
                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                        {
                            Detail = new DashboardViewModel(2);
                            // User manger
                            Detail.Name = "Shipping Address Manager"; //(string)GetGlobalResourceObject("MyResource", "anchorAddressMgr");
                            Detail.Description = "Create and modify your default addresses";
                            Detail.ImageURL = "<i class='fa fa-truck'></i>";
                            Detail.PageNavigateURl = "/BillingShippingAddressManager";
                            DashBordItems.Add(Detail);
                            Detail = new DashboardViewModel(10);
                        }

                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
                        {

                            // User manger
                            Detail.Name = "User Manager";// (string)GetGlobalResourceObject("MyResource", "anchorUserMgr");
                            Detail.Description = "Create and modify your webstore admin and manager users";
                            Detail.ImageURL = "<i class='fa fa-users'></i>";
                            Detail.PageNavigateURl = "/UserManager.aspx";
                           
                        }

                    }

                   

                }
            }
        }
    }
}