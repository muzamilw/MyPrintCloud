using MPC.Interfaces.WebStoreServices;
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
        public ActionResult Index()
        {
           List<Asset> GetAssets = _myCompanyService.GetAssetsByCompanyID(UserCookieManager.WBStoreId);
           List<Folder> GetFolder = _myCompanyService.GetFoldersByCompanyId(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
           ViewBag.Assets = GetAssets;
           ViewBag.Folders = GetFolder;
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
        //UpdateAsset has been made in the repository
    }
}