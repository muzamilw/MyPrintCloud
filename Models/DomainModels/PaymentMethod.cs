using System.Collections.Generic;


namespace MPC.Models.DomainModels
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string MethodName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<PaymentGateway> PaymentGateways { get; set; }
        public virtual ICollection<PrePayment> PrePayments { get; set; }
    }
}
