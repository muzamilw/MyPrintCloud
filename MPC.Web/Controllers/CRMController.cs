using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.MIS.Controllers
{
    public class CRMController : Controller
    {
        //
        // GET: /Settings/

        public ActionResult ContactCompanies()
        {
            return View();
        }

        public ActionResult ContactCompanyDetail()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult AddStoreWizard()
        {
            return View();
        }

        public PartialViewResult ProductWizardView()
        {
            return PartialView("AddStoreWizard");
        }
      
        public ActionResult CompaniesList()
        {
            return View();
        }
        public ActionResult CompaniesDetail()
        {
            return View();
        }
        public ActionResult AddStore()
        {
            return View();
        }
        public ActionResult AddCustomer()
        {
            return View();
        }
        public ActionResult AddBanner()
        {
            return View();
        }
        public ActionResult AddEditSecondaryPage()
        {
            return View();
        }

        public ActionResult SuppliersList()
        {

            return View();
        }

        public ActionResult SupplierDetail()
        {

            return View();
        }
    }
}
