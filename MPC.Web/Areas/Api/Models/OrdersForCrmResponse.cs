using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Api response model
    /// </summary>
    public class OrdersForCrmResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }
        public string CurrencySymbol { get; set; }
        /// <summary>
        /// List of Companies
        /// </summary>
        public IEnumerable<EstimateListView> OrdersList { get; set; }
    }
}