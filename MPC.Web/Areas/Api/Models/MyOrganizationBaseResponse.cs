﻿using System.Collections.Generic;

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

        /// <summary>
        /// States
        /// </summary>
        public List<StateDropDown> States { get; set; }

        /// <summary>
        /// Countries
        /// </summary>
        public List<CountryDropDown> Countries { get; set; }

        /// <summary>
        /// Currencies
        /// </summary>
        public IEnumerable<CurrencyDropDown> Currencies { get; set; }

        /// <summary>
        /// Length Units
        /// </summary>
        public IEnumerable<LengthUnitDropDown> LengthUnits { get; set; }

        /// <summary>
        /// Weight Units
        /// </summary>
        public IEnumerable<WeightUnitDropDown> WeightUnits { get; set; }

        /// <summary>
        /// Global Language
        /// </summary>
        public IEnumerable<GlobalLanguageDropDown> GlobalLanguages { get; set; }
    }
}