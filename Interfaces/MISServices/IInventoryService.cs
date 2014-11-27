using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Inventory Service Interface
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// Load Inventory Base data
        /// </summary>
        InventoryBaseResponse GetBaseData();

        /// <summary>
        /// Load Stock Items, based on search filters
        /// </summary>
        InventorySearchResponse LoadStockItems(InventorySearchRequestModel request);
    }
}
