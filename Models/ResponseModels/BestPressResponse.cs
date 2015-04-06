using System;
using System.Collections.Generic;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class BestPressResponse
    {
        public List<BestPress> PressList { get; set; }
        public List<CostCentre> UserCostCenters { get; set; }
    }
}
