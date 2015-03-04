//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_deliverynotes
    {
        public tbl_deliverynotes()
        {
            this.tbl_deliverynote_details = new HashSet<tbl_deliverynote_details>();
        }
    
        public int DeliveryNoteID { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<int> ContactCompanyID { get; set; }
        public string OrderReff { get; set; }
        public string footnote { get; set; }
        public string Comments { get; set; }
        public Nullable<int> LockedBy { get; set; }
        public Nullable<short> IsStatus { get; set; }
        public Nullable<long> ContactId { get; set; }
        public string ContactCompany { get; set; }
        public string CustomerOrderReff { get; set; }
        public Nullable<int> AddressID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDateTime { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public string SupplierTelNo { get; set; }
        public string CsNo { get; set; }
        public string SupplierURL { get; set; }
        public int RaisedBy { get; set; }
        public Nullable<int> FlagID { get; set; }
        public Nullable<int> EstimateID { get; set; }
        public Nullable<int> JobID { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string UserNotes { get; set; }
        public Nullable<System.DateTime> NotesUpdateDateTime { get; set; }
        public Nullable<int> NotesUpdatedByUserID { get; set; }
        public Nullable<int> SystemSiteID { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<bool> IsPrinted { get; set; }
    
        public virtual ICollection<tbl_deliverynote_details> tbl_deliverynote_details { get; set; }
    }
}