using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class StockCategoryResponse
    {  /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Stock Categories
        /// </summary>
        public IEnumerable<StockCategory> StockCategories{ get; set; }

        /// <summary>
        /// List of Stock Sub Categories
        /// </summary>
        public IEnumerable<StockSubCategory> StockSubCategories { get; set; } 

    }
}