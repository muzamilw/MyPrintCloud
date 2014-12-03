using System.Collections.Generic;
using MPC.MIS.Models;

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
        /// Stock Cost And Price
        /// </summary>
        public StockCostAndPrice StockCostAndPrice { get; set; }

        /// <summary>
        /// Length Units
        /// </summary>
        public IEnumerable<LengthUnit> LengthUnits { get; set; }

        /// <summary>
        /// Paper Basis Areas
        /// </summary>
        public IEnumerable<PaperBasisArea> PaperBasisAreas { get; set; }

        /// <summary>
        /// Registration Questions
        /// </summary>
        public IEnumerable<RegistrationQuestion> RegistrationQuestions { get; set; }
    }
}