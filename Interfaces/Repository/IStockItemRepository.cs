using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Stock Item Repository 
    /// </summary>
    public interface IStockItemRepository : IBaseRepository<StockItem, long>
    {
        /// <summary>
        /// Get Stock Items
        /// </summary>
        InventorySearchResponse GetStockItems(InventorySearchRequestModel request);

        /// <summary>
        /// Get Stock Items For Product
        /// </summary>
        InventorySearchResponse GetStockItemsForProduct(StockItemRequestModel request);

        List<StockItem> GetStockItemsByOrganisationID(long OrganisationID);

    }
}
