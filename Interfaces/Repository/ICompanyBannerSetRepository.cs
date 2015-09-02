using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Company Banner Set Repository
    /// </summary>
    public interface ICompanyBannerSetRepository : IBaseRepository<CompanyBannerSet, long>
    {
        /// <summary>
        /// Returns Active Banner Set for Specified Company
        /// </summary>
        CompanyBannerSet GetActiveBannerSetForCompany(long companyId);

        List<string> GetCompanyBannersByCompanyId(long companyId);
    }
}
