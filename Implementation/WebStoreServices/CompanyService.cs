
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;


namespace MPC.Implementation.WebStoreServices
{
    public class CompanyService : ICompanyService   
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
// ReSharper disable InconsistentNaming
        private readonly ICompanyRepository companyRepository;
// ReSharper restore InconsistentNaming
     

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public CompanyService(ICompanyRepository companyRepository )
        {
            this.companyRepository = companyRepository;
         
        }

        #endregion


        #region Public
        /// <summary>
        /// Resolves the Company/Store by the domain provided
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public Company GetCompanyByDomain(string domain)
        {
            return companyRepository.GetCompanyByDomain(domain);
        }

        #endregion
    }
}
