using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        /// Get Total Earnings Result
        /// </summary>
        IEnumerable<usp_TotalEarnings_Result> GetTotalEarnings(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Load Property
        /// </summary>
        void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false);

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
        /// <summary>
        /// Get Orders For Estimates List View
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetOrdersResponse GetOrdersForEstimates(GetOrdersRequest request);

        Estimate GetEstimateWithCompanyByOrderID(long OrderID);
        long GetEstimateIdOfInquiry(long inquiryId);

        /// <summary>
        /// Get Total Earnings For Dashboard
        /// </summary>
        IEnumerable<usp_TotalEarnings_Result> GetTotalEarningsForDashboard();
    }
}
