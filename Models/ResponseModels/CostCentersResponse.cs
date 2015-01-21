using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class CostCentersResponse
    {
        public IEnumerable<CostCentre> CostCenters { get; set; }
        public int RowCount { get; set; }
    }
}
