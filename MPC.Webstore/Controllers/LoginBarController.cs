using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.Common;
using Microsoft.Owin.Security;
using System.Threading;
using MPC.Models.Common;

namespace MPC.Webstore.Controllers
{
    public class LoginBarController : Controller
    {

        #region Private

        private readonly IWebstoreClaimsHelperService _webstoreclaimHelper;

        private readonly IItemService _itemService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LoginBarController(IWebstoreClaimsHelperService webstoreClaimHelper, IItemService itemService)
        {

            if (webstoreClaimHelper == null)
            {
                throw new ArgumentNullException("webstoreClaimHelper");
            }
            this._webstoreclaimHelper = webstoreClaimHelper;

            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }
            this._itemService = itemService;
        }

        #endregion


        // GET: LoginBar
        public ActionResult Index()
        {

            if (_webstoreclaimHelper.isUserLoggedIn())
            {
                ViewBag.isUserLoggedIn = true;
                ViewBag.LoginUserName = UserCookieManager.WEBContactFirstName + " " + UserCookieManager.WEBContactLastName;//Response.Cookies["WEBFirstName"].Value; 
                ViewBag.CartCount = string.Format("{0}", _itemService.GetCartItemsCount(_webstoreclaimHelper.loginContactID(), 0, _webstoreclaimHelper.loginContactCompanyID()).ToString());
            }
            else
            {
                ViewBag.isUserLoggedIn = false;
                ViewBag.LoginUserName = "";
                ViewBag.CartCount = string.Format("{0}", _itemService.GetCartItemsCount(0, UserCookieManager.TemporaryCompanyId, 0).ToString());
            }
            return PartialView("PartialViews/LoginBar");
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