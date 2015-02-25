using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Interfaces.Repository;

namespace MPC.Implementation.WebStoreServices
{
    public class PaypalPaymentRequestService : IPaypalPaymentRequestService
    {

        private readonly IPaypalPaymentRequestRepository _PaypalPaymentRequestRepository;

        public  PaypalPaymentRequestService(IPaypalPaymentRequestRepository PaypalPaymentRequestRepository){
            this._PaypalPaymentRequestRepository = PaypalPaymentRequestRepository;
        }

        public bool ChangeStatus(long orderId, PaymentRequestStatus status)
        {
            return _PaypalPaymentRequestRepository.ChangeStatus(orderId,status);
                       
        }


    }
}
