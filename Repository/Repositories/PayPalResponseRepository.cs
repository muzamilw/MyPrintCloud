using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.Repository;
using MPC.Repository.BaseRepository;
using MPC.Models.DomainModels;
using System.Globalization;
using Microsoft.Practices.Unity;
using System.Data.Entity;


namespace MPC.Repository.Repositories
{
    class PayPalResponseRepository : BaseRepository<PayPalResponse>, IPayPalResponseRepository
    {
        public PayPalResponseRepository(IUnityContainer container)
            : base(container)
        {

        }


        protected override IDbSet<PayPalResponse> DbSet
        {
            get {
                return db.PayPalResponses;
            }
        }


        public long WritePayPalResponse(int requestID, int orderID, string txn_id, string txn_type,
           double payment_price, string receiverEmail, string paymentStatus, string paymentType,
           string payerEmail, string payerStatus,
           string first_name, string last_name, string street, string city, string state, string zip,
           string country, bool is_success, string reason_fault, CultureInfo culInfo)
        {

            long payPalResponsePkey = 0;
            PayPalResponse payPalResponse = new PayPalResponse()
            {

                PayPalResponseId = 0,
                RequestId = requestID,
                OrderId = orderID,
                TransactionId = txn_id,
                TransactionType = txn_type,
                PaymentPrice = (double?)payment_price,
                PaymentStatus = paymentStatus,
                PaymentType = paymentType,
                PayerEmail = payerEmail,
                PayerStatus = payerStatus,
                ReceiverEmail = receiverEmail,
                FirstName = first_name,
                LastName = last_name,
                Street = street,
                City = city,
                State = state,
                Zip = zip,
                Country = country,
                IsSuccess = is_success,
                ReasonFault = reason_fault,
                ResponseDate = DateTime.Now

            };
            
            db.PayPalResponses.Add(payPalResponse);
                if (db.SaveChanges() > 0)
                    payPalResponsePkey = payPalResponse.PayPalResponseId;

           

            return payPalResponsePkey;
        }
    }
}
