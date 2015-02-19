using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Field Variable Detail API Controller
    /// </summary>
    public class FieldVariableDetailController : ApiController
    {
         #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FieldVariableDetailController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Field Variable Detail By ID
        /// </summary>
        public FieldVariable Get([FromUri]long fieldVariableId)
        {
            return companyService.GetFieldVariableDetail(fieldVariableId).CreateFrom();
        }
        #endregion
    }
}