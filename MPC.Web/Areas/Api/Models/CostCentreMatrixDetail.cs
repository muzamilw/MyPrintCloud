using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CostCentreMatrixDetail
    {
        public long Id { get; set; }
        public int MatrixId { get; set; }
        public string Value { get; set; }
    }
}