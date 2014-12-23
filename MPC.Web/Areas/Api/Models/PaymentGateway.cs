namespace MPC.MIS.Areas.Api.Models
{
    public class PaymentGateway
    {
        public int PaymentGatewayId { get; set; }
        public string BusinessEmail { get; set; }
        public string IdentityToken { get; set; }
        public bool IsActive { get; set; }
        public long? CompanyId { get; set; }
        public int? PaymentMethodId { get; set; }
        public string SecureHash { get; set; }
        public string PaymentMethodName { get; set; }
    }
}