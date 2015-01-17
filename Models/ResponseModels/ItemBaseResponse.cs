using System.Collections.Generic;
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
    }
}
