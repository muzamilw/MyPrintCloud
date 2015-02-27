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
                    Detail.PageNavigateURl = "/SavedDesignes.aspx";
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
                    Detail.PageNavigateURl = "#";
                    Detail.IsChangePassword = false;
                    DashBordItems.Add(Detail);

                }


                ViewData["rptRetailDashboardItem"] = DashBordItems.OrderBy(g => g.SortOrder).ToList();

       

                /*************************************************Broker / Corporate Orders  ***********************************/
                List<DashboardViewModel> BCDashBordItems = new List<DashboardViewModel>();
                DashboardViewModel BCDetail = new DashboardViewModel(1);
                if (((UserCookieManager.StoreMode == (int)StoreMode.Retail)) || (UserCookieManager.StoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.User))
                {
                  
                    //CorpDiv.Visible = false;
                    // My Order History
                    BCDetail = new DashboardViewModel(1);
                    if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
                    {
                        BCDetail.Name = "My Orders" + " (" + MyOrders() + ")"; //(string)GetGlobalResourceObject("MyResource", "lblORderTracking") + " (" + MyOrders() + ")";
                        BCDetail.Description = "View order details and attachments";
                    }
                    else
                    {
                        BCDetail.Name = "My Orders" + " (" + MyOrders() + ")";//(string)GetGlobalResourceObject("MyResource", "lblOrderList") + " (" + MyOrders() + ")";
                        BCDetail.Description = "View order details and attachments"; //(string)GetGlobalResourceObject("MyResource", "ltrlviewrocompletedo");
                    }
                    BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                    BCDetail.PageNavigateURl = "/ProductOrderHistory";
                    BCDashBordItems.Add(BCDetail);

                }

                if (UserCookieManager.StoreMode == (int)StoreMode.Corp && (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager))
                {
                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        BCDetail = new DashboardViewModel(2);
                        // All Order History

                        BCDetail.Name = "All Orders" + AllCorpOrdersCount(); // (string)GetGlobalResourceObject("MyResource", "lblAllOrderss");

                        BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "ltrlviewrocompletedo");
                        BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        BCDetail.PageNavigateURl = "/Orders.aspx?OrderStatus=All";
                        BCDetail.IsChangePassword = false;
                        BCDashBordItems.Add(BCDetail);
                        //// Pending Approvals
                        BCDetail = new DashboardViewModel(3);

                        BCDetail.Description = "Orders Pending Approval"; //(string)GetGlobalResourceObject("MyResource", "lblOrderApprovalDesc");
                        BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
                        {
                            BCDetail.Name = "Orders Pending Approval" + CorpCustomerPendingOrdersCountForManagers(); //(string)GetGlobalResourceObject("MyResource", "lblPendingApprovalsBtn") + CorpCustomerPendingOrdersCountForManagers();
                        }
                        else
                        {
                            BCDetail.Name = "Orders Pending Approval" + CorpCustomerPendingOrdersCount(); // (string)GetGlobalResourceObject("MyResource", "lblPendingApprovalsBtn") + CorpCustomerPendingOrdersCount();
                        }

                        BCDetail.PageNavigateURl = "#";

                        BCDetail.IsChangePassword = false;
                        BCDashBordItems.Add(BCDetail);
                        BCDetail = new DashboardViewModel(3);
                        // Order In production
                        BCDetail.Name = "Products Order History" + UpdateOrdersInProductionCount(); // (string)GetGlobalResourceObject("MyResource", "lblOrderProductnBtn") + UpdateOrdersInProductionCount();
                        BCDetail.Description = "Description";//(string)GetGlobalResourceObject("MyResource", "lblViewCurOrderStatus");
                        BCDetail.ImageURL = "<i class='fa fa-file-text-o'></i>";
                        BCDetail.PageNavigateURl = "/ProductsOrdersHistory.aspx?OrderStatus=In Production";
                        BCDetail.IsChangePassword = false;
                        BCDashBordItems.Add(BCDetail);
                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
                        {
                            BCDetail = new DashboardViewModel(4);
                            // User manger
                            BCDetail.Name = "User Manager";// (string)GetGlobalResourceObject("MyResource", "anchorUserMgr");
                            BCDetail.Description = "Create and modify your webstore admin and manager users";
                            BCDetail.ImageURL = "<i class='fa fa-users'></i>";
                            BCDetail.PageNavigateURl = "/UserManager.aspx";
                            BCDashBordItems.Add(BCDetail);
                        }
                    }

                }
                if (UserCookieManager.StoreMode == (int)StoreMode.Corp && _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                {

                    /*************************************************Store Preferneces ***********************************/


                    List<DashboardViewModel> StorePrefDashBordItems = new List<DashboardViewModel>();


                    // Payment Preferences
                    BCDetail = new DashboardViewModel(7);
                    BCDetail.Name = "Payments Manager"; //(string)GetGlobalResourceObject("MyResource", "lblPayPalSet");
                    BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblPaypalDesc");
                    BCDetail.ImageURL = "<i class='fa fa-credit-card'></i>";
                    BCDetail.PageNavigateURl = "/PaymentPreferences.aspx";
                    StorePrefDashBordItems.Add(BCDetail);

                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {


                        BCDetail = new DashboardViewModel(5);
                        // Banner Manager
                        BCDetail.Name = "Banner Manager";// (string)GetGlobalResourceObject("MyResource", "lblBannerMgr");
                        BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblBannerDesc");
                        BCDetail.ImageURL = "<i class='fa fa-tasks'></i>";
                        BCDetail.PageNavigateURl = "/BrokerBannerWiget.aspx";
                        StorePrefDashBordItems.Add(BCDetail);



                        BCDetail = new DashboardViewModel(6);
                        // Reports and Stats
                        BCDetail.Name = "Secondary Page Manager";// (string)GetGlobalResourceObject("MyResource", "lblScendMgr");
                        BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblSecondDesc");
                        BCDetail.ImageURL = "<i class='fa fa-file-text'></i>";
                        BCDetail.PageNavigateURl = "SecondaryPageManager.aspx";
                        StorePrefDashBordItems.Add(BCDetail);

                        BCDetail = new DashboardViewModel(8);

                        // Reports and Stats
                        BCDetail.Name = "Social Manager";// (string)GetGlobalResourceObject("MyResource", "lblSocialMgr");
                        BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblSocialDesc");
                        BCDetail.ImageURL = "<i class='fa fa-key'></i>";
                        BCDetail.PageNavigateURl = "#";
                        StorePrefDashBordItems.Add(BCDetail);


                        BCDetail = new DashboardViewModel(9);

                        BCDetail.Name = "Keyword MAnager";// (string)GetGlobalResourceObject("MyResource", "lblKeyWords");
                        BCDetail.Description = "Description";// (string)GetGlobalResourceObject("MyResource", "lblKayWordDesc");
                        BCDetail.ImageURL = "<i class='fa fa-file-text'></i>";
                        BCDetail.PageNavigateURl = "#";
                        StorePrefDashBordItems.Add(BCDetail);


                        BCDetail = new DashboardViewModel(10);

                        BCDetail.Name = "Google analytics"; //(string)GetGlobalResourceObject("MyResource", "lblGoogleAnalytics");
                        BCDetail.Description = "Description"; //(string)GetGlobalResourceObject("MyResource", "lblGAnalyticDesc");
                        BCDetail.ImageURL = "<i class='fa fa-file-text'></i>";
                        BCDetail.PageNavigateURl = "#";
                        StorePrefDashBordItems.Add(BCDetail);
                    }
                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                        {
                            BCDetail = new DashboardViewModel(2);
                            // User manger
                            BCDetail.Name = "Shipping Address Manager"; //(string)GetGlobalResourceObject("MyResource", "anchorAddressMgr");
                            BCDetail.Description = "Create and modify your default addresses";
                            BCDetail.ImageURL = "<i class='fa fa-truck'></i>";
                            BCDetail.PageNavigateURl = "/ShippingAddressManager.aspx";
                            StorePrefDashBordItems.Add(BCDetail);
                            BCDetail = new DashboardViewModel(10);
                        }

                        if (_webstoreclaimHelper.loginContactRoleID() == (int)Roles.Adminstrator || _webstoreclaimHelper.loginContactRoleID() == (int)Roles.Manager)
                        {

                            // User manger
                            BCDetail.Name = "User Manager";// (string)GetGlobalResourceObject("MyResource", "anchorUserMgr");
                            BCDetail.Description = "Create and modify your webstore admin and manager users";
                            BCDetail.ImageURL = "<i class='fa fa-users'></i>";
                            BCDetail.PageNavigateURl = "/UserManager.aspx";
                            StorePrefDashBordItems.Add(BCDetail);
                        }

                    }

                    ViewData["rptStorePreferences"] = StorePrefDashBordItems.OrderBy(g => g.SortOrder).ToList();

                }
                ViewData["rptBrokerCorpDasHBItems"] = BCDashBordItems.OrderBy(g => g.SortOrder).ToList();


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
    }
}