using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class usp_ChartEstimateToOrderConversion_Result
    {
        public double? EstimateTotal { get; set; }

        public double? ConvertedTotal { get; set; }
        public int? Month { get; set; }
        public string MonthName { get; set; }
        public int? Year { get; set; }
    }
}
