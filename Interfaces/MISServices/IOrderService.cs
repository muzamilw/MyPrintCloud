using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Order Service
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Get All Orders
        /// </summary>
        GetOrdersResponse GetAll(GetOrdersRequest request);

        /// <summary>
        /// Get By Id
        /// </summary>
        Estimate GetById(long orderId);
    }
}
