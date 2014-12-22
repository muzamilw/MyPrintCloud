namespace MPC.MIS.Areas.Api.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string MethodName { get; set; }
        public bool? IsActive { get; set; }
    }
}