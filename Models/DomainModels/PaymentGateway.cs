using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class PaymentGateway
    {
        public int PaymentGatewayId { get; set; }
        public string BusinessEmail { get; set; }
        public string IdentityToken { get; set; }
        public bool isActive { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> PaymentMethodId { get; set; }
        public string SecureHash { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
