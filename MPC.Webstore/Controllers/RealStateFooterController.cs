using MPC.Interfaces.WebStoreServices;
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

    public class RealStateFooterController : Controller
    {
          #region Private

        private readonly ICompanyService _myCompanyService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RealStateFooterController(ICompanyService myCompanyService)
        {
            this._myCompanyService = myCompanyService;
        }

        #endregion
        // GET: RealStateFooter
        public ActionResult Index()
        {
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            MPC.Models.DomainModels.Company model = null;
           // MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            ViewBag.TwitterUrl = StoreBaseResopnse.Company.TwitterURL;
            ViewBag.FacebookUrl = StoreBaseResopnse.Company.FacebookURL;
            ViewBag.LinkedinUrl = StoreBaseResopnse.Company.LinkedinURL;
            return PartialView("PartialViews/RealStateFooter");
        }
    }
}