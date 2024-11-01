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
    
    public partial class PaymentGateway
    {
        public int PaymentGatewayId { get; set; }
        public string BusinessEmail { get; set; }
        public string IdentityToken { get; set; }
        public bool isActive { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<int> PaymentMethodId { get; set; }
        public string SecureHash { get; set; }
        public string CancelPurchaseUrl { get; set; }
        public string ReturnUrl { get; set; }
        public string NotifyUrl { get; set; }
        public Nullable<bool> SendToReturnURL { get; set; }
        public Nullable<bool> UseSandbox { get; set; }
        public string LiveApiUrl { get; set; }
        public string TestApiUrl { get; set; }
    
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual Company Company { get; set; }
    }
}
