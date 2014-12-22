using System;
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
        public ActionResult Index(HttpPostedFileBase file, long organizationId)
        {
            if (file != null && file.InputStream != null)
            {
                // Before attempting to save the file, verify
                SaveFile(file);

            }
            return null;
        }

        //upload Files
        private void SaveFile(HttpPostedFileBase file)
        {
            if (file == null || file.InputStream == null)
            {
                return;
            }

            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                file.InputStream.CopyTo(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            myOrganizationService.SaveFileToFileTable(file.FileName, fileBytes);
        }

    }
}