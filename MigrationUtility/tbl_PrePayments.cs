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
    
    public partial class tbl_PrePayments
    {
        public long PrePaymentID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<long> OrderID { get; set; }
        public Nullable<double> Amount { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public Nullable<long> PayPalResponseID { get; set; }
        public int PaymentMethodID { get; set; }
        public string ReferenceCode { get; set; }
        public string PaymentDescription { get; set; }
    
        public virtual tbl_estimates tbl_estimates { get; set; }
        public virtual tbl_PaymentMethods tbl_PaymentMethods { get; set; }
        public virtual tbl_PayPalResponses tbl_PayPalResponses { get; set; }
    }
}
