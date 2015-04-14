using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CostCentreMatrixRequestModel
    {
        public CostCentreMatrix CostCentreMatrix { get; set; }
        public IEnumerable<CostCentreMatrixDetail> CostCentreMatrixDetail { get; set; }
    }
}