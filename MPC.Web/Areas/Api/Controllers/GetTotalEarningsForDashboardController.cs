using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;
using MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.Controllers
{

    /// <summary>
    /// Get Total Earnings For Dashboard API Controller
    /// </summary>
    public class GetTotalEarningsForDashboardController : ApiController
    {
        #region Private
        private readonly IDashboardService dashboardService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetTotalEarningsForDashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Addresses / Compnay Contacts
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewDashboard })]
        [CompressFilterAttribute]
        public DashBoardChartsResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return dashboardService.GetChartsForDashboard();
        }

        #endregion
    }
}