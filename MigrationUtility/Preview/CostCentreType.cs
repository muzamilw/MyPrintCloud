//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility.Preview
{
    using System;
    using System.Collections.Generic;
    
    public partial class CostCentreType
    {
        public CostCentreType()
        {
            this.CostCentres = new HashSet<CostCentre>();
        }
    
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public short IsSystem { get; set; }
        public short IsExternal { get; set; }
        public Nullable<long> OrganisationId { get; set; }
    
        public virtual ICollection<CostCentre> CostCentres { get; set; }
    }
}
