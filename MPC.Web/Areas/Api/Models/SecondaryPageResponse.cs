using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Secondary Page API Response
    /// </summary>
    public class SecondaryPageResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Cms Pages For List View
        /// </summary>
        public IEnumerable<CmsPageForListView> CmsPages { get; set; }
    }
}