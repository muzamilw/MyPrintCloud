using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CostCentreQuestionRequestModel
    {
        public CostCentreQuestion Question { get; set; }
        public  IEnumerable<CostCentreAnswer> Answer { get; set; }
    }
}