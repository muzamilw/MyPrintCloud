using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class ZapierInvoiceItemSupplier
    {
        public string SupplierName { get; set; }
        public long SupplierId { get; set; }
        public string SupplierVatRegNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Terms  { get; set; }


    }
}
