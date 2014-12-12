using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Products.Controllers
{
    [SiteAuthorize(MisRoles = new[] { SecurityRoles.Admin }, AccessRights = new[] { SecurityAccessRight.CanViewProduct })]
    public class HomeController : Controller
    {

        #region Private
        private readonly IItemService itemService;
        #endregion

        #region Constructors
        
        /// <summary>
        /// Constructor
        /// </summary>
        public HomeController(IItemService itemService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }

            this.itemService = itemService;


        }
        #endregion

        // GET: Settings/Home
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProduct })]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [SiteAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProduct })]
        public ActionResult Index(HttpPostedFileBase file, long itemId, int imageFileType)
        {
            if (file != null && file.InputStream != null)
            {
                // Before attempting to save the file, verify
                SaveFile(file, itemId, (ItemFileType)imageFileType);

            }
            return null;
        }

        [HttpPost]
        public ActionResult DeleteImage(long itemId, int imageFileType)
        {
            string filePath = itemService.DeleteProductImage(itemId, (ItemFileType)imageFileType);
            if (filePath != null && System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return Content("success");
        }

        /// <summary>
        /// Upload File For Product
        /// </summary>
        private void SaveFile(HttpPostedFileBase file, long itemId, ItemFileType imageFileType)
        {
            // Specify the path to save Product files.
            string path = Server.MapPath("~/Resorces/Products/" + itemId + "/");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }

            path += file.FileName;
            itemService.SaveProductImage(path, itemId, imageFileType);
            file.SaveAs(path);
        }

    }
}