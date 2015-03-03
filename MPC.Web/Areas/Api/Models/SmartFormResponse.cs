using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Smart Form API Response
    /// </summary>
    public class SmartFormResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// List of Smart Form
        /// </summary>
        public IEnumerable<SmartFormForListView> SmartForms { get; set; }
    }
}