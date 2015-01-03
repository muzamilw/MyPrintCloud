using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Item Price Matrix Repository
    /// </summary>
    public interface IItemPriceMatrixRepository : IBaseRepository<ItemPriceMatrix, long>
    {
        /// <summary>
        /// Get For Item by Section Flag
        /// </summary>
        IEnumerable<ItemPriceMatrix> GetForItemBySectionFlag(long sectionFlagId, long itemId);
    }
}
