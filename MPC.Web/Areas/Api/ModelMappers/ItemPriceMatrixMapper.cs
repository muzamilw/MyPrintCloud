using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Item Price Matrix WebApi Mapper
    /// </summary>
    public static class ItemPriceMatrixMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemPriceMatrix CreateFrom(this DomainModels.ItemPriceMatrix source)
        {
            return new ItemPriceMatrix
            {
                PriceMatrixId = source.PriceMatrixId,
                ItemId = source.ItemId,
                Quantity = source.Quantity,
                QtyRangeFrom = source.QtyRangeFrom,
                QtyRangeTo = source.QtyRangeTo,
                PricePaperType1 = source.PricePaperType1,
                PricePaperType2 = source.PricePaperType2,
                PricePaperType3 = source.PricePaperType3,
                PriceStockType4 = source.PriceStockType4,
                PriceStockType5 = source.PriceStockType5,
                PriceStockType6 = source.PriceStockType6,
                PriceStockType7 = source.PriceStockType7,
                PriceStockType8 = source.PriceStockType8,
                PriceStockType9 = source.PriceStockType9,
                PriceStockType10 = source.PriceStockType10,
                PriceStockType11 = source.PriceStockType11
            };
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.ItemPriceMatrix CreateFrom(this ItemPriceMatrix source)
        {
            return new DomainModels.ItemPriceMatrix
            {
                PriceMatrixId = source.PriceMatrixId,
                ItemId = source.ItemId,
                Quantity = source.Quantity,
                QtyRangeFrom = source.QtyRangeFrom,
                QtyRangeTo = source.QtyRangeTo,
                PricePaperType1 = source.PricePaperType1,
                PricePaperType2 = source.PricePaperType2,
                PricePaperType3 = source.PricePaperType3,
                PriceStockType4 = source.PriceStockType4,
                PriceStockType5 = source.PriceStockType5,
                PriceStockType6 = source.PriceStockType6,
                PriceStockType7 = source.PriceStockType7,
                PriceStockType8 = source.PriceStockType8,
                PriceStockType9 = source.PriceStockType9,
                PriceStockType10 = source.PriceStockType10,
                PriceStockType11 = source.PriceStockType11
            };
        }

    }
}