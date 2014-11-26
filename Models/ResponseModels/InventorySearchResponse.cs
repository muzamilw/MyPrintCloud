using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// 
    /// </summary>
    public class InventorySearchResponse
    {
        /// <summary>
        ///  StockItems
        /// </summary>
        public IEnumerable<StockItem> StockItems { get; set; }


        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
