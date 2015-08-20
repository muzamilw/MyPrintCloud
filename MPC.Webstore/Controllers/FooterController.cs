using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.Models;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Models.Common;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.ModelMappers;
using MPC.Models.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class FooterController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FooterController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion
        // GET: Footer
        public ActionResult Index()
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            MPC.Models.DomainModels.Company model = null;

          //  MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
          //  MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            if (StoreBaseResopnse.Company != null)
            {
                model = StoreBaseResopnse.Company;
            }

            StoreBaseResopnse = null;

            //MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(storeId).CreateFromCompany();

            return PartialView("PartialViews/Footer", model);
        }


    }
}