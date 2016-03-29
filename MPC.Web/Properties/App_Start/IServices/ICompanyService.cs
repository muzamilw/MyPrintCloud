using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;
using System.Collections.Generic;
namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Company Service Interface
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Load all companies
        /// </summary>
        IEnumerable<Company> LoadAll();

        /// <summary>
        /// Delete Compny
        /// </summary>
        void DeleteCompany(long companyId);

        /// <summary>
        /// Load Base data of compnies
        /// </summary>
        CompanyBaseDataResponse LoadCompanyBaseData();

        /// <summary>
        /// Search Compny
        /// </summary>
        CompanySearchRequestResponse SearchCompany(CompanySearchRequest request);

        /// <summary>
        /// Add / Update Company
        /// </summary>
        Company AddUpdateCompany(Company companyRequest);
    }
}
