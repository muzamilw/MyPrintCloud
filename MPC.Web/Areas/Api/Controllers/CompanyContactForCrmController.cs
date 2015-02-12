﻿using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Compnay contacts for crm
    /// </summary>
    public class CompanyContactForCrmController : ApiController
    {
        #region Private
        private readonly ICompanyContactService companyContactService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyContactForCrmController(ICompanyContactService companyContactService)
        {
            this.companyContactService = companyContactService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Addresses / Compnay Contacts
        /// </summary>
        public CompanyContactResponse Get([FromUri] CompanyContactRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int) HttpStatusCode.BadRequest, "Invalid Request");
            }
            return companyContactService.SearchCompanyContacts(request).CreateFrom();
        }

        /// <summary>
        /// Delete Contact
        /// </summary>
        public bool Delete(CompanyContactDeleteModel request)
        {
            if (request == null || !ModelState.IsValid || request.CompanyContactId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            return companyContactService.DeleteContactForCrm(request.CompanyContactId);
        }

        /// <summary>
        /// Get Addresses / Compnay Contacts
        /// </summary>
        public CompanyBaseResponse Get([FromUri] short companyId)
        {
            if (companyId <= 0 || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return companyContactService.GetContactDetail(companyId).CreateFrom();
        }
        #endregion
    }
}