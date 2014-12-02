using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class DeliveryNote
    {
        public int DeliveryNoteId { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<int> ContactCompanyId { get; set; }
        public string OrderReff { get; set; }
        public string footnote { get; set; }
        public string Comments { get; set; }
        public Nullable<int> LockedBy { get; set; }
        public Nullable<short> IsStatus { get; set; }
        public Nullable<long> ContactId { get; set; }
        public string ContactCompany { get; set; }
        public string CustomerOrderReff { get; set; }
        public Nullable<int> AddressId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDateTime { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public string SupplierTelNo { get; set; }
        public string CsNo { get; set; }
        public string SupplierURL { get; set; }
        public int RaisedBy { get; set; }
        public Nullable<int> FlagId { get; set; }
        public Nullable<int> EstimateId { get; set; }
        public Nullable<int> JobId { get; set; }
        public Nullable<int> InvoiceId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public string UserNotes { get; set; }
        public Nullable<System.DateTime> NotesUpdateDateTime { get; set; }
        public Nullable<int> NotesUpdatedByUserId { get; set; }
        public Nullable<int> SystemSiteId { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<bool> IsPrinted { get; set; }

        public virtual ICollection<DeliveryNoteDetail> DeliveryNoteDetails { get; set; }
    }
}
