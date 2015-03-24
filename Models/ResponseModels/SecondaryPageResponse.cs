using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Secondary Page Domain Response
    /// </summary> 
    public class SecondaryPageResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Cms Pages
        /// </summary>
        public IEnumerable<CmsPage> CmsPages { get; set; }

        /// <summary>
        /// Row Count
        /// </summary>
        public int SystemPagesRowCount { get; set; }

        /// <summary>
        /// List of Cms Pages
        /// </summary>
        public IEnumerable<CmsPage> SystemPages { get; set; }
    }
}
