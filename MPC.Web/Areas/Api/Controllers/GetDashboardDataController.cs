using System.Net;
using System.Web;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using System.Web.Http;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class GetDashboardDataController : ApiController
    {
       #region Private
        private readonly IDashboardService   _dashboardService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetDashboardDataController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        #endregion
        #region Public
        /// <summary>
        /// Get Addresses / Compnay Contacts
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewDashboard })]
        [CompressFilter]
        public Models.OrderStatusesResponse Get([FromUri] DashboardRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return _dashboardService.GetOrderStatusesCount(request).CreateFrom();
        }
        #endregion
    }
}