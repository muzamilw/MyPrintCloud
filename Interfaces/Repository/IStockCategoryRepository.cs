using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface IStockCategoryRepository : IBaseRepository<StockCategory, long>
    {
        StockCategoryResponse SearchStockCategory(StockCategoryRequestModel request);
    }
}
