using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class PrePayment
    {
        public long PrePaymentId { get; set; }
        public int? CustomerId { get; set; }
        public long? OrderId { get; set; }
        public double? Amount { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public long? PayPalResponseId { get; set; }
        public int PaymentMethodId { get; set; }
        public string ReferenceCode { get; set; }
        public string PaymentDescription { get; set; }

        public virtual Estimate Estimate { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual PayPalResponse PayPalResponse { get; set; }
    }
}
