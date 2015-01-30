using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class MachineResource
    {
        public long Id { get; set; }
        public int? MachineId { get; set; }
        public int? ResourceId { get; set; }

        public virtual Machine Machine { get; set; }
    }
}