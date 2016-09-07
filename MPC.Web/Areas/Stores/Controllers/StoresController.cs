using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using MPC.Models.ResponseModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Stores.Controllers
{
    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewStore })]
    public class StoresController : Controller
    {
        #region Private
        private readonly ICompanyService companyService;
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public StoresController(ICompanyService companyService)
        {
            if (companyService == null)
            {
                throw new ArgumentNullException("companyService");
            }

            this.companyService = companyService;


        }
        #endregion
        // GET: Stores/Stores
       [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, long companyId)
        {
            if (file != null && file.InputStream != null)
            {
                var organizationId = companyService.GetOrganisationId();
                // Before attempting to save the file, verify
                SaveFile(file, organizationId, companyId);

            }
            return null;
        }

        //upload Files
        private void SaveFile(HttpPostedFileBase file, long organizationId, long companyId)
        {
            // Specify the path to save organisations files.
            string organisationDirectoryPath = Server.MapPath("~/Organisations/" + organizationId + "/Companies/" + companyId);

            if (!Directory.Exists(organisationDirectoryPath))
            {
                Directory.CreateDirectory(organisationDirectoryPath);

            }
            string savePath = Server.MapPath("~/Organisations/" + organizationId + "/Companies/" + companyId + "/");
            string fileName = file.FileName;
            // Append the name of the file to upload to the path.
            savePath += fileName;
            file.SaveAs(savePath);
            companyService.SaveFile(savePath, companyId);
        }

        public ActionResult ProductTemplatesIndex(long storeId, long categoryId, long parentCategoryId)
        {
            ProductTemplateListResponseModel responseModel = companyService.GetFilteredProductTemplates(storeId,
                categoryId, parentCategoryId);
            return View(responseModel);
        }
     
    }
}