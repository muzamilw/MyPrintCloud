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
        public DateTime? DeliveryDate { get; set; }
        public long? CompanyId { get; set; }
        public string OrderReff { get; set; }
        public string footnote { get; set; }
        public string Comments { get; set; }
        public int? LockedBy { get; set; }
        public short? IsStatus { get; set; }
        public long? ContactId { get; set; }
        public string ContactCompany { get; set; }
        public string CustomerOrderReff { get; set; }
        public int? AddressId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierTelNo { get; set; }
        public string CsNo { get; set; }
        public string SupplierURL { get; set; }
        public Guid? RaisedBy { get; set; }
        public int? FlagId { get; set; }
        public int? EstimateId { get; set; }
        public int? JobId { get; set; }
        public int? InvoiceId { get; set; }
        public int? OrderId { get; set; }
        public string UserNotes { get; set; }
        public DateTime? NotesUpdateDateTime { get; set; }
        public int? NotesUpdatedByUserId { get; set; }
        public int? SystemSiteId { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsPrinted { get; set; }
        
        public long? OrganisationId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<DeliveryNoteDetail> DeliveryNoteDetails { get; set; }
        public virtual SectionFlag SectionFlag { get; set; }
    }
}
