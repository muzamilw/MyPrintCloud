using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Repository.BaseRepository;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using System.Data.Entity;
using Microsoft.Practices.Unity;
namespace MPC.Repository.Repositories
{
    public class PaypalPaymentRequestRepository : BaseRepository<PaypalPaymentRequest>, IPaypalPaymentRequestRepository
    {
        public PaypalPaymentRequestRepository(IUnityContainer container)
            : base(container)
        {

        }



        protected override IDbSet<PaypalPaymentRequest> DbSet
        {
            get
            {
                return db.PaypalPaymentRequests;
            }
           
        }


        public PaypalPaymentRequest GetPaypalPaymentRequestByOrderId(long orderId)
        {
            return db.PaypalPaymentRequests.Where(g => g.Order_ID == orderId).FirstOrDefault();
        }

        public bool ChangeStatus(long orderId, PaymentRequestStatus status)
        {
            PaypalPaymentRequest request = GetPaypalPaymentRequestByOrderId(Convert.ToInt32(orderId));
            if (request != null)
            {
                request.Status = (int)status;
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
