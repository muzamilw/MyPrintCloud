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
    
    public partial class tbl_audit
    {
        public long AuditNo { get; set; }
        public string InvoiceType { get; set; }
        public Nullable<int> InvoiceNo { get; set; }
        public string Account { get; set; }
        public string Detail { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string Reference { get; set; }
        public double Net { get; set; }
        public double Tax { get; set; }
        public Nullable<int> Paid { get; set; }
        public double AmountPaid { get; set; }
        public string Reconciled { get; set; }
        public Nullable<System.DateTime> BankReconciledDate { get; set; }
        public string TransactionUser { get; set; }
        public string VAT { get; set; }
        public int Nominal { get; set; }
        public Nullable<int> SystemSiteID { get; set; }
    }
}