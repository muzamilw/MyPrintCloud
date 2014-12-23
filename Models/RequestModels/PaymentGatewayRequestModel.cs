namespace MPC.Models.RequestModels
{
    public class PaymentGatewayRequestModel : GetPagedListRequest
    {
        public string SearchFilter { get; set; }
        public long CompanyId { get; set; }
        public long PaymentGatewayId { get; set; }
    }
}
