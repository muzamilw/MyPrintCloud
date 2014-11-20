using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.IServices;
using MPC.Webstore.Models;

namespace MPC.Webstore.Controllers
{
    public class HomeController : Controller
    {
         #region Private

        private readonly ICompanyService companyService;

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
            ViewBag.Companyname =  companyService.GetCompanyByDomain("yolk").Name;
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