using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// SectionCostCentreDetail Mapper
    /// </summary>
    public static class SectionCostCentreDetailMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static SectionCostCentreDetail CreateFrom(this DomainModels.SectionCostCentreDetail source)
        {
            return new SectionCostCentreDetail
            {
                SectionCostCentreDetailId = source.SectionCostCentreDetailId,
                Qty1 = source.Qty1,
                Qty2 = source.Qty2,
                Qty3 = source.Qty3,
                StockId = source.StockId,
                StockName = source.StockName,
                SectionCostCentreId = source.SectionCostCentreId,
                CostPrice = source.CostPrice,
                ActualQtyUsed = source.ActualQtyUsed
            };
        }

        /// <summary>
        /// Create From WebApi Model
        /// </summary>
        public static DomainModels.SectionCostCentreDetail CreateFrom(this SectionCostCentreDetail source)
        {
            return new DomainModels.SectionCostCentreDetail
            {
                SectionCostCentreDetailId = source.SectionCostCentreDetailId,
                Qty1 = source.Qty1,
                Qty2 = source.Qty2,
                Qty3 = source.Qty3,
                StockId = source.StockId,
                StockName = source.StockName,
                SectionCostCentreId = source.SectionCostCentreId,
                CostPrice = source.CostPrice,
                ActualQtyUsed = source.ActualQtyUsed
            };
        }

    }
}