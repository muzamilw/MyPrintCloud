using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CostCenterListViewModel
    {
        public long CostCentreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int CalculationMethodType { get; set; }
        public bool IsDisabled { get; set; }
    }
}