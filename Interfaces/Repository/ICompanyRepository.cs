using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    public interface ICompanyRepository : IBaseRepository<Company, long>
    {
        CompanyResponse GetCompanyById(long companyId);

        long GetStoreIdFromDomain(string domain);
        CompanyResponse SearchCompanies(CompanyRequestModel request);

        /// <summary>
        /// Get Suppliers For Inventories
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SupplierSearchResponseForInventory GetSuppliersForInventories(SupplierRequestModelForInventory request);

        /// <summary>
        /// Get Store By Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Company GetStoreById(long companyId);

        int CreateCustomer(string name, bool isEmailSubScription, bool isNewsLetterSubscription, ContactCompanyTypes customerType, string RegWithTwitter, Markup zeroMarkup, CompanyContact regContact = null);
        /// <summary>
        /// Get Company Price Flag id for Price Matrix in webstore
        /// </summary>
        int? GetPriceFlagIdByCompany(long CompanyId);
        
    }
}
