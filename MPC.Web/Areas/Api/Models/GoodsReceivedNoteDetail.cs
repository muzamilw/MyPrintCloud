namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Goods Received Note Detail API Model
    /// </summary>
    public class GoodsReceivedNoteDetail
    {

        public int GoodsReceivedDetailId { get; set; }
        public double? QtyReceived { get; set; }
        public double? Price { get; set; }
        public int? PackQty { get; set; }
        public double? TotalOrderedqty { get; set; }
        public string Details { get; set; }
        public string ItemCode { get; set; }
        public double? TotalPrice { get; set; }
        public double? NetTax { get; set; }
        public double? Discount { get; set; }
        public int? FreeItems { get; set; }
        public double? TaxValue { get; set; }
        public long? RefItemId { get; set; }
        public int? ProductType { get; set; }

    }
}