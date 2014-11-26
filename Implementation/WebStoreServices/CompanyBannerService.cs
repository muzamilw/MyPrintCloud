
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Implementation.WebStoreServices
{
    public class CompanyBannerService : ICompanyBannerService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        public readonly ICompanyBannerRepository CompanyBannerRepositoryRepository;
     

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CompanyBannerService(ICompanyBannerRepository companyBannerRepository)
        {
            this.CompanyBannerRepositoryRepository = companyBannerRepository;
         
        }

        #endregion


        #region Public
        /// <summary>
        /// Resolves the Company/Store by the domain provided
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public List<CompanyBanner> GetCompanyBannersById(long companyId)
        {
            return CompanyBannerRepositoryRepository.GetCompanyBannersById(companyId);
        }
      
        #endregion
    }
}
