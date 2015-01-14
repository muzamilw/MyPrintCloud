using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.Models;
using MPC.Webstore.SessionModels;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ResponseModels;
using System.Runtime;
using MPC.Models.Common;
namespace MPC.Webstore.Controllers
{
    public class LoginController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LoginController(ICompanyService myCompanyService, IWebstoreClaimsHelperService webstoreAuthorizationChecker)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            if (webstoreAuthorizationChecker == null)
            {
                throw new ArgumentNullException("webstoreAuthorizationChecker");
            }
            this._myCompanyService = myCompanyService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
        }

        #endregion

        [Dependency]
        public IWebstoreClaimsSecurityService ClaimsSecurityService { get; set; }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        // GET: Login
        public ActionResult Index(string FirstName, string LastName, string Email, string ReturnURL)
        {
            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();

            if ((baseResponse.Company.IsCustomer == (int)CustomerTypes.Corporate && baseResponse.Company.isAllowRegistrationFromWeb == true) || (baseResponse.Company.IsCustomer == 1))
            {
                ViewBag.AllowRegisteration = 1;
            }
            else
            {
                ViewBag.AllowRegisteration = 1;
            }
            if (string.IsNullOrEmpty(ReturnURL))
                ViewBag.ReturnURL = "Social";
            else  
                ViewBag.ReturnURL = ReturnURL;
       
            if (!string.IsNullOrEmpty(FirstName))
            {
                string returnUrl = string.Empty;

                CompanyContact user = new CompanyContact();

                if (!string.IsNullOrEmpty(Email))
                {
                    user = _myCompanyService.GetContactByEmail(Email);
                }
                else
                {
                    user = _myCompanyService.GetContactByFirstName(FirstName);
                }
                if (user != null)
                {
                    return VerifyUser(user, returnUrl);

                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View("PartialViews/Login");
                }
            }
            else
            {
                return View("PartialViews/Login");
            }
        }

        [HttpPost]
        public ActionResult Index(AccountViewModel model)
        {

            string returnUrl = string.Empty;

           

            if (ModelState.IsValid)
            {
                CompanyContact user =  null;
                 MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();

                 if ((baseResponse.Company.IsCustomer == (int)CustomerTypes.Corporate))
                 {
                     user = _myCompanyService.GetCorporateUserByEmailAndPassword(model.Email, model.Password, UserCookieManager.StoreId);
                 }
                 else
                 {
                     user = _myCompanyService.GetUserByEmailAndPassword(model.Email, model.Password);
                 }

                 
                if (user != null)
                {
                    if (model.KeepMeLoggedIn)
                        UserCookieManager.isWritePresistentCookie = true;
                    else
                        UserCookieManager.isWritePresistentCookie = false;
                    string ReturnURL = Request.Form["hfReturnURL"];
                    return VerifyUser(user, returnUrl);
                }
                else
                {
                   ViewBag.Message = "Invalid login attempt.";
                    return View("PartialViews/Login");
                }
            }
            else
            {
                return View("PartialViews/Login");
            }

        }

        private ActionResult VerifyUser(CompanyContact user, string ReturnUrl)
        {
            if (user.isArchived.HasValue && user.isArchived.Value == true)
            {
               ViewBag.Message = "Your account is archived.";
                return View("PartialViews/Login");
            }
            if (user.Company.IsDisabled == 1)
            {
                ViewBag.Message = "Your account is disabled. Please contact us for further information.";
                return View("PartialViews/Login");
            }
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp && user.isWebAccess == false)
            {
               ViewBag.Message ="Your account does not have the web access enabled. Please contact your Order Manager.";
                return View("PartialViews/Login");
            }
            else
            {

                //ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);

                //ClaimsSecurityService.AddSignInClaimsToIdentity(user.ContactId, user.CompanyId, Convert.ToInt32(user.ContactRoleId), Convert.ToInt64(user.TerritoryId), identity);

                //var claimsPriciple = new ClaimsPrincipal(identity);// HttpContext.User = new ClaimsPrincipal(identity);
                //// Make sure the Principal's are in sync
                //HttpContext.User = claimsPriciple;// ;
                //Thread.CurrentPrincipal = HttpContext.User;
                //AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

                UserCookieManager.ContactFirstName = user.FirstName;
                UserCookieManager.ContactLastName = user.LastName;
                UserCookieManager.ContactCanEditProfile = user.CanUserEditProfile ?? false;
                UserCookieManager.ShowPriceOnWebstore = user.IsPricingshown ?? true;
                
                UserCookieManager.Email = user.Email;

                if (ReturnUrl == "Social")
                    RedirectToLocal(ReturnUrl);
                else
                    Response.Redirect("/");// ControllerContext.HttpContext.Response.Redirect("");

                return null;
               
            }

        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                ControllerContext.HttpContext.Response.Redirect(returnUrl);
            }
            ControllerContext.HttpContext.Response.Redirect("/Home/Index");
           // ControllerContext.HttpContext.Response.Redirect(Url.Action("Index", "Home", null, protocol: Request.Url.Scheme));
            return null;
        }
    }
}