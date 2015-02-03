using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Invoice
    {
        public long InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public Nullable<int> InvoiceType { get; set; }
        public string InvoiceName { get; set; }
        public Nullable<int> ContactCompanyID { get; set; }
        public Nullable<int> ContactId { get; set; }
        public string ContactCompany { get; set; }
        public string OrderNo { get; set; }
        public Nullable<int> InvoiceStatus { get; set; }
        public Nullable<double> InvoiceTotal { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public string AccountNumber { get; set; }
        public string Terms { get; set; }
        public Nullable<System.DateTime> InvoicePostingDate { get; set; }
        public Guid? InvoicePostedBy { get; set; }
        public Guid? LockedBy { get; set; }
        public Nullable<int> AddressId { get; set; }
        public Nullable<bool> IsArchive { get; set; }
        public Nullable<double> TaxValue { get; set; }
        public Nullable<double> GrandTotal { get; set; }
        public Nullable<int> FlagID { get; set; }
        public string UserNotes { get; set; }
        public Nullable<System.DateTime> NotesUpdateDateTime { get; set; }
        public Nullable<int> NotesUpdatedByUserID { get; set; }
        public Nullable<int> SystemSiteId { get; set; }
        public Nullable<int> EstimateId { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<bool> IsProformaInvoice { get; set; }
        public Nullable<bool> IsPrinted { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Guid? ReportSignedBy { get; set; }
        public Nullable<System.DateTime> ReportLastPrintedDate { get; set; }
        public string HeadNotes { get; set; }
        public string FootNotes { get; set; }
        public string XeroAccessCode { get; set; }
        public Nullable<long> OrganisationId { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
