using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CompanyContactController : ApiController
    {
       #region Private

        private readonly ICompanyService companyService;
        private readonly ICompanyContactService companyContactService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyContactController(ICompanyService companyService, ICompanyContactService companyContactService)
        {
            this.companyService = companyService;
            this.companyContactService = companyContactService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Addresses
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Models.CompanyContactResponse Get([FromUri] CompanyContactRequestModel request)
        {
            var result = companyService.SearchCompanyContacts(request);
            return new Models.CompanyContactResponse
            {
                CompanyContacts = result.CompanyContacts.Select(x => x.CreateFrom()),
                RowCount = result.RowCount
            };
        }
        public bool Delete(CompanyContactDeleteModel request)
        {
            if (request == null || !ModelState.IsValid || request.CompanyContactId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            return companyContactService.Delete(request.CompanyContactId);
        }
        #endregion
	}
}