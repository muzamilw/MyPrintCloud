﻿namespace MPC.MIS.Areas.Api.Models
{
    public class CompanyTerritory
    {
        public long TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public long? CompanyId { get; set; }
        public string TerritoryCode { get; set; }
        public bool? isDefault { get; set; }

        //public ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}