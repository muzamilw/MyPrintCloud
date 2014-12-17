﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.Common;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Interfaces.WebStoreServices;

namespace MPC.Webstore.Controllers
{
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

            if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
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
                lstParentCategories = _myCompanyService.GetStoreParentCategories(UserCookieManager.StoreId);
            }

            return PartialView("PartialViews/CategoryHeader", lstParentCategories);
        }
    }
}