using System.Linq;
using DomainResponse = MPC.Models.ResponseModels;
using ApiResponse = MPC.MIS.ResponseModels;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Models;

namespace MPC.MIS.ModelMappers
{
    /// <summary>
    /// 
    /// </summary>
    public static class StockItemMapper
    {
        #region Base Reposne Mapper
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiResponse.InventoryBaseResponse CreateFrom(this DomainResponse.InventoryBaseResponse source)
        {
            return new ApiResponse.InventoryBaseResponse
            {
                //StockCategories = source.StockCategories.Select(coa => coa.CreateFrom()).ToList(),
                //StockSubCategories = source.StockSubCategories.Select(markup => markup.CreateFrom()).ToList(),
            };
        }

        #endregion

        #region Base Reposne Mapper

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiResponse.InventorySearchResponse CreateFrom(this DomainResponse.InventorySearchResponse source)
        {
            return new ApiResponse.InventorySearchResponse
            {
                StockItems = source.StockItems.Select(stockItem => stockItem.CreateFrom()).ToList(),
                TotalCount = source.TotalCount
            };
        }
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.StockItemForListView CreateFrom(this DomainModels.StockItem source)
        {
            return new ApiModels.StockItemForListView
            {
            };
        }
        #endregion

    }
}