using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class MachineListResponseModel
    {
        public IEnumerable<Machine> MachineList { get; set; }
        public IEnumerable<LookupMethod> lookupMethod { get; set; }
        public int RowCount { get; set; }
    }
}
