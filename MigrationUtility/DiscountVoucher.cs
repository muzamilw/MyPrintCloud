//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility
{
    using System;
    using System.Collections.Generic;
    
    public partial class DiscountVoucher
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
