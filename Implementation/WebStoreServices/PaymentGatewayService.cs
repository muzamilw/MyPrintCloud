using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.WebStoreServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.WebStoreServices
{
    public class PaymentGatewayService : IPaymentGatewayService
    {
        private readonly IPaymentGatewayRepository _paymentRepository;
        public PaymentGatewayService(IPaymentGatewayRepository paymentRepository)
        {
            this._paymentRepository = paymentRepository;
        }

        public PaymentGateway GetPaymentGatewayRecord()
        {

            return _paymentRepository.GetPaymentGatewayRecord();


        }
        public PaymentGateway GetPaymentGatewayRecord(long CompanyId, long PaymentGateWayid)
        {

            return _paymentRepository.GetPaymentGatewayRecord(CompanyId, PaymentGateWayid);


        }

        public List<PaymentGateway> GetAllActivePaymentGateways(long CompanyId)
        {

            return _paymentRepository.GetAllActivePaymentGateways(CompanyId);


        }

        public PaymentGateway GetPaymentByMethodId(long CompanyId, long PaymenthodMethodId)
        {

            return _paymentRepository.GetPaymentByMethodId(CompanyId, PaymenthodMethodId);


        }
    }
}
