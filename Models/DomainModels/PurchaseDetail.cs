namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Purchase Detail Domain Model
    /// </summary>
    public class PurchaseDetail
    {
        public int PurchaseDetailId { get; set; }
        public long? ItemId { get; set; }
        public double? quantity { get; set; }
        public int? PurchaseId { get; set; }
        public double? price { get; set; }
        public int? packqty { get; set; }
        public string ItemCode { get; set; }
        public string ServiceDetail { get; set; }
        public string ItemName { get; set; }
        public int? TaxId { get; set; }
        public double? TotalPrice { get; set; }
        public double? Discount { get; set; }
        public double? NetTax { get; set; }
        public int? freeitems { get; set; }
        public double? ItemBalance { get; set; }
        public int? DepartmentId { get; set; }
        public long? RefItemId { get; set; }
        public int? ProductType { get; set; }

        public virtual Purchase Purchase { get; set; }
        public virtual Item Item { get; set; }
    }
}
