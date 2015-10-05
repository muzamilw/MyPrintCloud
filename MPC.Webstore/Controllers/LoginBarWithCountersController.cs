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
    public class LoginBarWithCountersController : Controller
    {

        private readonly IWebstoreClaimsHelperService _webstoreclaimHelper;

        private readonly IItemService _itemService;
        private readonly ICompanyService _Companyservice;
        // GET: LoginBarWithCounters
        public LoginBarWithCountersController(IWebstoreClaimsHelperService webstoreClaimHelper, IItemService itemService, ICompanyService _Companyservice)
        {
           
         this._webstoreclaimHelper = webstoreClaimHelper;
           this._itemService = itemService;
           this._Companyservice = _Companyservice;
        }
        public ActionResult Index()
        {
            if (_webstoreclaimHelper.isUserLoggedIn())
            {
                ViewBag.isUserLoggedIn = true;
                ViewBag.LoginUserName = UserCookieManager.WEBContactFirstName + " " + UserCookieManager.WEBContactLastName;//Response.Cookies["WEBFirstName"].Value; 
                ViewBag.CartCount = string.Format("{0}", _itemService.GetCartItemsCount(_webstoreclaimHelper.loginContactID(), 0, _webstoreclaimHelper.loginContactCompanyID()).ToString());
                ViewBag.totalSAveItmes = _Companyservice.GetSavedDesignCountByContactId(_webstoreclaimHelper.loginContactID());
            }
            else
            {
                ViewBag.isUserLoggedIn = false;
                ViewBag.LoginUserName = "";
                ViewBag.CartCount = string.Format("{0}", _itemService.GetCartItemsCount(0, UserCookieManager.TemporaryCompanyId, 0).ToString());
                ViewBag.totalSAveItmes = _Companyservice.GetSavedDesignCountByContactId(_webstoreclaimHelper.loginContactID());
            }
            return PartialView("PartialViews/LoginBarWithCounters");
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