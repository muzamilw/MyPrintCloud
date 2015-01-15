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

namespace MPC.Webstore.Controllers
{
    public class LoginBarController : Controller
    {

        #region Private

        private readonly IWebstoreClaimsHelperService _webstoreclaimHelper;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LoginBarController(IWebstoreClaimsHelperService webstoreClaimHelper)
        {

            if (webstoreClaimHelper == null)
            {
                throw new ArgumentNullException("webstoreClaimHelper");
            }
            this._webstoreclaimHelper = webstoreClaimHelper;
        }

        #endregion
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

       
        // GET: LoginBar
        public ActionResult Index()
        {
            
            if (_webstoreclaimHelper.isUserLoggedIn())
            {
                ViewBag.isUserLoggedIn = true;
                ViewBag.LoginUserName = UserCookieManager.ContactFirstName + " " + UserCookieManager.ContactLastName;
            }
            else
            {
                ViewBag.isUserLoggedIn = false;
                ViewBag.LoginUserName = "";
            }
            return PartialView("PartialViews/LoginBar");
        }

        public ActionResult LogOut()
        {
            UserCookieManager.ContactFirstName = "";
            UserCookieManager.ContactLastName = "";
            UserCookieManager.ContactCanEditProfile = false;
            UserCookieManager.ShowPriceOnWebstore = true;
            UserCookieManager.isRegisterClaims = 2;
         
            //ControllerContext.HttpContext.Response.Redirect("/Home/Index");
            Response.Redirect("/"); //return //RedirectToAction("Index", "Home");
            return null;

        }
    }
}