using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Pre Payment Webapi Model
    /// </summary>
    public class PrePayment
    {
        public long PrePaymentId { get; set; }
        public int? CustomerId { get; set; }
        public long? OrderId { get; set; }
        public double? Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public long? PayPalResponseId { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public string ReferenceCode { get; set; }
        public string PaymentDescription { get; set; }
    }
}