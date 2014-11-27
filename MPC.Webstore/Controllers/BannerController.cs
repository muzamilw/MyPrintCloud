using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Models;
using MPC.Webstore.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class BannerController : Controller
    {
     
        public ActionResult Index()
        {
            List<CompanyBanner> model = null;
            ObjectCache cache = MemoryCache.Default;

            MyCompanyDomainBaseResponse obj = cache.Get("CompanyBaseResponse") as MyCompanyDomainBaseResponse;
            if (obj != null)
            {
                model = obj.Banners;
            }
            return PartialView("PartialViews/Banner", model);
        }
    }
}