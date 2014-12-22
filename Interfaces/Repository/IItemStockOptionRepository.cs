using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Item Stock Option Repository 
    /// </summary>
    public interface IItemStockOptionRepository : IBaseRepository<ItemStockOption, long>
    {
        List<ItemStockOption> GetStockList(long ItemID, long companyID);
    }
}
