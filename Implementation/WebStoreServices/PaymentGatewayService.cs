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
        public PaymentGateway GetPaymentGatewayRecord(long CompanyId)
        {

            return _paymentRepository.GetPaymentGatewayRecord(CompanyId);


        }
    }
}
