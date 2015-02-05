using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Item Vdp Price
    /// </summary>
    public class ItemVdpPrice
    {
        #region Persisted Properties
        public long ItemVdpPriceId { get; set; }
        public int? ClickRangeTo { get; set; }
        public int? ClickRangeFrom { get; set; }
        public double? PricePerClick { get; set; }
        public double? SetupCharge { get; set; }
        public long? ItemId { get; set; }

        #endregion


        #region Reference Properties
        public virtual Item Item { get; set; }

        #endregion

        #region Public

        /// <summary>
        /// Creates Clone of Entity
        /// </summary>
        public void Clone(ItemVdpPrice target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemVdpPriceClone_InvalidItem, "target");
            }

            target.ClickRangeFrom = ClickRangeFrom;
            target.ClickRangeTo = ClickRangeTo;
            target.PricePerClick = PricePerClick;
            target.SetupCharge = SetupCharge;
        }

        #endregion
    }
}
