using System;
using System.Linq.Expressions;
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
        /// Will return Stock with name A4 of type paper
        /// </summary>
        StockItem GetA4PaperStock();

        /// <summary>
        /// Load Property
        /// </summary>
        void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false);

        /// <summary>
        /// Get Stock Items
        /// </summary>
        InventorySearchResponse GetStockItems(InventorySearchRequestModel request);

        /// <summary>
        /// Get Stock Items In orders 
        /// </summary>
        InventorySearchResponse GetStockItemsInOrders(InventorySearchRequestModel request);

        /// <summary>
        /// Get Stock Items For Product
        /// </summary>
        InventorySearchResponse GetStockItemsForProduct(StockItemRequestModel request);

        List<StockItem> GetStockItemsByOrganisationID(long OrganisationID);

        List<StockItem> GetStockItemOfCategoryInk();

        string GetStockName(long StockID);
    }
}
