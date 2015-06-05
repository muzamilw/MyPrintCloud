using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface IPurchaseRepository : IBaseRepository<Purchase, long>
    {
        PurchaseResponseModel GetPurchases(PurchaseRequestModel request);

        /// <summary>
        /// Get Purchase Orders
        /// </summary>
        PurchaseResponseModel GetPurchaseOrders(PurchaseOrderSearchRequestModel request);
    }
}
