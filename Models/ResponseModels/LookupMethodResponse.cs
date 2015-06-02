using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class LookupMethodResponse
    {
        public long LookupMethodId { get; set; }
        public LookupMethod LookupMethod { get; set; }
        public MachineClickChargeLookup ClickChargeLookup { get; set; }
        public MachineClickChargeZone ClickChargeZone { get; set; }
        public MachineGuillotineCalc GuillotineCalc { get; set; }
        public IEnumerable<MachineGuilotinePtv> GuilotinePtv { get; set; }
        public MachineMeterPerHourLookup MeterPerHourLookup { get; set; }
        public MachinePerHourLookup PerHourLookup { get; set; }
        public MachineSpeedWeightLookup SpeedWeightLookup { get; set; }
        
    }
}
