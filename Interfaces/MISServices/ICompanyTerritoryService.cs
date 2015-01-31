using MPC.Models.DomainModels;

namespace MPC.Interfaces.MISServices
{
    public interface ICompanyTerritoryService
    {
        /// <summary>
        /// Save company territory 
        /// </summary>
        /// <param name="companyTerritory"></param>
        /// <returns></returns>
        CompanyTerritory Save(CompanyTerritory companyTerritory);
        /// <summary>
        /// Delete company territory 
        /// </summary>
        /// <param name="companyTerritory"></param>
        /// <returns></returns>
        bool Delete(CompanyTerritory companyTerritory);
        /// <summary>
        /// Get Company Territory By Id
        /// </summary>
        /// <param name="companyTerritoryId"></param>
        /// <returns></returns>
        CompanyTerritory Get(long companyTerritoryId);
    }
}
