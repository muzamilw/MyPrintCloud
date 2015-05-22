using System;
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cost Centre Type Domain Model
    /// </summary>
    /// 
    [Serializable()]
    public class CostCentreType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public short IsSystem { get; set; }
        public short IsExternal { get; set; }
        public long? OrganisationId { get; set; }

        public virtual ICollection<CostCentre> CostCentres { get; set; }
    }
}
