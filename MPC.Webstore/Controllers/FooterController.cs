using MPC.Webstore.Models;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class FooterController : Controller
    {
        // GET: Footer
        public ActionResult Index()
        {
            Company model = null;
            ObjectCache cache = MemoryCache.Default;

            MyCompanyDomainBaseResponse obj = cache.Get("CompanyBaseResponse") as MyCompanyDomainBaseResponse;
            if (obj != null)
            {
                model = obj.Company;
            }

            return PartialView("PartialViews/Footer", model);
        }
    }
}