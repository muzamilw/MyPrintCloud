﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class MachineClickChargeLookup
    {
        public long Id { get; set; }
        public long MethodId { get; set; }
        public double? SheetCost { get; set; }
        public int? Sheets { get; set; }
        public double? SheetPrice { get; set; }
        public double? TimePerHour { get; set; }

        public virtual LookupMethod LookupMethod { get; set; }
    }
}
