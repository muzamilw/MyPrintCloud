using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Order Statuses Response Model
    /// </summary>
    public class OrderStatusesResponse
    {
        /// <summary>
        /// Count of Pending Orders
        /// </summary>
        public int PendingOrdersCount { get; set; }

        /// <summary>
        /// Count of In- Productin Orders
        /// </summary>
        public int InProductionOrdersCount { get; set; }

        /// <summary>
        /// Count of Completed Orders
        /// </summary>
        public int CompletedOrdersCount { get; set; }

        /// <summary>
        /// Count of Un-Confirmed Orders
        /// </summary>
        public int UnConfirmedOrdersCount { get; set; }

        /// <summary>
        /// Count of Live stores
        /// </summary>
        public int LiveStoresCount { get; set; }

        /// <summary>
        /// Count of Orders current month
        /// </summary>
        public int CurrentMonthOdersCount { get; set; }

        /// <summary>
        /// Estimate Total
        /// </summary>
        public double? TotalEarnings { get; set; }

        /// <summary>
        /// Estimates / Orders list
        /// </summary>
        public IEnumerable<Estimate> Estimates { get; set; }

        /// <summary>
        /// List of Companies
        /// </summary>
        public IEnumerable<Company> Companies { get; set; }
    }
}