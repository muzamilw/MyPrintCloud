namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Goods Received Note Detail Domain Model
    /// </summary>
    public class GoodsReceivedNoteDetail
    {
        public int GoodsReceivedDetailId { get; set; }
        public long? ItemId { get; set; }
        public double? QtyReceived { get; set; }
        public int? GoodsreceivedId { get; set; }
        public double? Price { get; set; }
        public int? PackQty { get; set; }
        public double? TotalOrderedqty { get; set; }
        public string Details { get; set; }
        public string ItemCode { get; set; }
        public string Name { get; set; }
        public double? TotalPrice { get; set; }
        public int? TaxId { get; set; }
        public double? NetTax { get; set; }
        public double? Discount { get; set; }
        public int? FreeItems { get; set; }
        public int? DepartmentId { get; set; }
        public double? TaxValue { get; set; }
        public long? RefItemId { get; set; }
        public int? ProductType { get; set; }
        public virtual Item Item { get; set; }
        public virtual GoodsReceivedNote GoodsReceivedNote { get; set; }
    }
}
