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
        /// <summary>
        /// get cost center list according to stock option id
        /// </summary>
        /// <param name="StockOptionID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        List<string> GetProductItemAddOnCostCentres(long StockOptionID, long CompanyID);

        /// <summary>
        /// get id's of cost center except webstore cost cnetre 216 of first section of cloned item 
        /// </summary>
        /// <param name="StockOptionID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        List<SectionCostcentre> GetClonedItemAddOnCostCentres(long ItemId);
    }
}
