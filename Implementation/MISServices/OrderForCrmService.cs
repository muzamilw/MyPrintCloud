using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Service  for orders in crm company
    /// </summary>
    public class OrderForCrmService : IOrderForCrmService
    {
        #region Private

        private readonly IEstimateRepository estimateRepository;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public OrderForCrmService(IEstimateRepository estimateRepository)
        {
            this.estimateRepository = estimateRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// Gets list of Orders for company
        /// </summary>
        public OrdersForCrmResponse GetOrdersForCrm(GetOrdersRequest model)
        {
           return estimateRepository.GetOrdersForCrm(model);
        }

        #endregion
    }
}
