
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;


namespace MPC.MIS.Areas.Api.Controllers
{
    public class GetCompanyContactVariableController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetCompanyContactVariableController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Field variables
        /// </summary>
        public IEnumerable<CompanyContactVariable> Get([FromUri]long contactId)
        {
            return companyService.GetContactVariableByContactId(contactId).Select(cv => cv.CreateFrom());
        }

        #endregion

    }
}