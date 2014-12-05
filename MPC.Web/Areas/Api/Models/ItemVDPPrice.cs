namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Vdp Price WebApi Model
    /// </summary>
    public class ItemVdpPrice
    {
        #region Public

        public long ItemVdpPriceId { get; set; }
        public int? ClickRangeTo { get; set; }
        public int? ClickRangeFrom { get; set; }
        public double? PricePerClick { get; set; }
        public double? SetupCharge { get; set; }
        public long? ItemId { get; set; }

        #endregion
    }
}