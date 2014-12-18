using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
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