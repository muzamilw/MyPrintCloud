using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System.Runtime.Caching;

namespace MPC.Webstore.Controllers
{
    public class ProductParentCategoriesController : Controller
    {
          #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductParentCategoriesController(ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimHelper)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._myClaimHelper = myClaimHelper;
        }

        #endregion
        // GET: ParentCategories
        public ActionResult Index()
        {
            List<ProductCategory> lstParentCategories = new List<ProductCategory>();

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
                lstParentCategories = _myCompanyService.GetStoreParentCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
            }

           
            return PartialView("PartialViews/ProductParentCategories", lstParentCategories);
        }
    }
}