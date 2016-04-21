﻿using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class ManageAssetsController : Controller
    {
        // GET: ManageAssets
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _webclaims;
        private readonly IOrderService _orderService;

        private readonly IItemService _myItemService;
        public ManageAssetsController(ICompanyService _myCompanyService, IWebstoreClaimsHelperService _webclaims, IItemService myItemService, IOrderService orderService)
        {
            this._myCompanyService = _myCompanyService;
            this._webclaims = _webclaims;

            this._myItemService = myItemService;
            this._orderService = orderService;
        }

        public ActionResult Index(string folderId, string Searchfolder, string SelectedTreeID)
        {
            ViewBag.SelectedTreeID = folderId;
            List<Folder> GetFolder = new List<Folder>();
            List<Asset> GetAssets = new List<Asset>();
            long loginContactId = _webclaims.loginContactID();
            if (Searchfolder != null && Searchfolder != string.Empty)
            {
                Searchfolder = Searchfolder.Replace("___", " ");

                Folder RequiredFolder;
                List<Folder> FolderList;

                FolderList = _myCompanyService.GetAllFolders(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                RequiredFolder = FolderList.Where(i => i.FolderName.Contains(Searchfolder)).FirstOrDefault();
                if (RequiredFolder != null)
                {
                    if (RequiredFolder.FolderId > 0)
                    {

                        GetFolder = _myCompanyService.GetChildFolders(RequiredFolder.FolderId);
                        ViewBag.SelectedTreeID = RequiredFolder.FolderId;
                    }

                    GetAssets = _myCompanyService.GetAssetsByCompanyIDAndFolderID(UserCookieManager.WBStoreId, RequiredFolder.FolderId);
                }
                else
                {
                     GetFolder = _myCompanyService.GetFoldersByCompanyId(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                     GetAssets = _myCompanyService.GetAssetsByCompanyIDAndFolderID(UserCookieManager.WBStoreId,0);
                }
                ViewBag.Folders = GetFolder;
                List<TreeViewNodeVM> TreeModel = _myCompanyService.GetTreeVeiwList(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                ViewBag.TreeModel = TreeModel;
                ViewBag.Assets = GetAssets;
                ViewBag.Searchfolder = Searchfolder;
                ViewBag.Admin = Roles.Adminstrator;
                ViewBag.Manager = Roles.Manager;
                ViewBag.LoginContact = _webclaims.loginContactRoleID();
            }
            else
            {
                long contac = _webclaims.loginContactID();
                long FolderId = Convert.ToInt64(folderId);
                 GetFolder = new List<Folder>();
                 GetAssets = _myCompanyService.GetAssetsByCompanyIDAndFolderID(UserCookieManager.WBStoreId, FolderId);
                //foreach (var time in GetAssets)
                //{
                //    var txtAuctionDate = String.Format("{0:MM/dd/yyyy}", time.CreationDateTime);
                
                //}
                 List<TreeViewNodeVM> TreeModel = new List<TreeViewNodeVM>();
                if (FolderId > 0)
                {

                    GetFolder = _myCompanyService.GetChildFolders(FolderId);

                }
                else
                {
                    if (_webclaims.loginContactRoleID() != Convert.ToInt64(Roles.Adminstrator) &&
                   _webclaims.loginContactRoleID() != Convert.ToInt64(Roles.Manager))
                    {
                        //Get Folders by Company Territory
                        GetFolder = _myCompanyService.GetFoldersByCompanyTerritory(UserCookieManager.WBStoreId,
                            UserCookieManager.WEBOrganisationID, _webclaims.loginContactTerritoryID());
                    }
                    else
                    {
                        GetFolder = _myCompanyService.GetFoldersByCompanyId(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                    }
                    
                }
                if (_webclaims.loginContactRoleID() != Convert.ToInt64(Roles.Adminstrator) &&
                   _webclaims.loginContactRoleID() != Convert.ToInt64(Roles.Manager))
                {
                    //Get Folders by Company Territory
                    TreeModel = _myCompanyService.GetTreeVeiwListByTerritory(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, _webclaims.loginContactTerritoryID());
                }
                else
                {
                    TreeModel = _myCompanyService.GetTreeVeiwList(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                }
                //   if (Searchfolder != null && Searchfolder != string.Empty)
                // {
                //   GetFolder = FilteredFolderList(Searchfolder,false);
                // }
                if (TreeModel.Count > 0)
                    TreeModel.Insert(0, new TreeViewNodeVM {FolderId = 0, FolderName = "Home"});
                
                ViewBag.Folders = GetFolder;
                
                ViewBag.TreeModel = TreeModel;
                ViewBag.Assets = GetAssets;

                ViewBag.LoginContact = _webclaims.loginContactRoleID();
                ViewBag.Admin = Roles.Adminstrator;
                ViewBag.Manager = Roles.Manager;
                ViewBag.Searchfolder = Searchfolder;
            }
           return View("PartialViews/ManageAssets");
        }
       
        public ActionResult LoadFoldersOnSearch(string Searchfolder,string OrginalVal)
        {
            OrginalVal = OrginalVal.Replace("___", " ");

            Folder RequiredFolder;
            List<Folder> FolderList;
            List<Asset> GetAssets = new List<Asset>();
            FolderList = _myCompanyService.GetAllFolders(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            RequiredFolder = FolderList.Where(i => i.FolderName.Contains(Searchfolder)).FirstOrDefault();

            List<Folder> GetFolder = new List<Folder>();

            if (RequiredFolder == null)
            {
                 GetFolder = FolderList;
                 GetAssets = _myCompanyService.GetAssetsByCompanyIDAndFolderID(UserCookieManager.WBStoreId,0);
            }
            else
            {
                if (RequiredFolder.FolderId > 0)
                {
                    GetFolder = _myCompanyService.GetChildFolders(RequiredFolder.FolderId);
                    GetAssets = _myCompanyService.GetAssetsByCompanyIDAndFolderID(UserCookieManager.WBStoreId, RequiredFolder.FolderId);
                }
            }
          
            ViewBag.Folders = GetFolder;
            List<TreeViewNodeVM> TreeModel = _myCompanyService.GetTreeVeiwList(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            ViewBag.TreeModel = TreeModel;
            ViewBag.Assets=GetAssets;

            return View("PartialViews/ManageAssets");
        }



        [HttpGet]
        public JsonResult GetFolders()
        {
            List<Folder> GetFolder = _myCompanyService.GetAllFolders(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID).OrderBy(i=>i.FolderName).ToList();

            List<CompanyTerritory> companyTerritories = _myCompanyService.GetAllCompanyTerritories(UserCookieManager.WBStoreId).ToList();
            
            JsonResponse obj = new JsonResponse();
            obj.Folders = GetFolder;
            obj.CompanyTerritories = companyTerritories;
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
        [HttpPost]
        public JsonResult GetFolderIntellisenseData(string prefixText)
        {
            StringBuilder sb = new StringBuilder();
            List<Folder> Folders = FilteredFolderList(prefixText,true);
            foreach (var folder in Folders)
            {
                
                sb.AppendFormat("{0}:", folder.FolderName);
            }
            return Json(sb.ToString(), JsonRequestBehavior.DenyGet);
        }

        public List<Folder> FilteredFolderList(string prefixText,bool Flag)
        {
            List<Folder>FilterFolderList;
            List<Folder>FolderList;
            if (Flag)
            {
                FolderList= _myCompanyService.GetAllFolders(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                FilterFolderList = FolderList.Where(i => i.FolderName.Contains(prefixText)).OrderBy(ad => ad.FolderName).ToList();
            }
            else {
                FolderList = _myCompanyService.GetAllFolders(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                FilterFolderList = FolderList.Where(i => i.FolderName.Equals(prefixText)).OrderBy(ad => ad.FolderName).ToList();
            }
            return FilterFolderList;
        }

        public void CloneItemForManageAsset(long AssetId)
        {

            Asset GetAsset = _myCompanyService.GetAsset(AssetId);

            // refitemid = assetid 
            // image,thubnail path = asset path
            // producttype = 4
            // create section
            // set qty = 1
            // set qty1, qtybase , net total, grosstotal = 0
            // status = 3

            long OrderID = 0;
            long TemporaryRetailCompanyId = UserCookieManager.TemporaryCompanyId;

            if (UserCookieManager.WEBOrderId == 0 || _orderService.IsRealCustomerOrder(UserCookieManager.WEBOrderId, _webclaims.loginContactID(), _webclaims.loginContactCompanyID()) == false)
                {
                    OrderID = _orderService.ProcessPublicUserOrder(string.Empty, UserCookieManager.WEBOrganisationID, (StoreMode)UserCookieManager.WEBStoreMode, _webclaims.loginContactCompanyID(), _webclaims.loginContactID(), ref TemporaryRetailCompanyId);
                    UserCookieManager.WEBOrderId = OrderID;
                    UserCookieManager.TemporaryCompanyId = TemporaryRetailCompanyId;
                }
                else
                {
                    OrderID = UserCookieManager.WEBOrderId;

                    MPC.Models.DomainModels.Estimate oCookieOrder = _orderService.GetOrderByOrderID(OrderID);

                    if (oCookieOrder != null && oCookieOrder.StatusId != (int)OrderStatus.ShoppingCart)
                    {
                        OrderID = _orderService.ProcessPublicUserOrder(string.Empty, UserCookieManager.WEBOrganisationID, (StoreMode)UserCookieManager.WEBStoreMode, _webclaims.loginContactCompanyID(), _webclaims.loginContactID(), ref TemporaryRetailCompanyId);
                        UserCookieManager.WEBOrderId = OrderID;
                        UserCookieManager.TemporaryCompanyId = TemporaryRetailCompanyId;
                        // clone aset
                       //  _myItemService.CloneItem()
                    }

                }

            Item item = _myItemService.CloneItem(UserCookieManager.WEBOrganisationID, OrderID, GetAsset, string.Empty);

            if (item.ItemId > 0)
            {

                Response.Redirect("/ShopCart?Orderid=" + OrderID + "");
            }
        }
        [HttpGet]
        public JsonResult GetStoreTerritories()
        {
            List<CompanyTerritory> companyTerritories = _myCompanyService.GetAllCompanyTerritories(UserCookieManager.WBStoreId).ToList();
            JsonResponse obj = new JsonResponse();
            obj.CompanyTerritories = companyTerritories;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        //public void BindData(long FolderId)
        //{ 
        
        
        //}
        public class JsonResponse
        {
            public List<Folder> Folders;
            public List<Asset> Assets;
            public List<CompanyTerritory> CompanyTerritories;
        }
        //UpdateAsset has been made in the repository
    }
}