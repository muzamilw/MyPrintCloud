//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility.Preview
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuditTrailDetail
    {
        public long VoucherDetailID { get; set; }
        public long VoucherId { get; set; }
        public int DebitAccount { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string Description { get; set; }
        public int CreditAccount { get; set; }
    }
}
