using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    public interface IPaymentGatewayRepository : IBaseRepository<PaymentGateway, long>
    {
        Models.ResponseModels.PaymentGatewayResponse GetPaymentGateways(Models.RequestModels.PaymentGatewayRequestModel request);
        PaymentGateway GetPaymentGatewayRecord(long CompanyId, long PaymenthodGateWayId);
        long AddPrePayment(PrePayment prePayment);
        PaymentGateway GetPaymentGatewayRecord();
        List<PaymentGateway> GetAllActivePaymentGateways(long CompanyId);
        PaymentGateway GetPaymentByMethodId(long CompanyId, long PaymenthodMethodId);
    }
}
