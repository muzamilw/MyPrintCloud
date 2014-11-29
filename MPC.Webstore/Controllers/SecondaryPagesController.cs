using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class SecondaryPagesController : Controller
    {
        // GET: SecondaryPages
        public ActionResult Index()
        {
            ObjectCache cache = MemoryCache.Default;

            MyCompanyDomainBaseResponse obj = cache.Get("CompanyBaseResponse") as MyCompanyDomainBaseResponse;
            if (obj != null)
            {
                ViewData["PageCategory"] = obj.PageCategories;
                ViewData["CmsPage"] = obj.cmsPages;
            }
            return PartialView("PartialViews/SecondaryPages");
        }
    }
}