using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class CrmContactResponse
    {
        /// <summary>
        /// List of Addresses
        /// </summary>
        public IEnumerable<Address> Addresses { get; set; }

        /// <summary>
        /// List of CompanyTerritories
        /// </summary>
        public IEnumerable<CompanyTerritory> CompanyTerritories { get; set; }
    }
}
