using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Order Response Model
    /// </summary>
    public class GetOrdersResponse
    {
        /// <summary>
        /// Orders
        /// </summary>
        public IEnumerable<EstimateListView> Orders { get; set; }
        
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
        public string HeadNote { get; set; }
        public string FootNote { get; set; }
    }
}
