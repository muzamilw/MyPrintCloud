using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Supplier Api Base Response
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
        public IEnumerable<SectionFlagDropDown> Flags { get; set; }

        /// <summary>
        ///Price Flags
        /// </summary>
        public IEnumerable<SectionFlagDropDown> PriceFlags { get; set; }

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
        public IEnumerable<SystemUserDropDown> SystemUsers { get; set; }

        /// <summary>
        /// Registration Questions
        /// </summary>
        public IEnumerable<RegistrationQuestionDropDown> RegistrationQuestions { get; set; }

        /// <summary>
        /// Currency Symbol
        /// </summary>
        public string CurrencySymbol { get; set; }
    }
}