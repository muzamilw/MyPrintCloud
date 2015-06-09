using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface IPurchaseService
    {
        /// <summary>
        /// Gets list of Purchases
        /// </summary>
        PurchaseResponseModel GetPurchases(PurchaseRequestModel model);

        /// <summary>
        ///  Gets list of Purchase Orders
        /// </summary>
        PurchaseResponseModel GetPurchaseOrders(PurchaseOrderSearchRequestModel model);

        /// <summary>
        /// base Data for Purchase
        /// </summary>
        PurchaseBaseResponse GetBaseData();

        /// <summary>
        /// Save Purchase
        /// </summary>
        Purchase SavePurchase(Purchase purchase);

        /// <summary>
        /// Get Purchase By Id
        /// </summary>
        Purchase GetPurchaseById(int purchaseId);

        /// <summary>
        /// Delete Purchase Order
        /// </summary>
        void DeletePurchaseOrder(int purchaseId);


    }
}
