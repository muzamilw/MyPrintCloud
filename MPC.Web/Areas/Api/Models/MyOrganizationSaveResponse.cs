using System.Collections.Generic;
using MPC.MIS.Models;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// My Organization Save Web Response
    /// </summary>
    public class MyOrganizationSaveResponse
    {
        /// <summary>
        /// Organization Id
        /// </summary>
        public long OrganizationId { get; set; }

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