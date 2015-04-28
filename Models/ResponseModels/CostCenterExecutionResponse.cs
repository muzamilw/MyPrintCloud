using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.ResponseModels
{
    public class CostCenterExecutionResponse
    {
        public object[] CostCentreParamsArray { get; set; }
        public double dblReturnCost { get; set; }
        
    }
}
