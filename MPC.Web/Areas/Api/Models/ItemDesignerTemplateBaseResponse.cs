using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Designer Template Base Response Web Api Model
    /// </summary>
    public class ItemDesignerTemplateBaseResponse
    {
        /// <summary>
        /// Template Categories
        /// </summary>
        public IEnumerable<ProductCategoryForTemplate> TemplateCategories { get; set; }

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