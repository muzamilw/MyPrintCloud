//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility.Preview
{
    using System;
    using System.Collections.Generic;
    
    public partial class GoodsReceivedNote
    {
        public GoodsReceivedNote()
        {
            this.GoodsReceivedNoteDetails = new HashSet<GoodsReceivedNoteDetail>();
        }
    
        public int GoodsReceivedId { get; set; }
        public Nullable<int> PurchaseId { get; set; }
        public Nullable<System.DateTime> date_Received { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public Nullable<int> ContactId { get; set; }
        public string RefNo { get; set; }
        public string Comments { get; set; }
        public string UserNotes { get; set; }
        public Nullable<int> isProduct { get; set; }
        public string Tel1 { get; set; }
        public string code { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<double> TotalTax { get; set; }
        public Nullable<double> grandTotal { get; set; }
        public Nullable<double> NetTotal { get; set; }
        public Nullable<int> discountType { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public int LastChangedBy { get; set; }
        public int FlagId { get; set; }
        public int LockedBy { get; set; }
        public Nullable<int> SystemSiteId { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public Nullable<int> CarrierId { get; set; }
    
        public virtual ICollection<GoodsReceivedNoteDetail> GoodsReceivedNoteDetails { get; set; }
    }
}
