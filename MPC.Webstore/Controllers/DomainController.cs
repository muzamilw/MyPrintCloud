using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.Common;
using System.Web;
using System.Runtime.Caching;
namespace MPC.Webstore.Controllers
{
    
    public class DomainController : Controller
    {
         #region Private

        private readonly ICompanyService _myCompanyService;


        #endregion


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DomainController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
          
            this._myCompanyService = myCompanyService;
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
                if(UserCookieManager.StoreId == 0)
                {
                    UserCookieManager.StoreId = storeId;
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
                    UserCookieManager.StoreId = StoreBaseResopnse.Company.CompanyId;
                    UserCookieManager.StoreMode = StoreBaseResopnse.Company.IsCustomer;
                    UserCookieManager.isIncludeTax = StoreBaseResopnse.Company.isIncludeVAT ?? false;
                    UserCookieManager.TaxRate = StoreBaseResopnse.Company.TaxRate ?? 0;
                    UserCookieManager.OrganisationID = StoreBaseResopnse.Company.OrganisationId ?? 0;
                   
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
    }
}