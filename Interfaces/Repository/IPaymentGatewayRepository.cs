using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface IPaymentGatewayRepository : IBaseRepository<PaymentGateway, long>
    {
        Models.ResponseModels.PaymentGatewayResponse GetPaymentGateways(Models.RequestModels.PaymentGatewayRequestModel request);
    }
}
