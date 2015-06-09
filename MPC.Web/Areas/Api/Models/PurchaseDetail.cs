namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// /Purchase Detail API Model
    /// </summary>
    public class PurchaseDetail
    {
        public int PurchaseDetailId { get; set; }
        public long? ItemId { get; set; }
        public double? quantity { get; set; }
        public double? price { get; set; }
        public int? packqty { get; set; }
        public string ItemCode { get; set; }
        public string ServiceDetail { get; set; }
        public double? TotalPrice { get; set; }
        public double? Discount { get; set; }
        public double? NetTax { get; set; }
        public int? freeitems { get; set; }
        public long? RefItemId { get; set; }
        public int? ProductType { get; set; }
        public double? TaxValue { get; set; }
    }
}