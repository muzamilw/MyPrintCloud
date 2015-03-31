using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class CrmContactResponse
    {
       
        /// <summary>
        /// List of Addresses
        /// </summary>
        public IEnumerable<Address> Addresses{ get; set; }

        /// <summary>
        /// List of CompanyTerritories
        /// </summary>
        public IEnumerable<CompanyTerritory> CompanyTerritories{ get; set; }

    }
}