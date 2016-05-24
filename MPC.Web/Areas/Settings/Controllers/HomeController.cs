using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Settings.Controllers
{
    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewSettings })]
    public class HomeController : Controller
    {

        #region Private
        private readonly IMyOrganizationService myOrganizationService;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public HomeController(IMyOrganizationService myOrganizationService)
        {
            if (myOrganizationService == null)
            {
                throw new ArgumentNullException("myOrganizationService");
            }

            this.myOrganizationService = myOrganizationService;


        }
        #endregion
        // GET: Settings/Home
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewSecurity })]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        public ActionResult MyOrganization(HttpPostedFileBase file, long organizationId)
        {
            if (file != null && file.InputStream != null)
            {
                // Before attempting to save the file, verify
                SaveFile(file, organizationId);

            }
            return null;
        }

        //upload Files
        private void SaveFile(HttpPostedFileBase file, long organizationId)
        {
            if (file == null || file.InputStream == null)
            {
                return;
            }

            string path = Server.MapPath("~/MPC_Content/Organisations/" + organizationId);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = path + "\\" + file.FileName;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            file.SaveAs(path);
            int indexOf = path.LastIndexOf("MPC_Content", StringComparison.Ordinal);
            path = path.Substring(indexOf, path.Length - indexOf);
            myOrganizationService.SaveFilePath(path);
        }

        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        public ActionResult MyOrganization()
        {
            return View();
        }

        public ActionResult MachinesList()
        {
            return View();
        }
        public ActionResult MachinesDetail()
        {
            return View();
        }
        public ActionResult DeliveryCostCentres()
        {
            return View();
        }
        public ActionResult DeliveryAddOnsDetail()
        {
            return View();
        }
        public ActionResult DeliveryCarrier()
        {
            return View();
        }

        public ActionResult PhraseLibrary()
        {
            return View();
        }
        public ActionResult LookupMethods()
        {

            return View();

        }

        public ActionResult NotificationEmails()
        {
            return View();
        }

    }
}