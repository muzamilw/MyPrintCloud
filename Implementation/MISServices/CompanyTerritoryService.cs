using System;
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
        private readonly ICompanyContactRepository companyContactRepository;
        private readonly IAddressRepository addressRepository;
        private readonly ITemplateColorStylesRepository cmykColorRepository;
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
            if (companyTerritory.TerritorySpotColors != null)
            {
                foreach (TemplateColorStyle spotColor in companyTerritory.TerritorySpotColors)
                {
                    spotColor.TerritoryId = companyTerritory.TerritoryId;
                    cmykColorRepository.Add(spotColor);
                }
                cmykColorRepository.SaveChanges();
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
            if (companyTerritory.TerritorySpotColors != null)
            {
                UpdateTerritorySpotColors(companyTerritory);
                
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

        private void UpdateTerritorySpotColors(CompanyTerritory territory)
        {
            foreach (TemplateColorStyle spotColor in territory.TerritorySpotColors)
            {
                if (spotColor.PelleteId <= 0)
                {
                    cmykColorRepository.Add(spotColor); 
                }
                else
                {
                    var spotColorDb = cmykColorRepository.Find(spotColor.PelleteId);
                    if (spotColorDb != null)
                    {
                        spotColorDb.ColorC = spotColor.ColorC;
                        spotColorDb.ColorM = spotColor.ColorM;
                        spotColorDb.ColorY = spotColor.ColorY;
                        spotColorDb.ColorK = spotColor.ColorK;
                        spotColorDb.SpotColor = spotColor.SpotColor;
                        spotColorDb.Name = spotColor.SpotColor;
                        spotColorDb.CustomerId = spotColor.CustomerId;
                    }
                }
            }
            cmykColorRepository.SaveChanges();
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

        public CompanyTerritoryService(ICompanyTerritoryRepository companyTerritoryRepository, IScopeVariableRepository scopeVariableRepository,
            ICompanyContactRepository companyContactRepository, IAddressRepository addressRepository, ITemplateColorStylesRepository cmykColorRepository)
        {
            if (companyContactRepository == null)
            {
                throw new ArgumentNullException("companyContactRepository");
            }
            if (addressRepository == null)
            {
                throw new ArgumentNullException("addressRepository");
            }
            if (cmykColorRepository == null)
            {
                throw new ArgumentNullException("cmykColorRepository");
            }
            this.companyTerritoryRepository = companyTerritoryRepository;
            this.scopeVariableRepository = scopeVariableRepository;
            this.companyContactRepository = companyContactRepository;
            this.addressRepository = addressRepository;
            this.cmykColorRepository = cmykColorRepository;
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
            // Only Delete Territory if all of its referencing contacts and addresses have been archived
            // Before Deleting Territory delete them as well
            if (dbCompanyTerritory != null && (dbCompanyTerritory.Addresses == null || dbCompanyTerritory.Addresses.All(address => address.isArchived == true)) && 
                (dbCompanyTerritory.CompanyContacts == null || dbCompanyTerritory.CompanyContacts.All(contact => contact.isArchived == true)))
            {
                // Remove Archived Contacts
                if (dbCompanyTerritory.CompanyContacts != null)
                {
                    List<CompanyContact> companyContacts = dbCompanyTerritory.CompanyContacts.Where(contact => contact.isArchived == true).ToList();
                    companyContacts.ForEach(contact =>
                                            {
                                                dbCompanyTerritory.CompanyContacts.Remove(contact);
                                                companyContactRepository.Delete(contact);
                                            });
                }
                // Remove Archived Addresses 
                if (dbCompanyTerritory.Addresses != null)
                {
                    List<Address> addresses = dbCompanyTerritory.Addresses.Where(contact => contact.isArchived == true).ToList();
                    addresses.ForEach(address =>
                    {
                        dbCompanyTerritory.Addresses.Remove(address);
                        addressRepository.Delete(address);
                    });
                }
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
