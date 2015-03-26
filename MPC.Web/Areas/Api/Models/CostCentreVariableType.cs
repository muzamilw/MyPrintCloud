using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CostCentreVariableType
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CostCentreVariable> VariablesList { get; set; } 
    }
}