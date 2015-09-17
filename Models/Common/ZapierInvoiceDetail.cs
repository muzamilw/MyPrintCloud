using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class ZapierInvoiceDetail
    {
        public string CustomerName { get; set; }
        public string CustomerUrl { get; set; }
        public double TaxRate { get; set; }
        public string CurrencyCode { get; set; }
        public string VatNumber { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName  { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressCountry { get; set; }
        
        public DateTime InvoiceDueDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public long InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public List<ZapierInvoiceItem> InvoiceItems { get; set; }



    }
}
