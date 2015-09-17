using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class BlackAndWhiteHeaderMenuController : Controller
    {
        private readonly IWebstoreClaimsHelperService _webstoreclaimHelper;
         private readonly IItemService _itemService;
         private readonly ICompanyService _companyservice;
         public BlackAndWhiteHeaderMenuController(IWebstoreClaimsHelperService _webstoreclaimHelper, IItemService _itemService, ICompanyService _companyservice)
        {
          this._webstoreclaimHelper=_webstoreclaimHelper;
          this._itemService = _itemService;
          this._companyservice = _companyservice;
        }
        // GET: BlackAndWhiteHeaderMenu
        public ActionResult Index()
        {
            ViewBag.loggedInUser = Utils.GetKeyValueFromResourceFile("ltrlLoggedIn", UserCookieManager.WBStoreId, "Logged in") + " " + UserCookieManager.WEBContactFirstName + UserCookieManager.WEBContactLastName;
            if (_webstoreclaimHelper.isUserLoggedIn())
            {

                ViewBag.CartCount = string.Format("{0}", _itemService.GetCartItemsCount(_webstoreclaimHelper.loginContactID(), 0, _webstoreclaimHelper.loginContactCompanyID()).ToString());
            }
            else
            {

                ViewBag.CartCount = string.Format("{0}", _itemService.GetCartItemsCount(0, UserCookieManager.TemporaryCompanyId, 0).ToString());
            }
            ViewBag.OrderTotal = _companyservice.GetOrderTotalById(UserCookieManager.WEBOrderId);
            ViewBag.SavedDesignItmesTotal = _companyservice.GetSavedDesignCountByContactId(_webstoreclaimHelper.loginContactID());
            return View("PartialViews/BlackAndWhiteHeaderMenu");
        }
        public ActionResult LogOut()
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

                return null;
        }
    }
}