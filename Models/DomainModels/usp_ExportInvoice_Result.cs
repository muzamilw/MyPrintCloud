using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class usp_ExportInvoice_Result
    {
        public string AccountNumber { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string InvoiceCode { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<System.DateTime> InvoicePostingDate { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public Nullable<double> Qty1NetTotal { get; set; }
        public Nullable<double> Qty1 { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public string Name { get; set; }
        public string AddressName { get; set; }
        public string BAddress1 { get; set; }
        public string StatusName { get; set; }

        public string OrderNo { get; set; }

        public string InvoiceDescription { get; set; }

    }
}
