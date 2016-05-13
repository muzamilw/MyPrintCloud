using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    public class Invoice
    {
        public long InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public int? InvoiceType { get; set; }
        public string InvoiceName { get; set; }
        public long? CompanyId { get; set; }
        public long? ContactId { get; set; }
        public string OrderNo { get; set; }
        public short? InvoiceStatus { get; set; }
        public double? InvoiceTotal { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public string AccountNumber { get; set; }
        public string Terms { get; set; }
        public DateTime? InvoicePostingDate { get; set; }
        public Guid? InvoicePostedBy { get; set; }
        public Guid? LockedBy { get; set; }
        public int? AddressId { get; set; }
        public bool? IsArchive { get; set; }
        public double? TaxValue { get; set; }
        public double? GrandTotal { get; set; }
        public int? FlagID { get; set; }
        public string UserNotes { get; set; }
        public DateTime? NotesUpdateDateTime { get; set; }
        public int? NotesUpdatedByUserID { get; set; }
        public int? SystemSiteId { get; set; }
        public long? EstimateId { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsProformaInvoice { get; set; }
        public bool? IsPrinted { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public Guid? ReportSignedBy { get; set; }
        public DateTime? ReportLastPrintedDate { get; set; }
        public string HeadNotes { get; set; }
        public string FootNotes { get; set; }
        public string XeroAccessCode { get; set; }
        public long? OrganisationId { get; set; }
        [NotMapped]
        public string XeroPostUrl { get; set; }

        public string RefOrderCode { get; set; }
        
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual Company Company { get; set; }
        public virtual CompanyContact CompanyContact { get; set; }
        public virtual Status Status { get; set; }
    }
}
