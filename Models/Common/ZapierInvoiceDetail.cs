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
        public string BillingAddressName { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingAddressCity { get; set; }
        public string BillingAddressState { get; set; }
        public string BillingAddressPostalCode { get; set; }
        public string BillingAddressCountry { get; set; }

        public string ShippingAddressName { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }
        public string ShippingAddressCity { get; set; }
        public string ShippingAddressState { get; set; }
        public string ShippingAddressPostalCode { get; set; }
        public string ShippingAddressCountry { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public long InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public List<ZapierInvoiceItem> InvoiceItems { get; set; }



    }
}
