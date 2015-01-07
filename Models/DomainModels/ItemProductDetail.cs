namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Item Product Detail Domain Model
    /// </summary>
    public class ItemProductDetail
    {
        public int ItemDetailId { get; set; }
        public long? ItemId { get; set; }
        public bool? isInternalActivity { get; set; }
        public bool? isAutoCreateSupplierPO { get; set; }
        public bool? isQtyLimit { get; set; }
        public int? QtyLimit { get; set; }
        public int? DeliveryTimeSupplier1 { get; set; }
        public int? DeliveryTimeSupplier2 { get; set; }
        public bool? isPrintItem { get; set; }
        public bool? isAllowMarketBriefAttachment { get; set; }
        public string MarketBriefSuccessMessage { get; set; }
        public virtual Item Item { get; set; }
    }
}
