using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item Addon Cost Centre mapper
    /// </summary>
    public static class ItemAddonCostCentreMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemAddonCostCentre source, ItemAddonCostCentre target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ItemStockOptionId = source.ItemStockOptionId;
            target.IsMandatory = source.IsMandatory;
            target.CostCentreId = source.CostCentreId;
            target.Sequence = source.Sequence;
            target.IsSelectedOnLoad = source.IsSelectedOnLoad;
        }

        #endregion
    }
}
