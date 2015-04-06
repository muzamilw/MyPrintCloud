using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Order Base Response
    /// </summary>
    public class OrderBaseResponse
    {
        /// <summary>
        /// Section Flags
        /// </summary>
        public IEnumerable<SectionFlagDropDown> SectionFlags { get; set; }

        /// <summary>
        /// System Users
        /// </summary>
        public IEnumerable<SystemUserDropDown> SystemUsers { get; set; }

        /// <summary>
        /// Pipeline Sources
        /// </summary>
        public IEnumerable<PipeLineSource> PipeLineSources { get; set; }


        /// <summary>
        /// List of Markups
        /// </summary>
        public IEnumerable<Markup> Markups { get; set; }
        
        /// <summary>
        /// Payment Methods
        /// </summary>
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }

        /// <summary>
        /// Currency Symbol
        /// </summary>
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Stock Categories
        /// </summary>
        public IEnumerable<StockCategoryDropDown> StockCategories { get; set; }

        /// <summary>
        /// Chart Of Accounts
        /// </summary>
        public IEnumerable<ChartOfAccount> ChartOfAccounts { get; set; }

        /// <summary>
        /// Paper Sizes
        /// </summary>
        public IEnumerable<PaperSizeDropDown> PaperSizes { get; set; }

        /// <summary>
        /// Ink Plate Sides
        /// </summary>
        public IEnumerable<InkPlateSide> InkPlateSides { get; set; }
    }
}
