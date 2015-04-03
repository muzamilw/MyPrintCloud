using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class BestPressResponse
    {
        public List<BestPress> PressList { get; set; }
        public List<CostCentre> UserCostCenters { get; set; }
    }
}