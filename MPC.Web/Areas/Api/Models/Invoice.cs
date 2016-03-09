
using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Invoice Api model
    /// </summary>
    public class Invoice
    {
        public long InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public string CompanyName { get; set; }
        public string InvoiceName { get; set; }
        public bool? IsArchive { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public double? InvoiceTotal { get; set; }
        public string ContactName { get; set; }
        public short? InvoiceStatus { get; set; }
        public Guid? InvoicePostedBy { get; set; }
        public string Status { get; set; }
        public int? FlagId { get; set; }
        public long? CompanyId { get; set; }
        public long? ContactId { get; set; }
        public int? InvoiceType { get; set; }
        public string OrderNo { get; set; }
        public string AccountNumber { get; set; }
        public string Terms { get; set; }
        public DateTime? InvoicePostingDate { get; set; }
        public int? AddressId { get; set; }
        public double? TaxValue { get; set; }
        public double? GrandTotal { get; set; }
        public string UserNotes { get; set; }
        public long? EstimateId { get; set; }
        public bool? IsProformaInvoice { get; set; }
        public Guid? ReportSignedBy { get; set; }
        public string HeadNotes { get; set; }
        public string FootNotes { get; set; }
        public long? StoreId { get; set; }
        public short IsCustomer { get; set; }
        
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public ICollection<OrderItem> Items { get; set; }

    }
}