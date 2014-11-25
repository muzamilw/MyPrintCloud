using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;

namespace MPC.Interfaces.MISServices
{
    public interface IStockCategoryService
    {

        /// <summary>
        /// Get All Stock Categories
        /// </summary>
        /// <returns></returns>
        IEnumerable<StockCategory> GetAll(StockCategoryRequestModel request);

        /// <summary>
        /// Add New Stock Category
        /// </summary>
        /// <param name="stockCategory"></param>
        /// <returns></returns>
        StockCategory Add(StockCategory stockCategory);

        /// <summary>
        /// Update Stock Category
        /// </summary>
        /// <param name="stockCategory"></param>
        /// <returns></returns>
        StockCategory Update(StockCategory stockCategory);

        /// <summary>
        /// Delete Stock Category
        /// </summary>
        /// <param name="stockCategoryId"></param>
        /// <returns></returns>
        bool Delete(int stockCategoryId);

        /// <summary>
        /// Get Stock Category By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StockCategory GetStockCategoryById(int id);
    }
}
