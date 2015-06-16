using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Goods Received Note API Model
    /// </summary>
    public class GoodsReceivedNote
    {

        public int GoodsReceivedId { get; set; }
        public int? PurchaseId { get; set; }
        public string code { get; set; }
        public DateTime? date_Received { get; set; }
        public long? SupplierId { get; set; }
        public int? ContactId { get; set; }
        public string RefNo { get; set; }
        public int? Status { get; set; }
        public int FlagId { get; set; }
        public string Comments { get; set; }
        public string UserNotes { get; set; }
        public Guid? CreatedBy { get; set; }
        public double? Discount { get; set; }
        public int? discountType { get; set; }
        public double? TotalPrice { get; set; }
        public double? NetTotal { get; set; }
        public double? TotalTax { get; set; }
        public double? grandTotal { get; set; }
        public int? isProduct { get; set; }
        public string Tel1 { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public int? CarrierId { get; set; }
        public long? StoreId { get; set; }
        public short IsCustomer { get; set; }
        public string CompanyName { get; set; }
        public List<GoodsReceivedNoteDetail> GoodsReceivedNoteDetails { get; set; }









    }
}