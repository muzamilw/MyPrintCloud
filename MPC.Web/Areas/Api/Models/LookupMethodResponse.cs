using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
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
        public string CurrencySymbol { get; set; }
        
    }
}