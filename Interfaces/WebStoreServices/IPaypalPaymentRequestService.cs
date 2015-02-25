using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.Common;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IPaypalPaymentRequestService
    {


        bool ChangeStatus(long orderId, PaymentRequestStatus status);
        
    }
}
