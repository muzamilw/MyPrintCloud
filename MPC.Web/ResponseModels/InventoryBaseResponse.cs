using System.Collections.Generic;
using MPC.MIS.Models;

namespace MPC.MIS.ResponseModels
{
    /// <summary>
    /// Inventory Base Response Web Model
    /// </summary>
    public class InventoryBaseResponse
    {

        /// <summary>
        /// Stock Categories
        /// </summary>
        public IEnumerable<StockCategoryDropDown> StockCategories { get; set; }

        /// <summary>
        /// Stock Sub Categories
        /// </summary>
        public IEnumerable<StockSubCategoryDropDown> StockSubCategories { get; set; }
    }
}