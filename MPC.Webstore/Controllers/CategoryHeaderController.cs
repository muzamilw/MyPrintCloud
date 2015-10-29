using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.Common;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Interfaces.WebStoreServices;

namespace MPC.Webstore.Controllers
{//
    public class CategoryHeaderController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CategoryHeaderController(ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimHelper)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._myClaimHelper = myClaimHelper;
        }

        #endregion
        // GET: CategoryHeader
        public ActionResult Index()
        {
            List<ProductCategory> lstParentCategories = new List<ProductCategory>();

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            {
                ViewBag.DefaultUrl = "/Login";
                long roleid = _myClaimHelper.loginContactRoleID();

                if (_myClaimHelper.loginContactID() > 0)
                {
                    if(roleid == Convert.ToInt64(Roles.Adminstrator) || roleid == Convert.ToInt64(Roles.Manager))
                    {
                        lstParentCategories = _myCompanyService.GetAllParentCorporateCatalog((int)_myClaimHelper.loginContactCompanyID());
                    }
                    else
                    {
                        lstParentCategories = _myCompanyService.GetAllParentCorporateCatalogByTerritory((int)_myClaimHelper.loginContactCompanyID(), (int)_myClaimHelper.loginContactID());
                    }
                }
            }
            else
            {
                ViewBag.DefaultUrl = "/";
                lstParentCategories = _myCompanyService.GetStoreParentCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            }

            if (lstParentCategories != null && lstParentCategories.Count() > 4) 
            {
                lstParentCategories = lstParentCategories.OrderBy(i => i.DisplayOrder).Take(4).ToList();
            }
            return PartialView("PartialViews/CategoryHeader", lstParentCategories);
        }
    }
}