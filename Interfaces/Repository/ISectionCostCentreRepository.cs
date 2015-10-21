using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Section Cost Centre Repository 
    /// </summary>
    public interface ISectionCostCentreRepository : IBaseRepository<SectionCostcentre, int>
    {
         /// <summary>
        /// get item section cost centres
        /// </summary>
        /// <param name="StockOptionID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        List<SectionCostcentre> GetAllSectionCostCentres(long ItemSetionId);

        void RemoveCostCentreOfFirstSection(long ItemSetionId);
    }
}
