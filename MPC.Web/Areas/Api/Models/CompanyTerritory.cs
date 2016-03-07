using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class CompanyTerritory
    {
        public long TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public long? CompanyId { get; set; }
        public string TerritoryCode { get; set; }
        public bool? isDefault { get; set; }
        public bool? IsUseTerritoryColor { get; set; }
        public bool? IsUserTerritoryFont { get; set; }
        
        public List<ScopeVariable> ScopeVariables { get; set; }
        public List<TemplateColorStyle> TerritorySpotColors { get; set; }
        public List<TemplateFont> TerritoryFonts { get; set; }

        //public ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}