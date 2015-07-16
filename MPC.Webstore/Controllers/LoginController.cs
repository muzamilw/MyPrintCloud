using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.Models;
using MPC.Models.Common;
using System.Runtime.Caching;
namespace MPC.Webstore.Controllers
{
    public class LoginController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly MPC.Interfaces.MISServices.ICompanyService _CompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        private readonly IItemService _ItemService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LoginController(ICompanyService myCompanyService, IWebstoreClaimsHelperService webstoreAuthorizationChecker
            , IItemService ItemService, MPC.Interfaces.MISServices.ICompanyService _CompanyService)
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
            this._ItemService = ItemService;
            this._CompanyService = _CompanyService;
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
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;


            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

            if ((StoreBaseResopnse.Company.IsCustomer == (int)CustomerTypes.Corporate && StoreBaseResopnse.Company.isAllowRegistrationFromWeb == true) || (StoreBaseResopnse.Company.IsCustomer == 1))
            {
                ViewBag.AllowRegisteration = 1;
            }
            else
            {
                ViewBag.AllowRegisteration = 0;
            }

            ViewBag.CompanyName = StoreBaseResopnse.Company.Name;


            if (!string.IsNullOrEmpty(StoreBaseResopnse.Company.facebookAppId) && !string.IsNullOrEmpty(StoreBaseResopnse.Company.facebookAppKey))
            {
                ViewBag.FBInitTag = "<script>window.fbAsyncInit = function () {FB.init({appId: '" + StoreBaseResopnse.Company.facebookAppId + "',status: true, cookie: false, xfbml: true});};</script>";
                ViewBag.ShowFacebookSignInLink = 1;
            }
            else
            {
                ViewBag.FBInitTag = "";
                ViewBag.ShowFacebookSignInLink = 0;
            }
            if (!string.IsNullOrEmpty(StoreBaseResopnse.Company.twitterAppId) && !string.IsNullOrEmpty(StoreBaseResopnse.Company.twitterAppKey))
            {
                ViewBag.ShowTwitterSignInLink = 1;
            }
            else
            {
                ViewBag.ShowTwitterSignInLink = 0;
            }


            if (string.IsNullOrEmpty(ReturnURL))
                ViewBag.ReturnURL = "";
            else
                ViewBag.ReturnURL = ReturnURL;
            
