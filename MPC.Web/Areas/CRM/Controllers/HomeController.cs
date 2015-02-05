
using System.Web.Mvc;

namespace MPC.MIS.Areas.CRM.Controllers
{
    /// <summary>
    /// CRM Home Controller 
    /// </summary>
    public class HomeController : Controller
    {
        #region Private
        #endregion
        #region Constructors
        #endregion
        #region Public

        /// <summary>
        /// Shows Compnies List
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

       
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
        #endregion
    }
}