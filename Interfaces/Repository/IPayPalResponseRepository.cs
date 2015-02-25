using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IPayPalResponseRepository
    {


        long WritePayPalResponse(int requestID, int orderID, string txn_id, string txn_type,
           double payment_price, string receiverEmail, string paymentStatus, string paymentType,
           string payerEmail, string payerStatus,
           string first_name, string last_name, string street, string city, string state, string zip,
           string country, bool is_success, string reason_fault, CultureInfo culInfo);
    }
}
