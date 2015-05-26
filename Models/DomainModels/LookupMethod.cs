using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Lookup Method Domain Model
    /// </summary>
    public class LookupMethod
    {
        public long MethodId { get; set; }
        public string Name { get; set; }
        public long? Type { get; set; }
        public int? LockedBy { get; set; }
        public int OrganisationId { get; set; }
        public int? FlagId { get; set; }
        public int SystemSiteId { get; set; }
        public virtual ICollection<MachineClickChargeLookup> MachineClickChargeLookups { get; set; }
        public virtual ICollection<MachineClickChargeZone> MachineClickChargeZones { get; set; }
        public virtual ICollection<MachineGuillotineCalc> MachineGuillotineCalcs { get; set; }
        public virtual ICollection<MachineMeterPerHourLookup> MachineMeterPerHourLookups { get; set; }
        public virtual ICollection<MachinePerHourLookup> MachinePerHourLookups { get; set; }
        public virtual ICollection<MachineSpeedWeightLookup> MachineSpeedWeightLookups { get; set; }
        public virtual ICollection<Machine> Machines { get; set; }
    }
}
