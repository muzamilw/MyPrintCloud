using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MPC.MIS.Areas.Api.Models
{
    public class MachineUpdateRequestModel
    {
        public int Type { get; set; }
        public Machine machine { get; set; }
      
        public MachineClickChargeZone ClickChargeZone { get; set; }
        public MachineMeterPerHourLookup MeterPerHourLookup { get; set; }
        public MachineGuillotineCalc GuillotineCalc { get; set; }
        public IEnumerable<MachineGuilotinePtv> GuilotinePtv { get; set; }
        public MachineSpeedWeightLookup SpeedWeightCal { get; set; }
        
      
    }
}