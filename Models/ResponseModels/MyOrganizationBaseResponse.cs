using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// My Organization Base Domain Response
    /// </summary>
    public class MyOrganizationBaseResponse
    {
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

        /// <summary>
        ///Countries 
        /// </summary>
        public IEnumerable<Country> Countries { get; set; }

        /// <summary>
        /// States
        /// </summary>
        public IEnumerable<State> States { get; set; }

        /// <summary>
        /// Currencies
        /// </summary>
        public IEnumerable<Currency> Currencies { get; set; }

        /// <summary>
        /// Length Units
        /// </summary>
        public IEnumerable<LengthUnit> LengthUnits { get; set; }

        /// <summary>
        /// Weight Units
        /// </summary>
        public IEnumerable<WeightUnit> WeightUnits { get; set; }
    }
}
