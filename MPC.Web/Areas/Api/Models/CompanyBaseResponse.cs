using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class CompanyBaseResponse
    {
        /// <summary>
        /// System Users List
        /// </summary>
        public IEnumerable<SystemUserDropDown> SystemUsers { get; set; }
        public IEnumerable<CompanyTerritory> CompanyTerritories{ get; set; }
        public IEnumerable<PageCategoryDropDown> PageCategories{ get; set; }

        // public IEnumerable<Department> Departments { get; set; }
        // public IEnumerable<AccountManager> AccountManagers { get; set; }
    }
}