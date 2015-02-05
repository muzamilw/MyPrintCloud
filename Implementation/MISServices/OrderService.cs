using System;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Order Service
    /// </summary>
    public class OrderService : IOrderService
    {
        #region Private
        
        private readonly IEstimateRepository estimateRepository;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderService(IEstimateRepository estimateRepository)
        {
            if (estimateRepository == null)
            {
                throw new ArgumentNullException("estimateRepository");
            }

            this.estimateRepository = estimateRepository;
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Get All Orders
        /// </summary>
        public GetOrdersResponse GetAll(GetOrdersRequest request)
        {
            return estimateRepository.GetOrders(request);
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        public Estimate GetById(long orderId)
        {
            return estimateRepository.Find(orderId);
        }

        /// <summary>
        /// Delete Order
        /// </summary>
        public void DeleteOrder(long orderId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save Order
        /// </summary>
        public Estimate SaveOrder(Estimate estimate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
