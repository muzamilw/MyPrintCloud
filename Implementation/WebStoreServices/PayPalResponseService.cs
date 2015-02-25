using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.WebStoreServices;
using System.Globalization;
using MPC.Interfaces.Repository;

namespace MPC.Implementation.WebStoreServices
{
    public class PayPalResponseService : IPayPalResponseService
    {
        private readonly IPayPalResponseRepository _PayPalResponseRepository;
        public PayPalResponseService(IPayPalResponseRepository _PayPalResponseRepository)
        {
            this._PayPalResponseRepository = _PayPalResponseRepository;

        }


        public long WritePayPalResponse(int requestID, int orderID, string txn_id, string txn_type,
          double payment_price, string receiverEmail, string paymentStatus, string paymentType,
          string payerEmail, string payerStatus,
          string first_name, string last_name, string street, string city, string state, string zip,
          string country, bool is_success, string reason_fault, CultureInfo culInfo)
        {
            return _PayPalResponseRepository.WritePayPalResponse(requestID, orderID,txn_id, txn_type, payment_price, receiverEmail, paymentStatus,  paymentType, payerEmail, payerStatus, first_name, last_name, street, city, state, zip,  country, is_success, reason_fault, culInfo);

        }
    }
}
