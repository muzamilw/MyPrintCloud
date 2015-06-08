using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;


namespace MPC.Implementation.MISServices
{
    public class PurchaseService : IPurchaseService
    {
        #region Private

        private readonly IPurchaseRepository purchaseRepository;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            this.purchaseRepository = purchaseRepository;
        }

        #endregion
        public PurchaseResponseModel GetPurchases(PurchaseRequestModel requestModel)
        {
            return purchaseRepository.GetPurchases(requestModel);
        }

        public PurchaseResponseModel GetPurchaseOrders(PurchaseOrderSearchRequestModel request)
        {
            return purchaseRepository.GetPurchaseOrders(request);
        }
    }
}
