using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class ItemDetailBaseResponse
    {
        /// <summary>
        /// List of Markups
        /// </summary>
        public IEnumerable<Markup> Markups { get; set; }

        /// <summary>
        /// Paper Sizes
        /// </summary>
        public IEnumerable<PaperSize> PaperSizes { get; set; }

        /// <summary>
        /// Ink Plate Sides
        /// </summary>
        public IEnumerable<InkPlateSide> InkPlateSides { get; set; }
        /// <summary>
        /// Inks
        /// </summary>
        public IEnumerable<StockItem> Inks { get; set; }
        /// <summary>
        /// Ink Coverage Groups
        /// </summary>
        public IEnumerable<InkCoverageGroup> InkCoverageGroups { get; set; }

        /// <summary>
        /// Currency Symbol
        /// </summary>

        public string CurrencySymbol { get; set; }

        /// <summary>
        /// System Users
        /// </summary>
        public IEnumerable<SystemUser> SystemUsers { get; set; }

        /// <summary>
        /// Length Unit 
        /// </summary>
        public string LengthUnit { get; set; }

        /// <summary>
        /// Weight Unit
        /// </summary>
        public string WeightUnit { get; set; }

        /// <summary>
        /// Logged In User
        /// </summary>
        public Guid LoggedInUser { get; set; }

        /// <summary>
        /// Machines
        /// </summary>
        public IEnumerable<Machine> Machines { get; set; }
    }
}
