
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Customer Service Interface 
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Get Customers for List view 
        /// </summary>
        CompanyResponse GetCustomers(CompanyRequestModel requestModel);
    }
}
