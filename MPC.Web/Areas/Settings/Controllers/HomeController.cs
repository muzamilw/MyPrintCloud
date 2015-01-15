﻿using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.MISServices;

namespace MPC.MIS.Areas.Settings.Controllers
{
    //[SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewSecurity })]
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
        //[SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrganisation })]
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

            string path = Server.MapPath("~/MPC_Content/Organisations/Organisation" + organizationId);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = path + "\\Organisation" + organizationId + "_" + file.FileName + ".jpeg";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            file.SaveAs(path);
            myOrganizationService.SaveFilePath(path);
        }

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

    }
}