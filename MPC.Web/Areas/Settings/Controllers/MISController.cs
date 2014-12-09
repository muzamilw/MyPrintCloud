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

    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewSecurity })]
    public class MISController : Controller
    {

        #region Private
        private readonly IInventoryService inventoryService;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public MISController(IInventoryService inventoryService)
        {
            if (inventoryService == null)
            {
                throw new ArgumentNullException("inventoryService");
            }

            this.inventoryService = inventoryService;


        }
        #endregion

        // GET: Settings/MIS
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewPaperSheet })]
        public ActionResult PaperSheet()
        {
            return View();
        }

        // GET: Settings/MIS
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewInventory })]
        public ActionResult Inventory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Inventory(HttpPostedFileBase file, long supplierId)
        {
            if (file != null && file.InputStream != null)
            {
                // Before attempting to save the file, verify
                SaveFile(file, supplierId);
            }
            return null;
        }
        /// <summary>
        /// Upload File For Product
        /// </summary>
        private void SaveFile(HttpPostedFileBase file, long supplierId)
        {
            // Specify the path to save Product files.
            string path = Server.MapPath("~/Resorces/Company/" + supplierId);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }
            string savePath = Server.MapPath("~/Resorces/Company/" + supplierId + "/");
            string fileName = file.FileName;
            // Append the name of the file to upload to the path.
            savePath += fileName;
            //path = Server.MapPath(path + "/" + file.FileName);
            file.SaveAs(savePath);
            inventoryService.SaveCompanyImage(savePath, supplierId);
        }
        // GET: Settings/InventoryCategory
        public ActionResult InventoryCategory()
        {
            return View();
        }
    }
}