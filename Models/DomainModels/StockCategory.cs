using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    public class StockCategory
    {
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Fixed{ get; set; }
        public int ItemWeight { get; set; }
        public int ItemColour { get; set; }
        public int ItemSizeCustom { get; set; }
        public int ItemPaperSize { get; set; }
        public int ItemCoatedType { get; set; }
        public int ItemCoated { get; set; }
        public int ItemExposure { get; set; }
        public int ItemCharge { get; set; }
        public int RecLock { get; set; }
        public int TaxId { get; set; }
        public int Flag1 { get; set; }
        public int Flag2 { get; set; }
        public int Flag3 { get; set; }
        public int Flag4 { get; set; }
        public int CompanyId { get; set; }
        [NotMapped]
        public List<StockSubCategory> StockSubCategories { get; set; }
    }
}
