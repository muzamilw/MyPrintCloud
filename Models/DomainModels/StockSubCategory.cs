using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    public class StockSubCategory
    {
        public int SubCategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Fixed { get; set; }
        public int CategoryId { get; set; }
        public virtual StockCategory StockCategory { get; set; }
        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
