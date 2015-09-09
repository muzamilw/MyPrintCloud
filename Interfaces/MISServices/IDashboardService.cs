using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

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

        /// <summary>
        /// Get Total Earnings For Dashboard
        /// </summary>
        DashBoardChartsResponse GetChartsForDashboard();
    }
}
