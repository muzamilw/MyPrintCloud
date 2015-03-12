using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class MachinePerHourLookup
    {
        public long Id { get; set; }
        public long? MethodId { get; set; }
        public double? SpeedCost { get; set; }
        public double? Speed { get; set; }
        public double SpeedPrice { get; set; }

        public virtual LookupMethod LookupMethod { get; set; }
    }
}