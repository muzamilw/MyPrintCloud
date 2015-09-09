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
    public class OxfordAboutUsController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        // GET: OxfordAboutUs
        public OxfordAboutUsController(ICompanyService _myCompanyService, IWebstoreClaimsHelperService _myClaimHelper)
        {
            this._myCompanyService = _myCompanyService;
            this._myClaimHelper = _myClaimHelper;
        }
        public ActionResult Index()
        {
            //List<ProductCategory> AllCategroies = new List<ProductCategory>();
            ////List<ProductCategory> ChildCategories = new List<ProductCategory>();
            //AllCategroies = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);

            //ViewBag.ParentCategory = AllCategroies.Where(i => i.ParentCategoryId == null || i.ParentCategoryId == 0).OrderBy(g => g.DisplayOrder).ToList();

            //// ChildCategories=_myCompanyService.GetAllCategories(UserCookieManager.WBStoreId);

            //ViewBag.AllChildCategories = AllCategroies.Where(i => i.ParentCategoryId != null || i.ParentCategoryId != 0).OrderBy(g => g.DisplayOrder).ToList();
            List<ProductCategory> lstParentCategories = new List<ProductCategory>();
            List<ProductCategory> AllRetailCat = new List<ProductCategory>();
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            {
                Int64 roleid = _myClaimHelper.loginContactRoleID();

                if (_myClaimHelper.loginContactID() != 0 && roleid == Convert.ToInt32(Roles.Adminstrator))
                {
                    lstParentCategories = _myCompanyService.GetAllParentCorporateCatalog((int)_myClaimHelper.loginContactCompanyID());
                }
                else
                {
                    lstParentCategories = _myCompanyService.GetAllParentCorporateCatalogByTerritory((int)_myClaimHelper.loginContactCompanyID(), (int)_myClaimHelper.loginContactID());
                }

            }
            else
            {

                List<ProductCategory> AllCategroies = new List<ProductCategory>();
                //List<ProductCategory> ChildCategories = new List<ProductCategory>();
                AllCategroies = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                //SeablueToCategories
                ViewBag.lstParentCategories = _myCompanyService.GetStoreParentCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID).OrderBy(i => i.DisplayOrder).ToList();
                //rptRetroPCats
                // ViewBag.AllRetailCat = _myCompanyService.GetAllRetailPublishedCat().Where(i => i.ParentCategoryId == null || i.ParentCategoryId == 0).OrderBy(g => g.DisplayOrder).ToList();
                ViewBag.AllRetailCat = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID).ToList();
            }


            return PartialView("PartialViews/OxfordAboutUs");
        }
    }
}