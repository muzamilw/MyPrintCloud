using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Models;

namespace MPC.MIS.ModelMappers
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
        public static Models.StockCostAndPrice CreateFrom(this DomainModels.StockCostAndPrice source)
        {
            return new Models.StockCostAndPrice
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
        public static DomainModels.StockCostAndPrice CreateFrom(this Models.StockCostAndPrice source)
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