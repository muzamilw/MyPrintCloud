
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Service Interface for orders in crm company
    /// </summary>
    public interface IOrderForCrmService
    {
        /// <summary>
        /// Gets list of Orders for company
        /// </summary>
        OrdersForCrmResponse GetOrdersForCrm(GetOrdersRequest model);
    }
}
