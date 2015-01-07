﻿using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class ProductCategoryResultModel
    {
        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<ProductCategory> ProductCategories { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}