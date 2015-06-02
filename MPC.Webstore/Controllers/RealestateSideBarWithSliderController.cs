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
    public class RealestateSideBarWithSliderController : Controller
    {
        // GET: RealestateSideBarWithSlider
        private readonly ICompanyService _MyCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly IListingService _ListingService;
        public RealestateSideBarWithSliderController(ICompanyService _MyCompanyService, IWebstoreClaimsHelperService _webstoreAuthorizationChecker, IListingService _ListingService)
        {
           this._MyCompanyService=_MyCompanyService;
           this._webstoreAuthorizationChecker = _webstoreAuthorizationChecker;
           this._ListingService = _ListingService;
        }
        public ActionResult Index()
        {
            if (UserCookieManager.WEBStoreMode==(int)StoreMode.Corp)
            {
               // tbl_contacts contactUser = SessionParameters.CustomerContact;
                 CompanyContact contactUser = _MyCompanyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
                if (contactUser != null)
                {
                     ViewBag.lblUSerName = string.Format("{0} {1}", "Hi, " + contactUser.FirstName, contactUser.LastName);
                    if (contactUser.image != null)
                    {
                        ViewBag.imgUserProfileSrc = contactUser.image;

                    }
                    else
                    {
                        ViewBag.imgUserProfileSrc = string.Format("{0}", "Content/images/" + "realplaceholder.gif");
                    }
                }

                if (_webstoreAuthorizationChecker.isUserLoggedIn())
                {
                    List<ProductCategory> lstParentCategories = new List<ProductCategory>();

                    if (contactUser != null && contactUser.ContactRoleId == Convert.ToInt32(Roles.Adminstrator))
                    {
                        lstParentCategories = _MyCompanyService.GetAllParentCorporateCatalog((int)contactUser.CompanyId);
                    }
                    else
                    {
                        lstParentCategories = _MyCompanyService.GetAllParentCorporateCatalogByTerritory((int)contactUser.CompanyId,(int)_webstoreAuthorizationChecker.loginContactID());
                    }


                    if (lstParentCategories.Count > 0 && lstParentCategories.Count > 0)
                    {
                       ViewBag.rptParentCategoryNames = lstParentCategories;
                    }
                }
                List<Listing> lstProperties = new List<Listing>();

                ViewBag.lstProperties = _ListingService.GetPropertiesByContactCompanyID(UserCookieManager.WBStoreId);
                ViewBag.AllCategories = _MyCompanyService.GetAllCategories();
                ViewBag.AllListingImages = _ListingService.GetAllListingImages();
            }
            return PartialView("PartialViews/RealestateSideBarWithSlider");
        }
        [HttpPost]
        public void LogOut()
        {
            System.Web.HttpContext.Current.Response.Cookies["ShowPrice"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["CanEditProfile"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["WEBLastName"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["WEBFirstName"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies["WEBOrderId"].Expires = DateTime.Now.AddDays(-1);
            UserCookieManager.isRegisterClaims = 2;
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            {
                Response.Redirect("/Login");
            }
            else
            {
                Response.Redirect("/");
            }
            //Response.Redirect("/"); 
            
        }
        
        
    }
}