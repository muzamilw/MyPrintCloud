using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class GetCompanyContactVariableByCompanyIdController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetCompanyContactVariableByCompanyIdController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Scope Variables
        /// </summary>
        public IEnumerable<ScopeVariable> Get([FromUri]long companyId,int scope)
        {
            return companyService.GetFieldVariableByCompanyIdAndScope(companyId, scope).Select(cv => cv.CreateFromFieldVariable());
        }

        #endregion
    }
}