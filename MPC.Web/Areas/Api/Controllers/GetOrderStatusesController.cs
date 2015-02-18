using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Controller to Get Order's Statuses
    /// </summary>
    public class GetOrderStatusesController : ApiController
    {
        #region Private
        private readonly IDashboardService   dashboardService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetOrderStatusesController( IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Addresses / Compnay Contacts
        /// </summary>
        public OrderStatusesResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return dashboardService.GetOrderStatusesCount();
        }
        #endregion
    }
}