using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.WebBase.Mvc;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Interfaces.MISServices;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CostCenterExecutionController : ApiController
    {
        #region Private

        private readonly ICostCentersService CostCenterService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CostCenterExecutionController(ICostCentersService costcenterService)
        {
            if (costcenterService == null)
            {
                throw new ArgumentNullException("costcenterService");
            }
            this.CostCenterService = costcenterService;
        }

        #endregion

        #region Public
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        public double GetCostCenterResult([FromUri] CostCenterExecutionRequest request)
        {
            double dblResult = 0;
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return dblResult;
        }
        public CostCenterExecutionResponse GetCostCenterPrompts([FromUri] CostCenterExecutionRequest request, bool isPromptMode)
        {
            CostCenterExecutionResponse response = null;
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return response;
        }
        #endregion
    }
}
