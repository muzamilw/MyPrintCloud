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
    
    public partial class tbl_PaymentMethods
    {
        public tbl_PaymentMethods()
        {
            this.tbl_PaymentGateways = new HashSet<tbl_PaymentGateways>();
            this.tbl_PrePayments = new HashSet<tbl_PrePayments>();
        }
    
        public int PaymentMethodID { get; set; }
        public string MethodName { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual ICollection<tbl_PaymentGateways> tbl_PaymentGateways { get; set; }
        public virtual ICollection<tbl_PrePayments> tbl_PrePayments { get; set; }
    }
}