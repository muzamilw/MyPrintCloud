using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Webstore.Controllers
{
    public class ParentCategoriesController : Controller
    {
          #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ParentCategoriesController(ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimHelper)
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
            //List<ProductCategory> lstParentCategories = new List<ProductCategory>();

            //if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
            //{

            //    int roleid = _myClaimHelper.LoginContact().ContactRoleId ?? 0;

            //    if (_myClaimHelper.LoginContact() != null && roleid == Convert.ToInt32(Roles.Adminstrator))
            //    {
            //        lstParentCategories = _myCompanyService.GetAllParentCorporateCatalog((int)_myClaimHelper.LoginContact().CompanyId);
            //    }
            //    else
            //    {
            //        lstParentCategories = _myCompanyService.GetAllParentCorporateCatalogByTerritory((int)_myClaimHelper.LoginContact().CompanyId,(int) _myClaimHelper.LoginContact().ContactId);
            //    }


            //}
            //else
            //{
            //    lstParentCategories = _myCompanyService.GetParentCategories();

              
            //    //rptTopLevelCategory.DataBind();
            //}
            var model = _myCompanyService.GetCompanyParentCategoriesById(UserCookieManager.StoreId);
            return PartialView("PartialViews/ParentCategories",model);
        }
    }
}