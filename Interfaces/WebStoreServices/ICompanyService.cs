using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;

namespace MPC.Interfaces.WebStoreServices
{
    /// <summary>
    /// My Organization Service Interface
    /// </summary>
    public interface ICompanyService
    {
        MyCompanyDomainBaseReponse GetStoreFromCache(long companyId);
        long GetStoreIdFromDomain(string domain);
        List<ProductCategory> GetCompanyParentCategoriesById(long companyId);
        CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request);
        CompanyContact GetUserByEmailAndPassword(string email, string password);

        CompanyContact GetContactByFirstName(string FName);

        CompanyContact GetContactByEmail(string Email);

        long CreateContact(CompanyContact Contact, string Name, int OrganizationID, int CustomerType, string TwitterScreanName);


        CompanyContact CreateCorporateContact(int CustomerId, CompanyContact regContact, string TwitterScreenName);
        Company GetCompanyByCompanyID(Int64 companyID);

        CompanyContact GetContactByID(long contactID);

        List<Address> GetAddressesByTerritoryID(long TerritoryID);

        string GetUiCulture(long organisationId);

        CompanyContact GetContactByEmailAndMode(string Email, int Type, int customerID);

        string GeneratePasswordHash(string plainText);

        void UpdateUserPassword(int userId, string pass);

        SystemUser GetSystemUserById(long SystemUserId);

        List<ProductCategory> GetAllParentCorporateCatalogByTerritory(int customerId, int ContactId);
        List<ProductCategory> GetAllParentCorporateCatalog(int customerId);

        List<ProductCategory> GetStoreParentCategories(long companyId);
        List<ProductCategory> GetAllCategories(long companyId);
        CompanyContact GetCorporateUserByEmailAndPassword(string email, string password, long companyId);
    }
}
