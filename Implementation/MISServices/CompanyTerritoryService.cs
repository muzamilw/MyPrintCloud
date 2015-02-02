using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class CompanyTerritoryService : ICompanyTerritoryService
    {
        #region Private
        private readonly ICompanyTerritoryRepository companyTerritoryRepository;

        //#region Private Methods
        private CompanyTerritory Create(CompanyTerritory companyTerritory)
        {
            //check to maintain one companyTerritory as default in db
            CheckCompanyTerritoryDefualt(companyTerritory);
            companyTerritoryRepository.Add(companyTerritory);
            companyTerritoryRepository.SaveChanges();
            return companyTerritory;
        }

        private CompanyTerritory Update(CompanyTerritory companyTerritory)
        {
            //check to maintain one companyTerritory as default in db
            CheckCompanyTerritoryDefualt(companyTerritory);
            companyTerritoryRepository.Update(companyTerritory);
            companyTerritoryRepository.SaveChanges();
            return companyTerritory;
        }

        private void CheckCompanyTerritoryDefualt(CompanyTerritory companyTerritory)
        {
            if (companyTerritory.isDefault != null && companyTerritory.isDefault == true)
            {
                var companyTerritoriesToUpdate = companyTerritoryRepository.GetAll().Where(x => x.isDefault == true && x.CompanyId == companyTerritory.CompanyId);
                foreach (var territory in companyTerritoriesToUpdate)
                {
                    territory.isDefault = false;
                    companyTerritoryRepository.Update(territory);
                }
            }
        }
        #endregion
        #region Constructor

        public CompanyTerritoryService(ICompanyTerritoryRepository companyTerritoryRepository)
        {
            this.companyTerritoryRepository = companyTerritoryRepository;
        }
        #endregion
        public CompanyTerritory Save(CompanyTerritory companyTerritory)
        {
            if (companyTerritory.TerritoryId == 0)
            {
                return Create(companyTerritory);
            }
            return Update(companyTerritory);
        }

        public bool Delete(CompanyTerritory companyTerritory)
        {
            var dbCompanyTerritory = companyTerritoryRepository.GetTerritoryById(companyTerritory.TerritoryId);
            if (dbCompanyTerritory != null)
            {
                companyTerritoryRepository.Delete(dbCompanyTerritory);
                companyTerritoryRepository.SaveChanges();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Get Company Territory By Id
        /// </summary>
        /// <param name="companyTerritoryId"></param>
        /// <returns></returns>
        public CompanyTerritory Get(long companyTerritoryId)
        {
            if (companyTerritoryId > 0)
            {
                var result = companyTerritoryRepository.GetTerritoryById(companyTerritoryId);
                if (result != null)
                {
                    return result;
                }
                return null;
            }
            return null;
        }
    }
}
