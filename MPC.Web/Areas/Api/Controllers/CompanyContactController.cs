using System.Collections.Generic;
using System.IO;
using System.Text;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.Common;
using MPC.Models.RequestModels;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.WebBase.Mvc;
using Newtonsoft.Json;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Company Contact Controller 
    /// </summary>
    public class CompanyContactController : ApiController
    {
        #region Private
        private readonly ICompanyService companyService;
        private readonly ICompanyContactService companyContactService;
        private readonly IMyOrganizationService organisationService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyContactController(ICompanyService companyService, ICompanyContactService companyContactService, IMyOrganizationService myOrganizationService)
        {
            this.companyService = companyService;
            this.companyContactService = companyContactService;
            this.organisationService = myOrganizationService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Addresses / Compnay Contacts
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CompanyContactResponse Get([FromUri] CompanyContactRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return companyService.SearchCompanyContacts(request).CreateFrom();
        }

        /// <summary>
        /// Save Contact
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CompanyContact Post(CompanyContact companyContact)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var savedContact = companyContactService.Save(companyContact.Createfrom()).CreateFrom();
            companyContactService.PostDataToZapier(savedContact.ContactId);
            return savedContact;
        }


      

        /// <summary>
        /// Delete Contact
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CompanyContact Delete(CompanyContactDeleteModel request)
        {
            if (request == null || !ModelState.IsValid || request.CompanyContactId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }
            return companyContactService.Delete(request.CompanyContactId).CreateFrom();
        }

        
        #endregion
	}
}