            if (!string.IsNullOrEmpty(FirstName))
            {
                string returnUrl = string.Empty;

                CompanyContact user = new CompanyContact();

                if (!string.IsNullOrEmpty(Email))
                {
                    user = _myCompanyService.GetContactByEmail(Email, StoreBaseResopnse.Organisation.OrganisationId, UserCookieManager.WBStoreId);
                }
                else
                {
                    user = _myCompanyService.GetContactByFirstName(FirstName);
                }
                if (user != null)
                {
                    return VerifyUser(user, returnUrl, StoreBaseResopnse);

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

            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            if (ModelState.IsValid)
            {
                CompanyContact user = null;
                
                
                if ((StoreBaseResopnse.Company.IsCustomer == (int)CustomerTypes.Corporate))
                {
                    user = _myCompanyService.GetCorporateUserByEmailAndPassword(model.Email, model.Password, UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                }
                else
                {
                    user = _myCompanyService.GetRetailUser(model.Email, model.Password, UserCookieManager.WEBOrganisationID, UserCookieManager.WBStoreId);
                }

                if (user != null)
                {
                    if (model.KeepMeLoggedIn)
                        UserCookieManager.isWritePresistentCookie = true;
                    else
                        UserCookieManager.isWritePresistentCookie = false;
                    string ReturnURL = Request.QueryString["ReturnURL"];

                    return VerifyUser(user, ReturnURL, StoreBaseResopnse);
                }
                else
                {
                    ViewBag.Message = "Invalid login attempt.";
                    SetViewFlags(StoreBaseResopnse);

                    return View("PartialViews/Login");
                }
            }
            else
            {

                SetViewFlags(StoreBaseResopnse);

                return View("PartialViews/Login");
            }

        }

        private ActionResult VerifyUser(CompanyContact user, string ReturnUrl, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse)
        {
            try
            {
                if (user.isArchived.HasValue && user.isArchived.Value == true)
                {
                    ViewBag.Message = Utils.GetKeyValueFromResourceFile("DefaultAddress", UserCookieManager.WBStoreId); // "Your account is archived.";
                    SetViewFlags(StoreBaseResopnse);
                    return View("PartialViews/Login");
                }
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && user.isWebAccess == false)
                {
                    ViewBag.Message = Utils.GetKeyValueFromResourceFile("AccountHasNoWebAccess", UserCookieManager.WBStoreId);  //"Your account does not have the web access enabled. Please contact your Order Manager.";
                    SetViewFlags(StoreBaseResopnse);
                    return View("PartialViews/Login");
                }
                else
                {
                    
                    UserCookieManager.isRegisterClaims = 1;
                    UserCookieManager.WEBContactFirstName = user.FirstName;
                    UserCookieManager.WEBContactLastName = user.LastName == null ? "" : user.LastName;
                    UserCookieManager.ContactCanEditProfile = user.CanUserEditProfile ?? false;
                    UserCookieManager.ShowPriceOnWebstore = user.IsPricingshown ?? true;

                    UserCookieManager.WEBEmail = user.Email;

                    if(UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                    {
                        long Orderid = _ItemService.PostLoginCustomerAndCardChanges(UserCookieManager.WEBOrderId, user.CompanyId, user.ContactId, UserCookieManager.TemporaryCompanyId, UserCookieManager.WEBOrganisationID);

                        if (Orderid > 0)
                        {
                            UserCookieManager.TemporaryCompanyId = 0;
                            if (!string.IsNullOrEmpty(ReturnUrl))
                            {
                                RedirectToLocal(ReturnUrl);
                            }
                            else 
                            {
                                Response.Redirect("/ShopCart?OrderId=" + Orderid);
                            }
                        }
                    }

                   
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        RedirectToLocal(ReturnUrl);
                    }
                    else
                    {
                        Response.Redirect("/");
                    
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                ControllerContext.HttpContext.Response.Redirect(returnUrl);
            }
            
            return null;
        }

        private void SetViewFlags(MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse)
        {
            if (StoreBaseResopnse != null)
            {
                if ((StoreBaseResopnse.Company.IsCustomer == (int)CustomerTypes.Corporate && StoreBaseResopnse.Company.isAllowRegistrationFromWeb == true) || (StoreBaseResopnse.Company.IsCustomer == 1))
                {
                    ViewBag.AllowRegisteration = 1;
                }
                else
                {
                    ViewBag.AllowRegisteration = 0;
                }
                if (!string.IsNullOrEmpty(StoreBaseResopnse.Company.facebookAppId) && !string.IsNullOrEmpty(StoreBaseResopnse.Company.facebookAppKey))
                {
                    ViewBag.ShowFacebookSignInLink = 1;
                }
                else
                {
                    ViewBag.ShowFacebookSignInLink = 0;
                }
                if (!string.IsNullOrEmpty(StoreBaseResopnse.Company.twitterAppId) && !string.IsNullOrEmpty(StoreBaseResopnse.Company.twitterAppKey))
                {
                    ViewBag.ShowTwitterSignInLink = 1;
                }
                else
                {
                    ViewBag.ShowTwitterSignInLink = 0;
                }
                ViewBag.CompanyName = StoreBaseResopnse.Company.Name;
            }
            else
            {
                ViewBag.ShowTwitterSignInLink = 0;
                ViewBag.ShowFacebookSignInLink = 0;
                ViewBag.AllowRegisteration = 0;
                ViewBag.CompanyName = "";
            }

        }
    }
}