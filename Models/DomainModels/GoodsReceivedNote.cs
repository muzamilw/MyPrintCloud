using System;
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Goods Received Note Domain Model
    /// </summary>
    public class GoodsReceivedNote
    {
        public int GoodsReceivedId { get; set; }
        public int? PurchaseId { get; set; }
        public DateTime? date_Received { get; set; }
        public long? SupplierId { get; set; }
        public double? TotalPrice { get; set; }
        public int? UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public int? ContactId { get; set; }
        public string RefNo { get; set; }
        public string Comments { get; set; }
        public string UserNotes { get; set; }
        public int? isProduct { get; set; }
        public string Tel1 { get; set; }
        public string code { get; set; }
        public double? Discount { get; set; }
        public double? TotalTax { get; set; }
        public double? grandTotal { get; set; }
        public double? NetTotal { get; set; }
        public int? discountType { get; set; }
        public int? Status { get; set; }
        public int? CreatedBy { get; set; }
        public int LastChangedBy { get; set; }
        public int FlagId { get; set; }
        public int LockedBy { get; set; }
        public int? SystemSiteId { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public int? CarrierId { get; set; }
        public virtual Company Company { get; set; }
        public virtual SectionFlag SectionFlag { get; set; }
        public virtual ICollection<GoodsReceivedNoteDetail> GoodsReceivedNoteDetails { get; set; }
    }
}
