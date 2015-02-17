using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Linq;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Customer Controller 
    /// </summary>
    public class CustomerController : ApiController
    {
        #region Private

        private readonly ICustomerService customerService;
        private readonly ICompanyService companyService;

        #endregion
        #region Constuctor
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerController(ICustomerService customerService, ICompanyService companyService)
        {
            this.customerService = customerService;
            this.companyService = companyService;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Customers List 
        /// </summary>
        public CustomerResponse Get([FromUri] CompanyRequestModel request)
        {
            var customers = customerService.GetCustomers(request);
            return new CustomerResponse
            {
                Customers = customers.Companies.Select(company => company.CreateFromCustomer()).ToList(),
                RowCount = customers.RowCount
            };
        }
        public CompanyResponse Get([FromUri]int companyId)
        {
            return companyService.GetCompanyById(companyId).CreateFromForCrm();
        }
        #endregion
    }
}                                           