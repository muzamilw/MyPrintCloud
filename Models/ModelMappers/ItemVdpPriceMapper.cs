using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item Vdp Price mapper
    /// </summary>
    public static class ItemVdpPriceMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemVdpPrice source, ItemVdpPrice target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ItemVdpPriceId = source.ItemVdpPriceId;
            target.ItemId = source.ItemId;
            target.ClickRangeFrom = source.ClickRangeFrom;
            target.ClickRangeTo = source.ClickRangeTo;
            target.PricePerClick = source.PricePerClick;
            target.SetupCharge = source.SetupCharge;
        }

        #endregion
    }
}
