﻿using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

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

    }
}