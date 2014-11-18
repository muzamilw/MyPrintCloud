using System.Collections.Generic;
using MPC.Web.Models;

namespace MPC.Web.ResponseModels
{
    /// <summary>
    /// My Organization Base Web Response
    /// </summary>
    public class MyOrganizationBaseResponse
    {
        /// <summary>
        /// Chart Of Account List
        /// </summary>
        public List<ChartOfAccount> ChartOfAccounts { get; set; }

        /// <summary>
        /// Tax Rate List
        /// </summary>
        public List<TaxRate> TaxRates { get; set; }

        /// <summary>
        /// Markup List
        /// </summary>
        public List<Markup> Markups { get; set; }
    }
}