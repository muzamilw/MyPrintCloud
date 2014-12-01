using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.Models;
using MPC.Webstore.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index(AccountModel model)
        {
            //AccountModel model = new AccountModel();
            return View(model);
        }

        public ActionResult Banner()
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
        public ActionResult Login()
        {
            AccountModel model = new AccountModel();
            return PartialView("PartialViews/Login",model);
        }

        [HttpPost]
        public ActionResult Login(AccountModel model)
        {
            if (ModelState.IsValid)
            {
            }
            //return PartialView("PartialViews/Login", model);
            return PartialView(model);
        }
    }
}