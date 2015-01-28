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
          //  UserCookieManager.removeAllCookies();
           // _webstoreclaimHelper.removeAuthenticationClaim();
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
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