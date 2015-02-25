using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface IPaypalPaymentRequestRepository
    {

        bool ChangeStatus(long orderId, PaymentRequestStatus status);
        PaypalPaymentRequest GetPaypalPaymentRequestByOrderId(long orderId);
    }
}
