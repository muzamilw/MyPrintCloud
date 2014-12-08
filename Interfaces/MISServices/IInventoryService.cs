﻿using MPC.Models.DomainModels;
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

        /// <summary>
        /// Add/Update Stock Item
        /// </summary>
        StockItem SaveInevntory(StockItem request);

        /// <summary>
        ///Find Stock Item By Id 
        /// </summary>
        StockItem GetById(long stockItemId);

        /// <summary>
        /// Get Suppliers For Inventory
        /// </summary>
        SupplierSearchResponseForInventory LoadSuppliers(SupplierRequestModelForInventory request);
    }
}
