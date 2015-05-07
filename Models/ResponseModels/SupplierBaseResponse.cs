using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Supplier Base Response
    /// </summary>
    public class SupplierBaseResponse
    {
        /// <summary>
        /// Company Types
        /// </summary>
        public IEnumerable<CompanyType> CompanyTypes { get; set; }

        /// <summary>
        /// Flags
        /// </summary>
        public IEnumerable<SectionFlag> Flags { get; set; }

        /// <summary>
        ///Price Flags
        /// </summary>
        public IEnumerable<SectionFlag> PriceFlags { get; set; }

        /// <summary>
        /// Markups
        /// </summary>
        public IEnumerable<Markup> Markups { get; set; }

        /// <summary>
        /// Nominal Codes
        /// </summary>
        public IEnumerable<ChartOfAccount> NominalCodes { get; set; }

        /// <summary>
        /// Deafult Managers
        /// </summary>
        public IEnumerable<SystemUser> SystemUsers { get; set; }

        /// <summary>
        /// Regisration Questions
        /// </summary>
        public IEnumerable<RegistrationQuestion> RegistrationQuestions { get; set; }

        /// <summary>
        /// Currency Symbol
        /// </summary>
        public string CurrencySymbol { get; set; }
    }
}
