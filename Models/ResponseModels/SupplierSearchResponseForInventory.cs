using System.Collections;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Supplier Search Response For Inventory
    /// </summary>
    public class SupplierSearchResponseForInventory
    {
        /// <summary>
        /// Suppliers
        /// </summary>
        public IEnumerable<Company> Suppliers { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
