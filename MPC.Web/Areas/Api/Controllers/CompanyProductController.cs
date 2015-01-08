using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.MIS.ModelMappers;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;
using PagedList;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CompanyProductController : ApiController
    {
        #region Private

        private readonly IItemService itemService;
        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyProductController(IItemService itemService, ICompanyService companyService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }
            this.itemService = itemService;
            this.companyService = companyService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Items
        /// </summary>
        public ItemSearchResponse Get([FromUri] CompanyProductSearchRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return companyService.GetItems(request).CreateFrom();
        }
        #endregion
    }
}