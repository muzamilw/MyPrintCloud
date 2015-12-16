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
using MPC.Models.ResponseModels;
using System.Globalization;
using System.Threading;
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
        public ActionResult Index(string Message, string ReturnURL)
        {
            
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

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

            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.OauthErrorMessage = @"<script type='text/javascript' language='javascript'> $(document).ready(function () {ShowPopUp('Message', '" + Message + "'); });</script>";
               
            }
           
            return View("PartialViews/Login");
        }

        [HttpPost]
        public ActionResult Index(AccountViewModel model)
        {

            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

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
                    UserCookieManager.ShowPriceOnWebstore = user.IsPricingshown ?? false;
                    UserCookieManager.CanPlaceOrder = user.isPlaceOrder ?? false;
                    UserCookieManager.WEBEmail = user.Email;

                    if(UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                    {
                        long Orderid = _ItemService.PostLoginCustomerAndCardChanges(UserCookieManager.WEBOrderId, user.CompanyId, user.ContactId, UserCookieManager.TemporaryCompanyId, UserCookieManager.WEBOrganisationID, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), UserCookieManager.WBStoreId);

                        if (Orderid > 0)
                        {
                            UserCookieManager.TemporaryCompanyId = 0;

                            // this will update the order id if user is coming through cart
                            if(UserCookieManager.WEBOrderId != Orderid)
                            {
                                UserCookieManager.WEBOrderId = Orderid;
                            }

                            if (!string.IsNullOrEmpty(ReturnUrl))
                            {
                                RedirectToLocal(ReturnUrl);
                            }
                            else 
                            {
                                ControllerContext.HttpContext.Response.RedirectToRoute("ShopCart", new { OrderId = Orderid });
                               
                            }
                            return null;
                        }
                    }

                   
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        RedirectToLocal(ReturnUrl);
                    }
                    else
                    {
                        ControllerContext.HttpContext.Response.RedirectToRoute("Default");
                       
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

        [HttpPost]
        public JsonResult LoadStoreByContactInfo(long OrganisationId, string email, string password)
        {

            string Message = "ok";

            if (System.Text.RegularExpressions.Regex.IsMatch(email, "^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$"))
            {
                CompanyContact oContact = _myCompanyService.GetContactOnUserNamePass(OrganisationId, email, password);

                if (oContact != null)
                {
                    // Result = true;
                    MPC.Models.DomainModels.Company ContactCompany = _myCompanyService.GetCompanyByCompanyID(oContact.CompanyId);
                    long StoreId = 0;
                    if (ContactCompany.IsCustomer == (int)StoreMode.Corp)
                    {
                        StoreId = oContact.CompanyId;

                    }
                    else
                    {
                        if (ContactCompany.IsCustomer == (int)CustomerTypes.Customers)
                        {
                            StoreId = Convert.ToInt64(ContactCompany.StoreId);

                        }
                    }

                    if (StoreId > 0)
                    {
                        string CacheKeyName = "CompanyBaseResponse";
                        ObjectCache cache = MemoryCache.Default;
                        MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreFromCache(StoreId);

                        if (StoreBaseResopnse.Company != null)
                        {
                            // set company cookie
                            UserCookieManager.WBStoreId = StoreBaseResopnse.Company.CompanyId;
                            UserCookieManager.WEBStoreMode = StoreBaseResopnse.Company.IsCustomer;
                            UserCookieManager.isIncludeTax = StoreBaseResopnse.Company.isIncludeVAT ?? false;
                            UserCookieManager.TaxRate = StoreBaseResopnse.Company.TaxRate ?? 0;

                            // set user cookies
                            UserCookieManager.isRegisterClaims = 1;
                            UserCookieManager.WEBContactFirstName = oContact.FirstName;
                            UserCookieManager.WEBContactLastName = oContact.LastName == null ? "" : oContact.LastName;
                            UserCookieManager.ContactCanEditProfile = oContact.CanUserEditProfile ?? false;
                            UserCookieManager.ShowPriceOnWebstore = oContact.IsPricingshown ?? true;
                            UserCookieManager.WEBEmail = oContact.Email;
                            string languageName = _myCompanyService.GetUiCulture(Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId));

                            CultureInfo ci = null;

                            if (string.IsNullOrEmpty(languageName))
                            {
                                languageName = "en-US";
                            }

                            ci = new CultureInfo(languageName);

                            Thread.CurrentThread.CurrentUICulture = ci;
                            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);

                            UserCookieManager.PerformAutoLogin = true;
                            ControllerContext.HttpContext.Response.Redirect("http://mpc.foo.com");
                            return null;

                        }
                        else
                        {
                            Message = "1";
                            Url.Encode(Message);
                            ControllerContext.HttpContext.Response.Redirect("http://mpc/HtmlJsonPage/Login.html?message="+Message +"");
                            // no record found
                        }

                    }
                    else
                    {
                        Message = "1";
                        Url.Encode(Message);
                        ControllerContext.HttpContext.Response.Redirect("http://mpc/HtmlJsonPage/Login.html?message=" +Message +"");
                        // no record found
                    }
                }
                else
                {
                    Message = "2";
                    Url.Encode(Message);
                    ControllerContext.HttpContext.Response.Redirect("http://mpc/HtmlJsonPage/Login.html?message=" +Message+"");
                    //invalid email or pass
                }

            }
            else
            {
                // return message email is invalid
                Message = "3";
                Url.Encode(Message);
                ControllerContext.HttpContext.Response.Redirect("http://mpc/HtmlJsonPage/Login.html?message="+Message+"");
            }
            return Json(Message, JsonRequestBehavior.DenyGet);

        }
        //public void LoadStore(long OrganisationId,string email ,string password)
        //{
        //    CompanyContact Contact = _myCompanyService.GetContactOnUserNamePass(OrganisationId, email, password);
        //    MPC.Models.DomainModels.Company GetCompany = _myCompanyService.GetCompanyByCompanyID(Contact.CompanyId);

        //    if ((GetCompany.IsCustomer==(int)StoreMode.Corp))
        //    {
                

        //    }
        //    else
        //    {
        //        if ((GetCompany.IsCustomer == (int)CustomerTypes.Customers))
        //        {
                  
        //        }
        //    }
        //}
    }
}