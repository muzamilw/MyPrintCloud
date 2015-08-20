using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.ResponseModels;
using System.Runtime.Caching;

namespace MPC.Webstore.Controllers
{
    public class BaseController : Controller
    {
       
        //public MyCompanyDomainBaseReponse StoreCachedData { get; set; }
        //private readonly IWebstoreClaimsHelperService _myClaimsServcie;
        //#region Constructor
        ///// <summary>
        ///// Constructor
        ///// </summary>
        //public BaseController(ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimsServcie)
        //{
        //    this._myClaimsServcie = myClaimsServcie;
        //    string CacheKeyName = "CompanyBaseResponse";
        //    ObjectCache cache = MemoryCache.Default;
        //   // StoreCachedData = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
        //   // StoreCachedData = (cache.Get(CacheKeyName)) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>;
        //    Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse> domainResponse = (cache.Get(CacheKeyName)) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>;

        //    if (domainResponse == null)
        //    {
        //        if (UserCookieManager.WBStoreId > 0)
        //        {
        //            StoreCachedData = myCompanyService.GetStoreFromCache(UserCookieManager.WBStoreId);

        //        }
        //        else
        //        {
        //            TempData["ErrorMessage"] = "Your session is expired. Please re-enter your domain URL.";
        //            RedirectToAction("Error");
        //        }
        //    }
        //    else
        //    {
        //        // if company not found in cache then rebuild the cache
        //        if (!domainResponse.ContainsKey(UserCookieManager.WBStoreId))
        //        {
        //            StoreCachedData = myCompanyService.GetStoreFromCache(UserCookieManager.WBStoreId);
        //        }
        //        else 
        //        {
        //            StoreCachedData = domainResponse.Where(i => i.Key == UserCookieManager.WBStoreId).FirstOrDefault().Value;
        //        }
        //    }
        //}

        //public bool ShowPricesOnStore() 
        //{
        //    if (StoreCachedData.Company.ShowPrices == true)
        //    {
        //        if (_myClaimsServcie.loginContactID() > 0)
        //        {
        //            if (UserCookieManager.ShowPriceOnWebstore == true)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

       // #endregion
    }
}