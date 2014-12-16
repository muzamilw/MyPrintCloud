﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.Models;

namespace MPC.Webstore.Controllers
{
    public class HeaderController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public HeaderController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion
        // GET: News
        public ActionResult Index()
        {
            Company model = null;

            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();

            if (baseResponse.Company != null)
            {
                model = baseResponse.Company;
            }


            return PartialView("PartialViews/Header", model);
        }
    }
}