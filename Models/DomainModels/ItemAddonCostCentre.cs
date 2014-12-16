
namespace MPC.Models.DomainModels
{
    public class ItemAddonCostCentre
    {
        public int ProductAddOnId { get; set; }
        public long? ItemStockOptionId { get; set; }
        public long? CostCentreId { get; set; }
        public double? DiscountPercentage { get; set; }
        public bool? IsDiscounted { get; set; }
        public int? Sequence { get; set; }
        public bool? IsMandatory { get; set; }

        public virtual CostCentre CostCentre { get; set; }

        public virtual ItemStockOption ItemStockOption { get; set; }
    }
}
