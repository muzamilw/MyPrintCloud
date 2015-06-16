using System.Collections;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Inventory Base Response
    /// </summary>
    public class InventoryBaseResponse
    {
        /// <summary>
        /// Stock Categories
        /// </summary>
        public IEnumerable<StockCategory> StockCategories { get; set; }

        /// <summary>
        /// Stock Sub Category List
        /// </summary>
        public IEnumerable<StockSubCategory> StockSubCategories { get; set; }

        /// <summary>
        /// Paper Sizes List
        /// </summary>
        public IEnumerable<PaperSize> PaperSizes { get; set; }

        /// <summary>
        /// Section Flag
        /// </summary>
        public IEnumerable<SectionFlag> SectionFlags { get; set; }

        /// <summary>
        /// Weight Units
        /// </summary>
        public IEnumerable<WeightUnit> WeightUnits { get; set; }

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

        /// <summary>
        /// Organisation
        /// </summary>
        public Organisation Organisation { get; set; }

        /// <summary>
        /// Organisation culture 
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Weight Unit
        /// </summary>
        public string WeightUnit { get; set; }

        public bool IsImperical { get; set; }

    }
}
