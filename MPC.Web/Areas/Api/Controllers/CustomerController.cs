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

        #endregion
        #region Constuctor
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
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
        #endregion
    }
}