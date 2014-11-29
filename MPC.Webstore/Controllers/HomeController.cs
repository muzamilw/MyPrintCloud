using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ResponseModels;
using System.Runtime.Caching;
using MPC.Webstore.Models;

namespace MPC.Webstore.Controllers
{
    public class HomeController : Controller
    {
         #region Private

        private readonly ICompanyService _myCompanyService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public HomeController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion
        public ActionResult Index(AccountModel accountmodel)
        {
            List<CmsSkinPageWidget> model = null;
            string CacheKeyName = "CompanyBaseResponse";
            string val = ((System.Web.Routing.Route) (RouteData.Route)).Url; //RouteData.Values["controller"].ToString();
            val = val.Split('/')[0];
            ObjectCache cache = MemoryCache.Default;

            MyCompanyDomainBaseResponse responseObject = cache.Get(CacheKeyName) as MyCompanyDomainBaseResponse;

            if (responseObject == null)
            {
                long storeId = Convert.ToInt64(Session["storeId"]);
                MyCompanyDomainBaseResponse response = _myCompanyService.GetBaseData(storeId).CreateFrom();

                CacheItemPolicy policy = null;
                CacheEntryRemovedCallback callback = null;

                policy = new CacheItemPolicy();
                policy.Priority = CacheItemPriority.NotRemovable;
                policy.SlidingExpiration =
                    TimeSpan.FromMinutes(5);
                policy.RemovedCallback = callback;
                cache.Set(CacheKeyName, response, policy);
                model = response.CmsSkinPageWidgets.ToList();
                if (val == "Login")
                {
                    model = response.CmsSkinPageWidgets.Where(p => p.PageId == 59).ToList();
                }
                else
                {
                    model = response.CmsSkinPageWidgets.Where(p => p.PageId == 1).ToList();
                }
            }
            else
            {

                if (val == "Login")
                {
                    model = responseObject.CmsSkinPageWidgets.Where(p => p.PageId == 59).ToList();
                }
                else
                {
                    model = responseObject.CmsSkinPageWidgets.Where(p => p.PageId == 1).ToList();
                }
            }
            ViewBag.AccountModel = accountmodel;
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}