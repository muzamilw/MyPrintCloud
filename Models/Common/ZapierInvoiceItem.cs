using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class ZapierInvoiceItem
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double NetTotal { get; set; }
        public double TaxValue { get; set; }
        public double GrossTotal { get; set; }
        public ZapierInvoiceItemSupplier Supplier { get; set; }
    }
}
