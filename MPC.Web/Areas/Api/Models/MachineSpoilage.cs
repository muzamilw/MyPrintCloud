using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class MachineSpoilage
    {
        public int MachineSpoilageId { get; set; }
        public int? MachineId { get; set; }
        public int? SetupSpoilage { get; set; }
        public float? RunningSpoilage { get; set; }
        public int? NoOfColors { get; set; }
    }
}