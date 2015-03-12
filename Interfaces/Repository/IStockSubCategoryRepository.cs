using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    public interface IStockSubCategoryRepository : IBaseRepository<StockSubCategory, long>
    {
        /// <summary>
        /// Get list of Stock Sub Category for Stock Category Ids
        /// </summary>
        IEnumerable<StockSubCategory> GetStockSubCategoriesByStockCategoryIds(List<long> Ids);
    }
}
