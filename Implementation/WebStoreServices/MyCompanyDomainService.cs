using MPC.Interfaces.WebStoreServices;
using MPC.Interfaces.Repository;

namespace MPC.Implementation.WebStoreServices
{
    public class MyCompanyDomainService: IMyCompanyDomainService
    {
        private readonly ICompanyDomainRepository _companyDomainRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyDomainRepository"></param>
        public MyCompanyDomainService(ICompanyDomainRepository companyDomainRepository)
        {
            this._companyDomainRepository = companyDomainRepository;
        }
        
    }
}
