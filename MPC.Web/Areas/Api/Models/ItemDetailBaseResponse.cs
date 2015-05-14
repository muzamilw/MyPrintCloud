﻿using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
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
        public IEnumerable<PaperSizeDropDown> PaperSizes { get; set; }

        /// <summary>
        /// Ink Plate Sides
        /// </summary>
        public IEnumerable<InkPlateSide> InkPlateSides { get; set; }

        /// <summary>
        /// Stock items Lists of Stock category type 2(Inks)
        /// </summary>
        public IEnumerable<StockItemForDropDown> Inks { get; set; }

        /// <summary>
        /// Ink Coverage Group
        /// </summary>
        public IEnumerable<InkCoverageGroup> InkCoverageGroup { get; set; }

        /// <summary>
        /// Currency Symbol
        /// </summary>

        public string CurrencySymbol { get; set; }

        /// <summary>
        /// System Users
        /// </summary>
        public IEnumerable<SystemUserDropDown> SystemUsers { get; set; }

    }
}