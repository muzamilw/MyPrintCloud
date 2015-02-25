using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using MPC.Models.DomainModels;
using MPC.Models.Common;
using MPC.Interfaces.Repository;
using System.Data.Entity;
using MPC.Interfaces.WebStoreServices;
using Microsoft.Practices.Unity;

namespace MPC.Repository.Repositories
{
    public class PrePaymentRepository : BaseRepository<PrePayment>, IPrePaymentRepository
    {

        private readonly IOrderService _OrderService;
        private readonly IPaypalPaymentRequestService _PaypalPaymentRequestService;


        public PrePaymentRepository(IUnityContainer container, IOrderService OrderService, IPaypalPaymentRequestService PaypalPaymentRequestService)
            : base(container)
        {
            this._OrderService = OrderService;
            this._PaypalPaymentRequestService = PaypalPaymentRequestService;
        }

        protected override IDbSet<PrePayment> DbSet
        {
            get
            {
                return db.PrePayments;
                
            }
        }
       public void CreatePrePayment(PaymentMethods payMethod, long orderID, int? customerID, long payPalResponseID, string transactionID, double amountReceived, StoreMode Mode, string responsecodeNab = "")
        {
            PrePayment tblPrePayment = null;
            Estimate tblOrder = null;
            //MPCEntities db = null;

            try
            {
                tblPrePayment = new PrePayment()
                {
                    PrePaymentId = 0,
                    Amount = amountReceived,
                    CustomerId = customerID,
                    OrderId = orderID,
                    PaymentDate = DateTime.Now,
                    PaymentMethodId = (int)payMethod,
                    PayPalResponseId = payPalResponseID,
                    ReferenceCode = transactionID,
                    PaymentDescription = responsecodeNab
                };

                if (tblPrePayment.PayPalResponseId == 0)
                    tblPrePayment.PayPalResponseId = null;

               // db = new MPCEntities();
                tblOrder = _OrderService.GetOrderByID(orderID);
                // db.tbl_estimates.Where(estm => estm.EstimateID == orderID).FirstOrDefault();
                
                if (tblOrder != null)
                {
                    db.PrePayments.Add(tblPrePayment);

                    _OrderService.UpdateOrderStatusAfterPrePayment(tblOrder, OrderStatus.PendingOrder, Mode);

                    // Change the Payment request status
                    _PaypalPaymentRequestService.ChangeStatus(orderID, PaymentRequestStatus.Successfull);

                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
