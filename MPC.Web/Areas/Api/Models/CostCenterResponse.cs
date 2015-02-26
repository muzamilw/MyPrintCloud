using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CostCenterResponse
    {
        public IEnumerable<CostCenterListViewModel> CostCenters { get; set; }
        
        public int RowCount { get; set; }
    }
}