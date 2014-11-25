
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System.Collections.Generic;

namespace MPC.Implementation.WebStoreServices
{
    public class CompanyBannerSetService : ICompanyBannerSetService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        public readonly ICompanyBannerSetRepository companySetRepository;
     

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CompanyBannerSetService(ICompanyBannerSetRepository companySetepository)
        {
            this.companySetRepository = companySetepository;
         
        }

        #endregion


        #region Public
        /// <summary>
        /// Resolves the Company/Store by the domain provided
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public List<CompanyBannerSet> GetCompanyBannersById(long companyId, long organisationId)
        {
            return companySetRepository.GetCompanyBannersById(companyId, organisationId);
        }
      
        #endregion
    }
}
