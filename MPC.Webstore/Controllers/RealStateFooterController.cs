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
        // GET: RealStateFooter
        public ActionResult Index()
        {
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            MPC.Models.DomainModels.Company model = null;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            ViewBag.TwitterUrl = StoreBaseResopnse.Company.TwitterURL;
            ViewBag.FacebookUrl = StoreBaseResopnse.Company.FacebookURL;

            return PartialView("PartialViews/RealStateFooter");
        }
    }
}