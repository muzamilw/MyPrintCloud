using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public long InvoiceId { get; set; }
        public int DetailType { get; set; }
        public Nullable<long> ItemId { get; set; }
        public string InvoiceTitle { get; set; }
        public int NominalCode { get; set; }
        public double ItemCharge { get; set; }
        public double Quantity { get; set; }
        public double ItemTaxValue { get; set; }
        public int FlagId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string Description { get; set; }
        public Nullable<int> ItemType { get; set; }
        public Nullable<int> TaxId { get; set; }
        public double? TaxValue { get; set; }
        public double? ItemGrossTotal { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Item Item { get; set; }
    }
}
