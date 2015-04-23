﻿using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Order Base Response
    /// </summary>
    public class OrderBaseResponse
    {
        /// <summary>
        /// Section Flags
        /// </summary>
        public IEnumerable<SectionFlag> SectionFlags { get; set; }

        /// <summary>
        /// System Users
        /// </summary>
        public IEnumerable<SystemUser> SystemUsers { get; set; }

        /// <summary>
        /// Pipeline Sources
        /// </summary>
        public IEnumerable<PipeLineSource> PipeLineSources { get; set; }

        /// <summary>
        /// List of Markups
        /// </summary>
        public IEnumerable<Markup> Markups { get; set; }
        
        /// <summary>
        /// Payment Methoda
        /// </summary>
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }

        /// <summary>
        /// Organisation
        /// </summary>
        public Organisation Organisation { get; set; }

        /// <summary>
        /// Stock Categories
        /// </summary>
        public IEnumerable<StockCategory> StockCategories { get; set; }

        /// <summary>
        /// Chart Of Accounts
        /// </summary>
        public IEnumerable<ChartOfAccount> ChartOfAccounts { get; set; }

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
        /// Cost Centers
        /// </summary>
        public IEnumerable<CostCentre> CostCenters { get; set; }
    }
}
