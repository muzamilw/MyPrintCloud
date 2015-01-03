using MPC.Models.Common;
using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Item Add on Cost Centre Repository 
    /// </summary>
    public interface IItemAddOnCostCentreRepository : IBaseRepository<ItemAddonCostCentre, long>
    {
        List<AddOnCostsCenter> AddOnsPerStockOption(long itemId, long companyId);
    }
}
