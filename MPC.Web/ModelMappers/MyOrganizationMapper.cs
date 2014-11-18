using System.Linq;
using DomainResponse = MPC.Models.ResponseModels;
using ApiResponse = MPC.Web.ResponseModels;

namespace MPC.Web.ModelMappers
{
    /// <summary>
    /// My Organization Mapper
    /// </summary>
    public static class MyOrganizationMapper
    {
        #region Base Reposne Mapper
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiResponse.MyOrganizationBaseResponse CreateFrom(this DomainResponse.MyOrganizationBaseResponse source)
        {
            return new ApiResponse.MyOrganizationBaseResponse
            {
                ChartOfAccounts = source.ChartOfAccounts.Select(coa => coa.CreateFrom()).ToList(),
                Markups = source.Markups.Select(markup => markup.CreateFrom()).ToList(),
                TaxRates = source.TaxRates.Select(taxRate => taxRate.CreateFrom()).ToList(),
            };
        }

        #endregion
    }
}