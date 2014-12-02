using System.Collections.Generic;
using MPC.Models.DomainModels;
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

        List<CmsPage> GetSecondaryPages(long companyId);

        List<PageCategory> GetSecondaryPageCategories();
    }
}
