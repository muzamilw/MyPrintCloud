using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Order Response Model
    /// </summary>
    public class GetOrdersResponse
    {
        /// <summary>
        /// Orders
        /// </summary>
        public IEnumerable<Estimate> Orders { get; set; }
        
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
        public string HeadNote { get; set; }
        public string FootNote { get; set; }
    }
}
