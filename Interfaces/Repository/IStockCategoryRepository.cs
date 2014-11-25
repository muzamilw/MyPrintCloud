using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;

namespace MPC.Interfaces.Repository
{
    public interface IStockCategoryRepository : IBaseRepository<StockCategory, long>
    {
        IEnumerable<StockCategory> SearchStockCategory(StockCategoryRequestModel request, out int rowCount);
    }
}
