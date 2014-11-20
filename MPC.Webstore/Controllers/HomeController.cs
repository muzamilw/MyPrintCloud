using System;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;

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