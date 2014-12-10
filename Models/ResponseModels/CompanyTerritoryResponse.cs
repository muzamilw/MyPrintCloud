using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class CompanyTerritoryResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Companies
        /// </summary>
        public IEnumerable<CompanyTerritory> CompanyTerritories { get; set; }
    }
}
