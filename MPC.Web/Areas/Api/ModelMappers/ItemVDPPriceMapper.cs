using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Item Vdp Price Mapper
    /// </summary>
    public static class ItemVdpPriceMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemVdpPrice CreateFrom(this DomainModels.ItemVdpPrice source)
        {
            return new ItemVdpPrice
            {
                ItemVdpPriceId = source.ItemVdpPriceId,
                ItemId = source.ItemId,
                ClickRangeFrom = source.ClickRangeFrom,
                ClickRangeTo = source.ClickRangeTo,
                PricePerClick = source.PricePerClick,
                SetupCharge = source.SetupCharge
            };
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.ItemVdpPrice CreateFrom(this ItemVdpPrice source)
        {
            return new DomainModels.ItemVdpPrice
            {
                ItemVdpPriceId = source.ItemVdpPriceId,
                ItemId = source.ItemId,
                ClickRangeFrom = source.ClickRangeFrom,
                ClickRangeTo = source.ClickRangeTo,
                PricePerClick = source.PricePerClick,
                SetupCharge = source.SetupCharge
            };
        }

    }
}