using System.Collections.Generic;
using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class CompanyTerritoryService : ICompanyTerritoryService
    {
        #region Private
        private readonly ICompanyTerritoryRepository companyTerritoryRepository;
        private readonly IScopeVariableRepository scopeVariableRepository;

        //#region Private Methods
        private CompanyTerritory Create(CompanyTerritory companyTerritory)
        {
            //check to maintain one companyTerritory as default in db
            CheckCompanyTerritoryDefualt(companyTerritory);
            companyTerritoryRepository.Add(companyTerritory);
            companyTerritoryRepository.SaveChanges();

            if (companyTerritory.ScopeVariables != null)
            {
                foreach (ScopeVariable scopeVariable in companyTerritory.ScopeVariables)
                {
                    scopeVariable.Id = companyTerritory.TerritoryId;
                    scopeVariableRepository.Add(scopeVariable);
                }
                scopeVariableRepository.SaveChanges();
            }
            return companyTerritory;
        }

        private CompanyTerritory Update(CompanyTerritory companyTerritory)
        {
            //check to maintain one companyTerritory as default in db
            CheckCompanyTerritoryDefualt(companyTerritory);
            companyTerritoryRepository.Update(companyTerritory);
            if (companyTerritory.ScopeVariables != null)
            {
                UpdateScopVariables(companyTerritory);
            }
            companyTerritoryRepository.SaveChanges();

            return companyTerritory;
        }
        /// <summary>
        /// Update Scope Variables
        /// </summary>
        private void UpdateScopVariables(CompanyTerritory companyTerritory)
        {
            IEnumerable<ScopeVariable> scopeVariables = scopeVariableRepository.GetContactVariableByContactId(companyTerritory.TerritoryId, (int)FieldVariableScopeType.Territory);
            foreach (ScopeVariable scopeVariable in companyTerritory.ScopeVariables)
            {
                ScopeVariable scopeVariableDbItem = scopeVariables.FirstOrDefault(
                    scv => scv.ScopeVariableId == scopeVariable.ScopeVariableId);
                if (scopeVariableDbItem != null)
                {
                    scopeVariableDbItem.Value = scopeVariable.Value;
                }
            }
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

        public CompanyTerritoryService(ICompanyTerritoryRepository companyTerritoryRepository, IScopeVariableRepository scopeVariableRepository)
        {
            this.companyTerritoryRepository = companyTerritoryRepository;
            this.scopeVariableRepository = scopeVariableRepository;
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

        public bool Delete(long companyTerritoryId)
        {
            var dbCompanyTerritory = companyTerritoryRepository.GetTerritoryById(companyTerritoryId);
            if (dbCompanyTerritory != null && (dbCompanyTerritory.Addresses == null || !dbCompanyTerritory.Addresses.Any()) && (dbCompanyTerritory.CompanyContacts == null || !dbCompanyTerritory.CompanyContacts.Any()))
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
