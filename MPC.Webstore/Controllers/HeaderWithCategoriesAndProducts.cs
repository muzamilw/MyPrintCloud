using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class HeaderWithCategoriesAndProductsController : Controller
    {
        // GET: FutureFormHeader
        private ICompanyService _myCompanyService;

        public HeaderWithCategoriesAndProductsController(ICompanyService _myCompanyService)
        {
            this._myCompanyService = _myCompanyService;
        }
        public ActionResult Index()
        {
            List<ProductCategory> AllCategroies = new List<ProductCategory>();
            //List<ProductCategory> ChildCategories = new List<ProductCategory>();
            AllCategroies = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            ViewBag.ParentCategory = AllCategroies.Where(i => i.ParentCategoryId == null || i.ParentCategoryId == 0).OrderBy(g => g.DisplayOrder).Take(4).ToList();
            // ChildCategories=_myCompanyService.GetAllCategories(UserCookieManager.WBStoreId);
            ViewBag.AllChildCategories = AllCategroies.Where(i => i.ParentCategoryId != null || i.ParentCategoryId != 0).OrderBy(g => g.DisplayOrder).ToList();

            MPC.Models.DomainModels.Company model = null;
            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            //   MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            return PartialView("PartialViews/HeaderWithCategoriesAndProducts", StoreBaseResopnse.Company);
        }
    }
}