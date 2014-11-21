using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;

namespace MPC.Webstore.Controllers
{
    public class HomeController : Controller
    {
         #region Private

// ReSharper disable InconsistentNaming
        private readonly ICompanyService companyService;
// ReSharper restore InconsistentNaming

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public HomeController(ICompanyService companyService)
        {
            if (companyService == null)
            {
                throw new ArgumentNullException("companyService");
            }
            this.companyService = companyService;
        }

        #endregion

        
        public ActionResult Index()
        {
            List<string> widgets = new List<string>();
            widgets.Add("News");
            widgets.Add("RaveReviews");

            ViewBag.Companyname =  Session["store"] as Company;
            ViewBag.widgets = widgets;
            return View();
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