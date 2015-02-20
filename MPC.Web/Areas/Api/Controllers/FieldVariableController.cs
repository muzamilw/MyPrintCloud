﻿using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Field Variable API Controller
    /// </summary>
    public class FieldVariableController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FieldVariableController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Field variables
        /// </summary>
        public FieldVariableResponse Get([FromUri] FieldVariableRequestModel request)
        {
            return companyService.GetFieldVariables(request).CreateFrom();
        }

        [ApiException]
        [HttpPost]
        public long Post(FieldVariable fieldVariable)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return companyService.SaveFieldVariable(fieldVariable.CreateFrom());
        }

        #endregion

    }
}