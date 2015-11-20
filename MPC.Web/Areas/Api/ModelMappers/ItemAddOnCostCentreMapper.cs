using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Item Addon Cost Centre Mapper
    /// </summary>
    public static class ItemAddOnCostCentreMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemAddOnCostCentre CreateFrom(this DomainModels.ItemAddonCostCentre source)
        {
// ReSharper disable SuggestUseVarKeywordEvident
            ItemAddOnCostCentre itemAddOnCostCentre = new ItemAddOnCostCentre
// ReSharper restore SuggestUseVarKeywordEvident
            {
                ProductAddOnId = source.ProductAddOnId,
                ItemStockOptionId = source.ItemStockOptionId,
                IsMandatory = source.IsMandatory,
                CostCentreId = source.CostCentreId,
                Sequence = source.Sequence,
            };

            if (source.CostCentre == null)
            {
                return itemAddOnCostCentre;
            }

            if (source.CostCentre.CalculationMethodType == 3 )
            {
                var perQtyTotal = ((source.CostCentre.CostPerUnitQuantity * source.CostCentre.UnitQuantity) + source.CostCentre.SetupCost);
                var result = perQtyTotal > source.CostCentre.MinimumCost ? source.CostCentre.CostPerUnitQuantity : source.CostCentre.MinimumCost;
                itemAddOnCostCentre.TotalPrice = result;

            }
            else if (source.CostCentre.CalculationMethodType != 3)
            {
                var result = source.CostCentre.SetupCost > source.CostCentre.MinimumCost ? source.CostCentre.SetupCost : source.CostCentre.MinimumCost;
                itemAddOnCostCentre.TotalPrice = result;
            }

            itemAddOnCostCentre.CostCentreName = source.CostCentre.Name;
            itemAddOnCostCentre.CostCentreType = source.CostCentre.Type;
            itemAddOnCostCentre.CalculationMethodType = source.CostCentre.CalculationMethodType;
            itemAddOnCostCentre.CostCentreTypeName = source.CostCentre.CostCentreType != null ? source.CostCentre.CostCentreType.TypeName : string.Empty;
            itemAddOnCostCentre.CostCentreQuantitySourceType = source.CostCentre.QuantitySourceType;
            itemAddOnCostCentre.CostCentreTimeSourceType = source.CostCentre.TimeSourceType;

            return itemAddOnCostCentre;
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.ItemAddonCostCentre CreateFrom(this ItemAddOnCostCentre source)
        {
            return new DomainModels.ItemAddonCostCentre
            {
                ProductAddOnId = source.ProductAddOnId,
                ItemStockOptionId = source.ItemStockOptionId,
                IsMandatory = source.IsMandatory,
                CostCentreId = source.CostCentreId,
                Sequence = source.Sequence
            };
        }

    }
}