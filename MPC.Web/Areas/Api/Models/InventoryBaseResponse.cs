using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Inventory Base Response Web Model
    /// </summary>
    public class InventoryBaseResponse
    {

        /// <summary>
        /// Stock Categories
        /// </summary>
        public IEnumerable<StockCategoryDropDown> StockCategories { get; set; }

        /// <summary>
        /// Stock Sub Categories
        /// </summary>
        public IEnumerable<StockSubCategoryDropDown> StockSubCategories { get; set; }

        /// <summary>
        /// Paper Size DropDown List
        /// </summary>
        public IEnumerable<PaperSizeDropDown> PaperSizes { get; set; }

        /// <summary>
        /// Section Flag Drop Down
        /// </summary>
        public IEnumerable<SectionFlagDropDown> SectionFlags { get; set; }

        /// <summary>
        /// Weight Unit DropDown
        /// </summary>
        public IEnumerable<WeightUnitDropDown> WeightUnits { get; set; }

        /// <summary>
        /// Length Units
        /// </summary>
        public IEnumerable<LengthUnitDropDown> LengthUnits { get; set; }

        /// <summary>
        /// Paper Basis Areasc DropDown
        /// </summary>
        public IEnumerable<PaperBasisAreaDropDown> PaperBasisAreas { get; set; }

        /// <summary>
        /// Registration Questions
        /// </summary>
        public IEnumerable<RegistrationQuestionDropDown> RegistrationQuestions { get; set; }


        /// <summary>
        /// Currency Symbol
        /// </summary>
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Organisation Region
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Weight Unit
        /// </summary>
        public string WeightUnit { get; set; }

        /// <summary>
        /// IsImperical
        /// </summary>
        public bool IsImperical { get; set; }
    }
}