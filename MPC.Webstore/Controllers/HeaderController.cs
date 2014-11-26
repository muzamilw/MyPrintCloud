using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.Models;

namespace MPC.Webstore.Controllers
{
    public class HeaderController : Controller
    {

        // GET: News
        public ActionResult Index()
        {
            Company model = null;
            ObjectCache cache = MemoryCache.Default;

            MyCompanyDomainBaseResponse obj = cache.Get("CompanyBaseResponse") as MyCompanyDomainBaseResponse;
            if (obj != null)
            {
                model = obj.Company;
            }

            return PartialView("PartialViews/Header", model);
        }
    }
}