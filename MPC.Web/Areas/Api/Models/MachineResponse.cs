using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class MachineResponse
    {
        public IEnumerable<MachineList> machine { get; set; }
        public int RowCount { get; set; }
    }
}