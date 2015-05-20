using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Order Base Response
    /// </summary>
    public class InvoiceBaseResponse
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
        /// Cost Centers
        /// </summary>
        public IEnumerable<CostCentre> CostCenters { get; set; }

        /// <summary>
        /// Currency Symbol
        /// </summary>
        public string CurrencySymbol { get; set; }


        /// <summary>
        /// LoggedInUserId
        /// </summary>
        public Guid LoggedInUserId { get; set; }
    }
}
