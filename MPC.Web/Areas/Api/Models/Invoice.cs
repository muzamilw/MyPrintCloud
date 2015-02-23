
using System;

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


    }
}