using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;


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
        /// Get Scope variables
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        public IEnumerable<ScopeVariable> Get([FromUri]long id, int scope)
        {
            return companyService.GetContactVariableByContactId(id, scope).Select(cv => cv.CreateFrom());
        }

        #endregion

    }
}