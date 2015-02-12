using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

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

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyContactController(ICompanyService companyService, ICompanyContactService companyContactService)
        {
            this.companyService = companyService;
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
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return companyService.SearchCompanyContacts(request).CreateFrom();
        }

        /// <summary>
        /// Save Contact
        /// </summary>
        public CompanyContact Post(CompanyContact companyContact)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return companyContactService.Save(companyContact.Createfrom()).CreateFrom();
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
            return companyContactService.Delete(request.CompanyContactId);
        }
        #endregion
	}
}