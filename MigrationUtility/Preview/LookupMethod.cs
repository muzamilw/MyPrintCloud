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
    
    public partial class LookupMethod
    {
        public LookupMethod()
        {
            this.MachineClickChargeLookups = new HashSet<MachineClickChargeLookup>();
            this.MachineClickChargeZones = new HashSet<MachineClickChargeZone>();
            this.MachineGuillotineCalcs = new HashSet<MachineGuillotineCalc>();
            this.MachineMeterPerHourLookups = new HashSet<MachineMeterPerHourLookup>();
            this.MachinePerHourLookups = new HashSet<MachinePerHourLookup>();
            this.MachineSpeedWeightLookups = new HashSet<MachineSpeedWeightLookup>();
        }
    
        public long MethodId { get; set; }
        public string Name { get; set; }
        public Nullable<long> Type { get; set; }
        public Nullable<int> LockedBy { get; set; }
        public int OrganisationID { get; set; }
        public Nullable<int> FlagId { get; set; }
        public int SystemSiteId { get; set; }
    
        public virtual ICollection<MachineClickChargeLookup> MachineClickChargeLookups { get; set; }
        public virtual ICollection<MachineClickChargeZone> MachineClickChargeZones { get; set; }
        public virtual ICollection<MachineGuillotineCalc> MachineGuillotineCalcs { get; set; }
        public virtual ICollection<MachineMeterPerHourLookup> MachineMeterPerHourLookups { get; set; }
        public virtual ICollection<MachinePerHourLookup> MachinePerHourLookups { get; set; }
        public virtual ICollection<MachineSpeedWeightLookup> MachineSpeedWeightLookups { get; set; }
    }
}
