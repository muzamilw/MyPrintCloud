namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Addon Cost Centre WebApi Model
    /// </summary>
    public class ItemAddOnCostCentre
    {
        public int ProductAddOnId { get; set; }
        public long? ItemStockOptionId { get; set; }
        public long? CostCentreId { get; set; }
        public double? DiscountPercentage { get; set; }
        public bool? IsDiscounted { get; set; }
        public int? Sequence { get; set; }
        public bool? IsMandatory { get; set; }
        public string CostCentreName { get; set; }
        public int? CostCentreType { get; set; }
        public string CostCentreTypeName { get; set; }
        public int? CostCentreQuantitySourceType { get; set; }
        public int? CostCentreTimeSourceType { get; set; }
        public double? TotalPrice { get; set; }
    }
}