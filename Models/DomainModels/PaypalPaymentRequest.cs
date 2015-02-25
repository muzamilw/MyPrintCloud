using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Paypal Payment Request Domain Model
    /// </summary>
    public class PaypalPaymentRequest
    {
        public int Request_ID { get; set; }
        public int Order_ID { get; set; }
        public string ProductID { get; set; }
        public decimal Price { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
    }
}
