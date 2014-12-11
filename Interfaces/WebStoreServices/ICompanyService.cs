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

        MyCompanyDomainBaseReponse GetBaseData(long companyId);
        long GetCompanyIdByDomain(string domain);
        List<ProductCategory> GetCompanyParentCategoriesById(long companyId);
        CompanyResponse GetAllCompaniesOfOrganisation(CompanyRequestModel request);
        CompanyContact GetContactUser(string email, string password);

        CompanyContact GetContactByFirstName(string FName);

        CompanyContact GetContactByEmail(string Email);

        Int64 CreateContact(CompanyContact Contact, string Name, int OrganizationID, int CustomerType, string TwitterScreanName);


        CompanyContact CreateCorporateContact(int CustomerId, CompanyContact regContact, string TwitterScreenName);
        Company GetCompanyByCompanyID(Int64 companyID);

        CompanyContact GetContactByID(Int64 contactID);

        List<Address> GetAddressesByTerritoryID(Int64 TerritoryID);
        //List<CmsPage> GetSecondaryPages(long companyId);

        //List<PageCategory> GetSecondaryPageCategories();
    }
}
