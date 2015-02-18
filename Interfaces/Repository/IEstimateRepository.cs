﻿using MPC.MIS.Areas.Api.Models;
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
    }
}
