using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.ResponseModels
{
    public class DashBoardChartsResponse
    {
          /// <summary>
        /// Count of Pending Orders
        /// </summary>
        public IEnumerable<usp_TotalEarnings_Result> TotalEarningResult { get; set; }

        public IEnumerable<usp_ChartTopPerformingStores_Result> TopPerformingStores { get; set; }

        public IEnumerable<usp_ChartMonthlyOrdersCount_Result> MonthlyOrdersCount { get; set; }
        public IEnumerable<usp_ChartEstimateToOrderConversion_Result> EstimateToOrderConversion { get; set; }
        public IEnumerable<usp_ChartEstimateToOrderConversionCount_Result> EstimateToOrderConversionCount { get; set; }
        public IEnumerable<usp_ChartRegisteredUserByStores_Result>  RegisteredUserByStores { get; set; }
        public IEnumerable<usp_ChartTop10PerfomingCustomers_Result> Top10PerformingCustomers { get; set; }

        public IEnumerable<usp_ChartMonthlyEarningsbyStore_Result> MonthlyEarningsbyStore { get; set; }
        public string CurrencySymbol { get; set; }

        public int SixMonthRegisteredUsers { get; set; }
        public int SixMonthOrdersProcessed { get; set; }
        public double SixMonthDirectOrdersTotal { get; set; }
        public double SixMonthOnlineOrdersTotal { get; set; }

    }
}
