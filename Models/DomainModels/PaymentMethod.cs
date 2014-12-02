using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string MethodName { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public virtual ICollection<PaymentGateway> PaymentGateways { get; set; }
        public virtual ICollection<PrePayment> PrePayments { get; set; }
    }
}
