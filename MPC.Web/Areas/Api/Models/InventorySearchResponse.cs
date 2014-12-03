using System.Collections.Generic;
using MPC.MIS.Models;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Inventory Search Response Web Reponse
    /// </summary>
    public class InventorySearchResponse
    {
        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<StockItemForListView> StockItems { get; set; }
        
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}