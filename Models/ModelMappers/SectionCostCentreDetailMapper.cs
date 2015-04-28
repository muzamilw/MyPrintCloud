using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Section Cost Centre mapper
    /// </summary>
    public static class SectionCostCentreDetailMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this SectionCostCentreDetail source, SectionCostCentreDetail target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.SectionCostCentreDetailId = source.SectionCostCentreDetailId;
            target.StockName = source.StockName;
            target.SectionCostCentreId = source.SectionCostCentreId;
            target.Qty1 = source.Qty1;
            target.Qty2 = source.Qty2;
            target.Qty3 = source.Qty3;
            target.CostPrice = source.CostPrice;
            target.StockId = source.StockId;
        }

        #endregion
    }
}
