using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;

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
            MPC.Models.ResponseModels.CompanyContactResponse response = companyContactService.SearchCompanyContacts(request);
            return new CompanyContactResponse
            {
                CompanyContacts = response.CompanyContacts.Select(contact => contact.CreateFrom()),
                RowCount = response.RowCount
            };
        }
        #endregion
    }
}