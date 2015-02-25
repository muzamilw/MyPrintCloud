using System;
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Paypal Response Domain Model
    /// </summary>
    public class PayPalResponse
    {
        public long PayPalResponseId { get; set; }
        public long? RequestId { get; set; }
        public long? OrderId { get; set; }
        public string TransactionId { get; set; }
        public double? PaymentPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public bool? IsSuccess { get; set; }
        public string ReasonFault { get; set; }
        public DateTime? ResponseDate { get; set; }
        public string TransactionType { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentType { get; set; }
        public string PayerEmail { get; set; }
        public string PayerStatus { get; set; }
        public string ReceiverEmail { get; set; }

        public virtual ICollection<PrePayment> PrePayments { get; set; } 
    }
}
