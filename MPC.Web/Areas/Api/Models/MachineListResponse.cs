using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class MachineListResponse
    {
        public IEnumerable<MachineList> machine { get; set; }
        //public LookupMethod lookupMethod { get; set; }
        public int RowCount { get; set; }
    }
}