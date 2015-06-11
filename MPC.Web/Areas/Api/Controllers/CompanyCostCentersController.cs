using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Activity Detail API Controller
    /// </summary>
    public class CompanyCostCentersController : ApiController
    {
        #region Private

        private readonly ICompanyCostCentreService companyCostCentreService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyCostCentersController(ICompanyCostCentreService companyCostCentreService)
        {
            this.companyCostCentreService = companyCostCentreService;
        }

        #endregion

        #region Public


        /// <summary>
        /// Get Activity Detail By ID
        /// </summary>
        [CompressFilterAttribute]
        public MPC.MIS.Areas.Api.Models.CostCentreResponseModel Get([FromUri]GetCostCentresRequest requestModel)
        {
            return companyCostCentreService.GetCompanyCostCentreByCompanyId(requestModel).CreateFrom();
        }

        #endregion
    }
}