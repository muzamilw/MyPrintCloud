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
       
        public ManageAssetsController(ICompanyService _myCompanyService)
        {
            this._myCompanyService = _myCompanyService;
        }
        public ActionResult Index(string folderId)
        {
            long FolderId = Convert.ToInt64(folderId);
           // if (folderId > 0)
           // {
            List<Asset> GetAssets = _myCompanyService.GetAssetsByCompanyIDAndFolderID(UserCookieManager.WBStoreId, FolderId);
           // }
            List<Folder> GetFolder = _myCompanyService.GetFoldersByCompanyId(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
          // ViewBag.Assets = GetAssets;
            ViewBag.Folders = GetFolder;
            List<TreeViewNodeVM> TreeModel = _myCompanyService.GetTreeVeiwList(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            ViewBag.TreeModel = TreeModel;
            ViewBag.Assets = GetAssets;
           return View("PartialViews/ManageAssets");
        }
        [HttpPost]
        public ActionResult Index( Asset Model)
        {
            return View("PartialViews/ManageAssets");
        }
        [HttpPost]
        public void DeleteAsset(long AssetID)
        {
           _myCompanyService.DeleteAsset(AssetID);
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