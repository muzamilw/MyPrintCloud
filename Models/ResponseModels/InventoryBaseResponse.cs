using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Inventory Base Response
    /// </summary>
    public class InventoryBaseResponse
    {
        /// <summary>
        /// Stock Categories
        /// </summary>
        public IEnumerable<StockCategory> StockCategories { get; set; }

        /// <summary>
        /// Stock Sub Category List
        /// </summary>
        public IEnumerable<StockSubCategory> StockSubCategories { get; set; }
    }
}
