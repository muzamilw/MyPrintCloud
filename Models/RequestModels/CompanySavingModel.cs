using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.RequestModels
{
    public class CompanySavingModel
    {
        public Company Company { get; set; }
        public IEnumerable<CompanyTerritory> NewAddedCompanyTerritories { get; set; }
        public IEnumerable<CompanyTerritory> EdittedCompanyTerritories { get; set; }
        public IEnumerable<CompanyTerritory> DeletedCompanyTerritories { get; set; }
    }
}
