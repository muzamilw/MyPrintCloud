using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MPC.Repository.Repositories
{
    public class PaymentGatewayRepository : BaseRepository<PaymentGateway>, IPaymentGatewayRepository
    {
        public PaymentGatewayRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PaymentGateway> DbSet
        {
            get
            {
                return db.PaymentGateways;
            }
        }
        public Models.ResponseModels.PaymentGatewayResponse GetPaymentGateways(Models.RequestModels.PaymentGatewayRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchFilter);
            bool isPaymentGatewayIdInSearch = request.PaymentGatewayId != 0;
            Expression<Func<PaymentGateway, bool>> query =
                s =>
                    (isSearchFilterSpecified && (s.PaymentMethod.MethodName.Contains(request.SearchFilter)) ||
                     (s.BusinessEmail.Contains(request.SearchFilter)) ||
                     !isSearchFilterSpecified) && (s.CompanyId == request.CompanyId);

            int rowCount = DbSet.Count(query);
            // ReSharper disable once ConditionalTernaryEqualBranch
            IEnumerable<PaymentGateway> paymentGateways = request.IsAsc
                ? DbSet.Where(query)
                    .OrderByDescending(x => x.PaymentGatewayId)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(x => x.PaymentGatewayId)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new PaymentGatewayResponse
            {
                RowCount = rowCount,
                PaymentGateways = paymentGateways
            };
        }


        /// <summary>
        /// returns the active payment gateway
        /// </summary>
        /// <returns></returns>
        public PaymentGateway GetPaymentGatewayRecord(long CompanyId, long PaymenthodGateWayId)
        {
            
                return (from res in db.PaymentGateways
                        where res.isActive == true && res.CompanyId == CompanyId && res.PaymentGatewayId == PaymenthodGateWayId
                        select res).FirstOrDefault();
          

        }

        public  PaymentGateway GetPaymentGatewayRecord()
        {
           
                return (from res in db.PaymentGateways
                        where res.isActive == true && res.CompanyId == null
                        select res).FirstOrDefault();
          


        }
        /// <summary>
        /// add payment 
        /// </summary>
        /// <param name="prePayment"></param>
        /// <returns></returns>
        public long AddPrePayment(PrePayment prePayment)
        {
            try
            {
               
                    db.PrePayments.Add(prePayment);
                    db.SaveChanges();
                    return prePayment.PrePaymentId;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// returns all active payment gateway
        /// </summary>
        /// <returns></returns>
        public List<PaymentGateway> GetAllActivePaymentGateways(long CompanyId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            return (from res in db.PaymentGateways.Include("PaymentMethod")
                    where res.isActive == true && res.CompanyId == CompanyId
                    select res).ToList();


        }
        /// <summary>
        /// returns the active payment gateway by method
        /// </summary>
        /// <returns></returns>
        public PaymentGateway GetPaymentByMethodId(long CompanyId, long PaymenthodMethodId)
        {

            return (from res in db.PaymentGateways
                    where res.isActive == true && res.CompanyId == CompanyId && res.PaymentMethodId == PaymenthodMethodId
                    select res).FirstOrDefault();


        }
    }
}
