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
    
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            this.PaymentGateways = new HashSet<PaymentGateway>();
            this.PrePayments = new HashSet<PrePayment>();
        }
    
        public int PaymentMethodId { get; set; }
        public string MethodName { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual ICollection<PaymentGateway> PaymentGateways { get; set; }
        public virtual ICollection<PrePayment> PrePayments { get; set; }
    }
}
