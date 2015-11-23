using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class StockCategorySubCategoryResponse
    {

        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Stock Categories
        /// </summary>
        public IEnumerable<StockCategoryDropDown> StockCategories { get; set; }


        /// <summary>
        /// List of Stock sub Categories
        /// </summary>
        public IEnumerable<StockSubCategoryDropDown> StockSubCategories { get; set; }
    }


}