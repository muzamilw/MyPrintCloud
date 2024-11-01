﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CrmSupplierController : ApiController
    {
        #region Private

        private readonly ICrmSupplierService crmSupplierService;
        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="crmSupplierService"></param>
        public CrmSupplierController(ICrmSupplierService crmSupplierService, ICompanyService companyService)
        {
            this.crmSupplierService = crmSupplierService;
            this.companyService = companyService;
        }

        #endregion
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewSupplier })]
        [CompressFilterAttribute]
        public CrmSupplierResponse Get([FromUri] CompanyRequestModel request)
        {
            var result = crmSupplierService.GetAllCompaniesOfOrganisation(request);
            return new CrmSupplierResponse
            {
                Companies = result.Companies.Select(x => x.CrmSupplierListViewCreateFrom()),
                RowCount = result.RowCount
            };
        }
    }
}