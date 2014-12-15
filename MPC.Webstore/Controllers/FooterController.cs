using MPC.Interfaces.WebStoreServices;
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
using MPC.Webstore.Models;
using MPC.Models.Common;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.ModelMappers;

namespace MPC.Webstore.Controllers
{
    public class FooterController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

        #endregion
        // GET: Footer
        public ActionResult Index()
        {
            MPC.Webstore.Models.Company model = null;
            ObjectCache cache = MemoryCache.Default;

            MyCompanyDomainBaseResponse obj = cache.Get("CompanyBaseResponse") as MyCompanyDomainBaseResponse;
            if (obj != null)
            {
                model = obj.Company;
            }
            long storeId = Convert.ToInt64(Session["storeId"]);

            //MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(storeId).CreateFromCompany();

            return PartialView("PartialViews/Footer", model);
        }


    }
}