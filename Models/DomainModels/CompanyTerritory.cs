using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    public class CompanyTerritory
    {
        public long TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public long? CompanyId { get; set; }
        public string TerritoryCode { get; set; }
        public bool? isDefault { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
