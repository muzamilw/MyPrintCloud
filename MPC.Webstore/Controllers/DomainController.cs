﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using Microsoft.Owin.Security;
using ICompanyService = MPC.Interfaces.WebStoreServices.ICompanyService;
using System.Runtime.Caching;
using System.Globalization;
using MPC.Models.ResponseModels;

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
        public ActionResult Index()
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            string url = HttpContext.Request.Url.ToString();
            //if (!string.IsNullOrEmpty(url))
            //{
            //    url = url.Substring(7);
            //}
            if (!string.IsNullOrEmpty(url) && url.Contains("http://"))
            {
                url = url.Replace("http://", "");//.Substring(7);
            }
            else if (!string.IsNullOrEmpty(url) && url.Contains("https://"))
            {
                url = url.Replace("https://", ""); 
            }
            long storeId = _myCompanyService.GetStoreIdFromDomain(url);
            if (storeId == 0)
            {
                TempData["ErrorMessage"] = Utils.GetKeyValueFromResourceFile("ltrldomainerrmess", UserCookieManager.WBStoreId, "The Domain in requested url does not point to any of the available stores.");
                return RedirectToAction("Error", "Home");
            }
            else
            {
                UserCookieManager.WEBOrderId = 0;
                if (UserCookieManager.WBStoreId == 0)
                {
                    UserCookieManager.WBStoreId = storeId;
                }

                MyCompanyDomainBaseReponse StoreBaseResopnse = null;
                if ((cache.Get(CacheKeyName) as Dictionary<long, MyCompanyDomainBaseReponse>) != null && (cache.Get(CacheKeyName) as Dictionary<long, MyCompanyDomainBaseReponse>).ContainsKey(storeId))
                {
                    StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MyCompanyDomainBaseReponse>)[storeId];
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
                    TempData["ErrorMessage"] = Utils.GetKeyValueFromResourceFile("ltrldomainerrmess", UserCookieManager.WBStoreId, "The Domain in requested url does not point to any of the available stores.");

                    return RedirectToAction("Error", "Home");
                }
            }
            return null;
        }

        public ActionResult ClearCache(long StoreId)
        {
            _myCompanyService.GetStoreFromCache(StoreId, true);
            return View();
        }

        public void ClearCacheObject()
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = null;

            policy = new CacheItemPolicy();
            policy.Priority = CacheItemPriority.NotRemovable;

            policy.RemovedCallback = null;

            Dictionary<long, MyCompanyDomainBaseReponse> stores = new Dictionary<long, MyCompanyDomainBaseReponse>();
            cache.Set(CacheKeyName, stores, policy);
        }

    }
}