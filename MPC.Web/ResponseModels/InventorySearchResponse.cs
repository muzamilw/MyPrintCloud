using System.Collections.Generic;
using MPC.MIS.Models;

namespace MPC.MIS.ResponseModels
{
    /// <summary>
    /// Inventory Search Response Web Reponse
    /// </summary>
    public class InventorySearchResponse
    {
        /// <summary>
        ///  StockItems
        /// </summary>
        public IEnumerable<StockItemForListView> StockItems { get; set; }


        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}