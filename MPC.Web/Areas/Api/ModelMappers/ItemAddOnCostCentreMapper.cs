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
            ItemAddOnCostCentre itemAddOnCostCentre = new ItemAddOnCostCentre
            {
                ProductAddOnId = source.ProductAddOnId,
                ItemStockOptionId = source.ItemStockOptionId,
                IsMandatory = source.IsMandatory,
                CostCentreId = source.CostCentreId
            };

            if (source.CostCentre == null)
            {
                return itemAddOnCostCentre;
            }

            itemAddOnCostCentre.CostCentreName = source.CostCentre.Name;
            itemAddOnCostCentre.CostCentreType = source.CostCentre.Type;

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
                CostCentreId = source.CostCentreId
            };
        }

    }
}