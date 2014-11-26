using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class StockCategoryResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Stock Categories
        /// </summary>
        public IEnumerable<StockCategory> StockCategories{ get; set; } 

    }
}
