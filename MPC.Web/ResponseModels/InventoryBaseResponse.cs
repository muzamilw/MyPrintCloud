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
        public StockCategoryDropDown StockCategories { get; set; }

        /// <summary>
        /// Stock Sub Categories
        /// </summary>
        public StockSubCategoryDropDown StockSubCategories { get; set; }
    }
}