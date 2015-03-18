using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Customer Service 
    /// </summary>
    public class CustomerService : ICustomerService
    {
        #region Private
        private readonly ICompanyRepository companyRepository;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }
        #endregion
        #region Public
        /// <summary>
        /// Get Customers for List view 
        /// </summary>
        public CompanyResponse GetCustomers(CompanyRequestModel requestModel)
        {
            return companyRepository.SearchCompaniesForCustomer(requestModel);
        }
        #endregion
    }
}
