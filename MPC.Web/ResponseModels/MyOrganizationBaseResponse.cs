using System.Collections.Generic;
using MPC.MIS.Models;

namespace MPC.MIS.ResponseModels
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