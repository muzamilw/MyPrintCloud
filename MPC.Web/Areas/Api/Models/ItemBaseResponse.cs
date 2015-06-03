using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Base Response Web Api Model
    /// </summary>
    public class ItemBaseResponse
    {
        /// <summary>
        /// Cost Centres
        /// </summary>
        public IEnumerable<CostCentre> CostCentres { get; set; }

        /// <summary>
        /// Countries
        /// </summary>
        public IEnumerable<Country> Countries { get; set; }

        /// <summary>
        /// States
        /// </summary>
        public IEnumerable<State> States { get; set; }

        /// <summary>
        /// Section Flags
        /// </summary>
        public IEnumerable<SectionFlagDropDown> SectionFlags { get; set; }

        /// <summary>
        /// Suppliers
        /// </summary>
        public IEnumerable<SupplierForInventory> Suppliers { get; set; }

        /// <summary>
        /// Parent Product Categories
        /// </summary>
        public IEnumerable<ProductCategoryDropDown> ProductCategories { get; set; }

        /// <summary>
        /// Paper Sizes
        /// </summary>
        public IEnumerable<PaperSizeDropDown> PaperSizes { get; set; }

        /// <summary>
        /// Length Unit 
        /// </summary>
        public string LengthUnit { get; set; }

        /// <summary>
        /// Currency Unit 
        /// </summary>
        public string CurrencyUnit { get; set; }
        public string WeightUnit { get; set; }

        /// <summary>
        /// Inks
        /// </summary>
        public IEnumerable<StockItemForDropDown> Inks { get; set; }

        /// <summary>
        /// Machines
        /// </summary>
        public IEnumerable<Machine> Machines { get; set; }
    }
}