using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class CompanyResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Companies
        /// </summary>
        public IEnumerable<Company> Companies { get; set; } 
    }
}