using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IPaymentGatewayService
    {
        PaymentGateway GetPaymentGatewayRecord();
        PaymentGateway GetPaymentGatewayRecord(long CompanyId, long PaymentGateWayid);
        List<PaymentGateway> GetAllActivePaymentGateways(long CompanyId);
        PaymentGateway GetPaymentByMethodId(long CompanyId, long PaymenthodMethodId);
    }
}
