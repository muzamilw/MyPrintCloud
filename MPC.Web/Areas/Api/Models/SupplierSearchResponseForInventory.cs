using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Supplier Search Response Api Model For Inventory
    /// </summary>
    public class SupplierSearchResponseForInventory
    {
        /// <summary>
        /// Suppliers
        /// </summary>
        public IEnumerable<SupplierForInventory> Suppliers { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}