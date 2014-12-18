using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class CompanyBaseResponse
    {
        /// <summary>
        /// System Users List
        /// </summary>
        public IEnumerable<SystemUser> SystemUsers { get; set; }
        public IEnumerable<CompanyTerritory> CompanyTerritories { get; set; }
        public IEnumerable<PageCategory> PageCategories { get; set; }
        // public IEnumerable<Department> Departments { get; set; }
        // public IEnumerable<AccountManager> AccountManagers { get; set; }
    }
}
