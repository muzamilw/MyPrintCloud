using System.Collections.Generic;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Estimate Repository 
    /// </summary>
    public interface IEstimateRepository : IBaseRepository<Estimate, long>
    {
        /// <summary>
        /// Get Estimates
        /// </summary>
        GetOrdersResponse GetOrders(GetOrdersRequest request);

        /// <summary>
        /// Get Order Statuses Response
        /// </summary>
        OrderStatusesResponse GetOrderStatusesCount();

        /// <summary>
        /// Gets list of Orders for company edit tab
        /// </summary>
        OrdersForCrmResponse GetOrdersForCrm(GetOrdersRequest model);

        /// <summary>
        /// Gives count of new orders by given number of last dats
        /// </summary>
        int GetNewOrdersCount(int noOfLastDays, long companyId);
        /// <summary>
        /// Get Order Statuses Count For Menu Items
        /// </summary>
        /// <returns></returns>
        OrderMenuCount GetOrderStatusesCountForMenuItems();
        /// <summary>
        /// Get Orders For Dashboard Screen
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IEnumerable<Estimate> GetEstimatesForDashboard(DashboardRequestModel request);
    }
}
