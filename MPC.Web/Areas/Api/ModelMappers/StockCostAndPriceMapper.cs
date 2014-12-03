using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Stock Cost And Price
    /// </summary>
    public static class StockCostAndPriceMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static StockCostAndPrice CreateFrom(this DomainModels.StockCostAndPrice source)
        {
            return new StockCostAndPrice
            {
                CostPriceId = source.CostPriceId,
                CostPrice = source.CostPrice,
                PackCostPrice = source.PackCostPrice,
                FromDate = source.FromDate,
                ToDate = source.ToDate,
                CostOrPriceIdentifier = source.CostOrPriceIdentifier,
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.StockCostAndPrice CreateFrom(this StockCostAndPrice source)
        {
            return new DomainModels.StockCostAndPrice
            {
                CostPriceId = source.CostPriceId,
                CostPrice = source.CostPrice,
                PackCostPrice = source.PackCostPrice,
                FromDate = source.FromDate,
                ToDate = source.ToDate,
                CostOrPriceIdentifier = source.CostOrPriceIdentifier,
            };
        }

        #endregion
    }
}