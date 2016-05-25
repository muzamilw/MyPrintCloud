
using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.CRM.Controllers
{
    /// <summary>
    /// CRM Home Controller 
    /// </summary>
   // [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewCrm })]
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
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProspect })]
        public ActionResult Index(int? id)
        {
            ViewBag.ActiveTab = ActiveModuleMenuItem.Companies;
            ViewBag.CallingMethod = (string)TempData["CallingMethod"] != "" ? TempData["CallingMethod"] : "0";
            ViewBag.CompanyId = id ?? 0;
            return View();
        }

        /// <summary>
        /// Contacts
        /// </summary>
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCrm })]
        public ActionResult Contacts()
        {
            ViewBag.ActiveTab = ActiveModuleMenuItem.Contacts;
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

        public ActionResult ToDoList()
        {
            ViewBag.ActiveTab = ActiveModuleMenuItem.ToDoList;
            return View();
        }
        public ActionResult EMarketing()
        {
            ViewBag.ActiveTab = ActiveModuleMenuItem.EMarketing;
            return View();
        }


        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCalendar })]
        public ActionResult Calendar()
        {
            ViewBag.ActiveTab = ActiveModuleMenuItem.Calendar;
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
            ViewBag.ActiveTab = ActiveModuleMenuItem.Supplier;
            return View();
        }


        public ActionResult SupplierDetail()
        {

            return View();
        }
        #endregion
    }
}