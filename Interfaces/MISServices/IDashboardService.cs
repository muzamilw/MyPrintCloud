
using MPC.MIS.Areas.Api.Models;

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
        OrderStatusesResponse GetOrderStatusesCount();
    }
}
