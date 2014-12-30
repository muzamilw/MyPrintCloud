namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Item Price Matrix Domain Model
    /// </summary>
    public class ItemPriceMatrix
    {
        public long PriceMatrixId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public long? ItemId { get; set; }
        public double? PricePaperType1 { get; set; }
        public double? PricePaperType2 { get; set; }
        public double? PricePaperType3 { get; set; }
        public int? QtyRangeFrom { get; set; }
        public int? QtyRangeTo { get; set; }
        public int? SupplierId { get; set; }
        public double? PriceStockType4 { get; set; }
        public double? PriceStockType5 { get; set; }
        public double? PriceStockType6 { get; set; }
        public double? PriceStockType7 { get; set; }
        public double? PriceStockType8 { get; set; }
        public double? PriceStockType9 { get; set; }
        public double? PriceStockType10 { get; set; }
        public double? PriceStockType11 { get; set; }
        public int? FlagId { get; set; }
        public int? SupplierSequence { get; set; }
        public virtual Item Item { get; set; }
    }
}
