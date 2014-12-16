﻿using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface ICompanyService
    {
        CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request);
        CompanyTerritoryResponse SearchCompanyTerritories(CompanyTerritoryRequestModel request);
        CompanyContactResponse SearchCompanyContacts(CompanyContactRequestModel request);
        AddressResponse SearchAddresses(AddressRequestModel request);
        CompanyResponse GetCompanyById(int companyId);
        CompanyBaseResponse GetBaseData(long clubId);
        /// <summary>
        /// Save File Path In Db against organization ID
        /// </summary>
        void SaveFile(string filePath, long companyId);

        Company SaveCompany(CompanySavingModel company);
        long GetOrganisationId();

    }
}