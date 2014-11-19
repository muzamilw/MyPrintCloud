using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// My Organization Save Domain Response(Add/Update)
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
        public IEnumerable<ChartOfAccount> ChartOfAccounts { get; set; }

        /// <summary>
        /// Tax Rate List
        /// </summary>
        public IEnumerable<TaxRate> TaxRates { get; set; }

        /// <summary>
        /// Markup List
        /// </summary>
        public IEnumerable<Markup> Markups { get; set; }
    }
}
