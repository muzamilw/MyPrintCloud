
using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.CRM.Controllers
{
    /// <summary>
    /// CRM Home Controller 
    /// </summary>
    //[SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewCrm })]
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
        //[SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProspect })]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Contacts
        /// </summary>
        //[SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewContact })]
        public ActionResult Contacts()
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


        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCalendar })]
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

        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewSupplier })]
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