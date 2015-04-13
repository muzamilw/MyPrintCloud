
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Dashboard service interface
    /// </summary>
    public interface IDashboardService
    {
        /// <summary>
        /// Get Order Statuses Response
        /// </summary>
        OrderStatusesResponse GetOrderStatusesCount(DashboardRequestModel request);
    }
}
