using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.Common;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IPrePaymentService
    {
        void CreatePrePayment(PaymentMethods payMethod, long orderID, int? customerID, long payPalResponseID, string transactionID, double amountReceived, StoreMode Mode, string responsecodeNab = "");

        void CreatePrePaymentPayWay(PaymentMethods payMethod, long orderID, int? customerID, long payPalResponseID, string transactionID, double amountReceived);

        void CreatePrePaymentStripe(PaymentMethods payMethod, long orderID, int? customerID, string ReceiptNumber, string transactionID, double amountReceived);
    }
}
