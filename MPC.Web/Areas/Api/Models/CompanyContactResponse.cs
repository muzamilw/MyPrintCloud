using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class CompanyContactResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Companies
        /// </summary>
        public IEnumerable<CompanyContact> CompanyContacts { get; set; }
    }
}