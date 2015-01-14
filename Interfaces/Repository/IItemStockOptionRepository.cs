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
        /// <summary>
        /// get list of stock options by item and company id
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        List<ItemStockOption> GetAllStockListByItemID(long ItemID, long companyID);
    }
}
