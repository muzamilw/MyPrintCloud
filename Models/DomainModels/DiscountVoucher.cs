using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class DiscountVoucher
    {
        public long DiscountVoucherId { get; set; }
        public string VoucherCode { get; set; }
        public Nullable<System.DateTime> ValidFromDate { get; set; }
        public Nullable<System.DateTime> ValidUptoDate { get; set; }
        public Nullable<long> OrderId { get; set; }
        public double DiscountRate { get; set; }
        public Nullable<System.DateTime> ConsumedDate { get; set; }
        public bool IsEnabled { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CompanyId { get; set; }
    }
}
