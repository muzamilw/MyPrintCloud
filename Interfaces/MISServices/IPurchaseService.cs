
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
    }
}
