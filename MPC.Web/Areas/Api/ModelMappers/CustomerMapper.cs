using MPC.MIS.Areas.Api.Models;
using Company = MPC.Models.DomainModels.Company;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Customer's Mapper
    /// </summary>
    public static class CustomerMapper
    {
        #region Private

        /// <summary>
        /// Get Customer's Status
        /// </summary>
        private static string GetCustomerStatus(short statusCode)
        {
            if (statusCode == 0)
                return "Inactive";
            if (statusCode == 1)
                return "Active";
            if (statusCode == 2)
                return "Banned";
            if (statusCode == 3)
                return "Pending";
            return string.Empty;
        }
        #endregion
        #region Public
        /// <summary>
        /// Create Customer List View
        /// </summary>
        public static CustomerListViewModel CreateFromCustomer(this Company source)
        {
            return new CustomerListViewModel
            {
                CustomerName = source.Name,
                DateCreted = source.CreationDate,
                Email = source.MarketingBriefRecipient,
                Status = GetCustomerStatus(source.Status),
                CompnayId = source.CompanyId
            };
        }
        #endregion
    }
}