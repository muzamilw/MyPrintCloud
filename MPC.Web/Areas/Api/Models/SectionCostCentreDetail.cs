namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Section Cost Centre Detail WebApi Model
    /// </summary>
    public class SectionCostCentreDetail
    {
        public int SectionCostCentreDetailId { get; set; }
        public int? SectionCostCentreId { get; set; }
        public long? StockId { get; set; }
        public int? SupplierId { get; set; }
        public double? Qty1 { get; set; }
        public double? Qty2 { get; set; }
        public double? Qty3 { get; set; }
        public double? CostPrice { get; set; }
        public int? ActualQtyUsed { get; set; }
        public string StockName { get; set; }
        public string Supplier { get; set; }
    }
}