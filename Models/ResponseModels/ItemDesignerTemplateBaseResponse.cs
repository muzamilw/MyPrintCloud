using System.Collections.Generic;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Item Designer Template Base Response
    /// </summary>
    public class ItemDesignerTemplateBaseResponse
    {
        /// <summary>
        /// Template Categories
        /// </summary>
        public IEnumerable<ProductCategory> TemplateCategories { get; set; }

        /// <summary>
        /// Category Regions
        /// </summary>
        public IEnumerable<CategoryRegion> CategoryRegions { get; set; }

        /// <summary>
        /// Category Types
        /// </summary>
        public IEnumerable<CategoryType> CategoryTypes { get; set; }
    }
}
