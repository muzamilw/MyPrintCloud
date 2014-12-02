using System.Collections.Generic;
using MPC.MIS.Models;

namespace MPC.MIS.ResponseModels
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