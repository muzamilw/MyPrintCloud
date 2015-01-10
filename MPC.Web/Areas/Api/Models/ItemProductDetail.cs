namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Product Detail WebApi Model
    /// </summary>
    public class ItemProductDetail
    {
        public int ItemDetailId { get; set; }
        public long? ItemId { get; set; }
        public bool? IsInternalActivity { get; set; }
// ReSharper disable InconsistentNaming
        public bool? IsAutoCreateSupplierPO { get; set; }
// ReSharper restore InconsistentNaming
        public bool? IsQtyLimit { get; set; }
        public int? QtyLimit { get; set; }
        public int? DeliveryTimeSupplier1 { get; set; }
        public int? DeliveryTimeSupplier2 { get; set; }
        public bool? IsPrintItem { get; set; }
        public bool? IsAllowMarketBriefAttachment { get; set; }
        public string MarketBriefSuccessMessage { get; set; }
    }
}