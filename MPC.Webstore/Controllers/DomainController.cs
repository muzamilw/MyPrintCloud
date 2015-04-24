using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.Models;
using DotNetOpenAuth.OAuth2;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Text;
using System.Security.Claims;
using ICompanyService = MPC.Interfaces.WebStoreServices.ICompanyService;
using MPC.Models.Common;
using MPC.Interfaces.Common;
using System.Reflection;
using MPC.Models.DomainModels;
using MPC.WebBase.UnityConfiguration;
using System.Runtime.Caching;
using System.Web.Security;
using WebSupergoo.ABCpdf8;
using System.Web;
using System.Web.Http;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace MPC.Webstore.Controllers
{

    public class DomainController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webauthorizationChecker;

        #endregion

        [Dependency]
        public IWebstoreClaimsSecurityService ClaimsSecurityService { get; set; }




        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DomainController(ICompanyService myCompanyService, IWebstoreClaimsHelperService _webauthorizationChecker)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }

            this._myCompanyService = myCompanyService;
            this._webauthorizationChecker = _webauthorizationChecker;
        }

        #endregion

        // GET: Domain
        public void Index()
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            string url = HttpContext.Request.Url.ToString();
            if (!string.IsNullOrEmpty(url))
            {
                url = url.Substring(7);
            }
            long storeId = _myCompanyService.GetStoreIdFromDomain(url);
            if (storeId == 0)
            {
                Response.Redirect("/Error");

            }
            else
            {
                if (UserCookieManager.WBStoreId == 0)
                {
                    UserCookieManager.WBStoreId = storeId;
                }

                MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = null;
                if ((cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>) != null && (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>).ContainsKey(storeId))
                {
                    StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[storeId];
                }
                else
                {
                    StoreBaseResopnse = _myCompanyService.GetStoreFromCache(storeId);
                }

                if (StoreBaseResopnse.Company != null)
                {
                    UserCookieManager.WBStoreId = StoreBaseResopnse.Company.CompanyId;
                    UserCookieManager.WEBStoreMode = StoreBaseResopnse.Company.IsCustomer;
                    UserCookieManager.isIncludeTax = StoreBaseResopnse.Company.isIncludeVAT ?? false;
                    UserCookieManager.TaxRate = StoreBaseResopnse.Company.TaxRate ?? 0;
                    UserCookieManager.WEBOrganisationID = StoreBaseResopnse.Company.OrganisationId ?? 0;
                    UserCookieManager.isRegisterClaims = 2;
                    // set global language of store

                    string languageName = _myCompanyService.GetUiCulture(Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId));

                    CultureInfo ci = null;

                    if (string.IsNullOrEmpty(languageName))
                    {
                        languageName = "en-US";
                    }

                    ci = new CultureInfo(languageName);

                    Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);

                    if (StoreBaseResopnse.Company.IsCustomer == 3)// corporate customer
                    {
                        Response.Redirect("/Login");
                    }
                    else
                    {
                        Response.Redirect("/");
                    }
                }
                else
                {
                    RedirectToAction("Error", "Home");
                }
            }

            // return RedirectToAction("Index", "Home");
            //  return View();
        }

        public void updateCache(string name)
        {
            _myCompanyService.GetStoreFromCache(Convert.ToInt64(name), true);
            RedirectToAction("Error", "Home");
        }

        public ActionResult AutoLoginOrRegister(string C, string F, string L, string E, string CC)
        {

            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(E, "^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$"))
                {
                    if (!string.IsNullOrEmpty(C))
                    {

                        MPC.Models.DomainModels.Company oCompany = _myCompanyService.isValidWebAccessCode(C, UserCookieManager.WEBOrganisationID);

                        if (oCompany != null)
                        {
                            CompanyContact oContact = _myCompanyService.GetOrCreateContact(oCompany, E, F, L, C);
                            if (oContact == null && oCompany.isAllowRegistrationFromWeb == true)
                            {
                                return RedirectToAction("Error", "Home", new { Message = "You are not allowed to register." });
                            }
                            else
                            {
                                string CacheKeyName = "CompanyBaseResponse";
                                ObjectCache cache = MemoryCache.Default;
                                MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = null;
                                if ((cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>) != null && (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>).ContainsKey(oCompany.CompanyId))
                                {
                                    StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[oCompany.CompanyId];
                                }
                                else
                                {
                                    StoreBaseResopnse = _myCompanyService.GetStoreFromCache(oCompany.CompanyId);
                                }

                                if (StoreBaseResopnse.Company != null)
                                {
                                    // set company cookie
                                    UserCookieManager.WBStoreId = StoreBaseResopnse.Company.CompanyId;
                                    UserCookieManager.WEBStoreMode = StoreBaseResopnse.Company.IsCustomer;
                                    UserCookieManager.isIncludeTax = StoreBaseResopnse.Company.isIncludeVAT ?? false;
                                    UserCookieManager.TaxRate = StoreBaseResopnse.Company.TaxRate ?? 0;
                                    
                                    // set user cookies
                                    UserCookieManager.isRegisterClaims = 1;
                                    //UserCookieManager.WEBContactFirstName = oContact.FirstName;
                                    UserCookieManager.WEBContactLastName = oContact.LastName == null ? "" : oContact.LastName;
                                    UserCookieManager.ContactCanEditProfile = oContact.CanUserEditProfile ?? false;
                                    UserCookieManager.ShowPriceOnWebstore = oContact.IsPricingshown ?? true;
                                    UserCookieManager.WEBEmail = oContact.Email;
                                    Response.Cookies["WEBFirstName"].Value = oContact.FirstName;
                                    string languageName = _myCompanyService.GetUiCulture(Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId));

                                    CultureInfo ci = null;

                                    if (string.IsNullOrEmpty(languageName))
                                    {
                                        languageName = "en-US";
                                    }

                                    ci = new CultureInfo(languageName);

                                    Thread.CurrentThread.CurrentUICulture = ci;
                                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
                                   // ViewBag.ResponseRedirectUrl = "/";
                                    return View();
                                    // ControllerContext.HttpContext.Response.Redirect("/");
                                    //return null;
                                }
                                else
                                {
                                    return RedirectToAction("Error", "Home", new { Message = "Please try again." });
                                }
                            }
                        }
                        else
                        {
                            return RedirectToAction("Error", "Home", new { Message = "Your Web Access Code is invalid." });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { Message = "Please enter Web Access Code to proceed." });
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Home", new { Message = "Please enter valid email address to proceed." });
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}