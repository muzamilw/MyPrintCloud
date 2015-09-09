using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Purchase API Model
    /// </summary>
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public string Code { get; set; }
        public DateTime? date_Purchase { get; set; }
        public long? SupplierId { get; set; }
        public int ContactId { get; set; }
        public string RefNo { get; set; }
        public int? isproduct { get; set; }
        public int? Status { get; set; }
        public int SupplierContactAddressID { get; set; }
        public string SupplierContactCompany { get; set; }
        public int? FlagID { get; set; }
        public string Comments { get; set; }
        public string FootNote { get; set; }
        public Guid? CreatedBy { get; set; }
        public double? Discount { get; set; }
        public int? discountType { get; set; }
        public double? TotalPrice { get; set; }
        public double? NetTotal { get; set; }
        public double? TotalTax { get; set; }
        public double? GrandTotal { get; set; }
        public long? StoreId { get; set; }
        public short IsCustomer { get; set; }
   
        public  List<PurchaseDetail> PurchaseDetails { get; set; }
    }
}