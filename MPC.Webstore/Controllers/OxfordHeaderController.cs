using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class OxfordHeaderController : Controller
    {
        // GET: OxfordHeader
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;
        public OxfordHeaderController(ICompanyService _myCompanyService, IWebstoreClaimsHelperService _webstoreAuthorizationChecker)
        {
            this._myCompanyService = _myCompanyService;
            this._webstoreAuthorizationChecker = _webstoreAuthorizationChecker;
        }
        public ActionResult Index()
        {

            CompanyContact LoginCompany = _myCompanyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
            MPC.Models.DomainModels.Company model = null;
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            ////   MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            return PartialView("PartialViews/OxfordHeader", StoreBaseResopnse.Company);

        }
    }
}