using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

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

        int CreateContact(CompanyContact contact);
        //List<CmsPage> GetSecondaryPages(long companyId);

        //List<PageCategory> GetSecondaryPageCategories();
    }
}
