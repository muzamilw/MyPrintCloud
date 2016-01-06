using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class ManageAssetsController : Controller
    {
        // GET: ManageAssets
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webclaims;
        public ManageAssetsController(ICompanyService _myCompanyService, IWebstoreClaimsHelperService _webclaims)
        {
            this._myCompanyService = _myCompanyService;
            this._webclaims = _webclaims;
        }
        public ActionResult Index(string folderId)
        {
            long contac = _webclaims.loginContactID();
            long FolderId = Convert.ToInt64(folderId);
            List<Folder> GetFolder = new List<Folder>();
            List<Asset> GetAssets = _myCompanyService.GetAssetsByCompanyIDAndFolderID(UserCookieManager.WBStoreId, FolderId);
            if (FolderId > 0)
            {

                GetFolder = _myCompanyService.GetChildFolders(FolderId);

            }
            else
            {

                 GetFolder = _myCompanyService.GetFoldersByCompanyId(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            }
         
            ViewBag.Folders = GetFolder;
            List<TreeViewNodeVM> TreeModel = _myCompanyService.GetTreeVeiwList(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            ViewBag.TreeModel = TreeModel;
            ViewBag.Assets = GetAssets;
          
            ViewBag.LoginContact = _webclaims.loginContactRoleID();
            ViewBag.Admin = Roles.Adminstrator;
            ViewBag.Manager = Roles.Manager;
           return View("PartialViews/ManageAssets");
        }
        [HttpPost]
        public ActionResult Index( Asset Model)
        {
            return View("PartialViews/ManageAssets");
        }
       
        [HttpGet]
        public JsonResult GetFolders()
        {
            List<Folder> GetFolder = _myCompanyService.GetAllFolders(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            
            JsonResponse obj = new JsonResponse();
            obj.Folders = GetFolder;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetChildFolders(long FolderID)
        {
            JsonResponse obj = new JsonResponse();
            Folder folder = _myCompanyService.GetFolderByFolderId(FolderID);
            if (folder.ParentFolderId != null && folder.ParentFolderId != 0)
            {

                List<Folder> GetFolder = _myCompanyService.GetChildFolders(folder.ParentFolderId??0);

                obj.Folders = GetFolder;
            }
            
             return Json(obj, JsonRequestBehavior.AllowGet);
        }
        
        public class JsonResponse
        {
            public List<Folder> Folders;
            public List<Asset> Assets;
        }
        //UpdateAsset has been made in the repository
    }
}