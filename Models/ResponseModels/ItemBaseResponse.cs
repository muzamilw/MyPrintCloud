using System.Collections.Generic;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Item Base Response
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
        public IEnumerable<SectionFlag> SectionFlags { get; set; }

        /// <summary>
        /// Suppliers
        /// </summary>
        public IEnumerable<Company> Suppliers { get; set; }

        /// <summary>
        /// Parent Product Categories
        /// </summary>
        public IEnumerable<ProductCategory> ProductCategories { get; set; }

        /// <summary>
        /// Paper Sizes
        /// </summary>
        public IEnumerable<PaperSize> PaperSizes { get; set; }

        /// <summary>
        /// Length Unit 
        /// </summary>
        public string LengthUnit { get; set; }

        /// <summary>
        /// Currency Unit 
        /// </summary>
        public string CurrencyUnit { get; set; }
        
        /// <summary>
        /// Weight Unit
        /// </summary>
        public string WeightUnit { get; set; }
        
        /// <summary>
        /// Machines
        /// </summary>
        public IEnumerable<Machine> Machines { get; set; }

        /// <summary>
        /// Inks
        /// </summary>
        public IEnumerable<StockItem> Inks { get; set; }

        /// <summary>
        /// A4 Paper Stock Item
        /// </summary>
        public StockItem A4PaperStockItem { get; set; }
    }
}
