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
    
    public partial class AccountDefault
    {
        public int DefaultId { get; set; }
        public Nullable<System.DateTime> StartFinancialYear { get; set; }
        public Nullable<System.DateTime> EndFinancialYear { get; set; }
        public int Bank { get; set; }
        public int Sales { get; set; }
        public int Till { get; set; }
        public int Purchase { get; set; }
        public int Debitor { get; set; }
        public int Creditor { get; set; }
        public int Prepayment { get; set; }
        public int VATonSale { get; set; }
        public int VATonPurchase { get; set; }
        public int DiscountAllowed { get; set; }
        public int DiscountTaken { get; set; }
        public int OpeningBalance { get; set; }
        public int SystemSiteId { get; set; }
    }
}
