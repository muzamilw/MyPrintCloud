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

        /// <summary>
        /// Paper Sizes List
        /// </summary>
        public IEnumerable<PaperSize> PaperSizes { get; set; }

        /// <summary>
        /// Section Flag
        /// </summary>
        public IEnumerable<SectionFlag> SectionFlags { get; set; }

        /// <summary>
        /// Weight Units
        /// </summary>
        public IEnumerable<WeightUnit> WeightUnits { get; set; }
    }
}
