using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class usp_ChartEstimateToOrderConversionCount_Result
    {
        public int? EstimateCount { get; set; }
        public int? ConvertedCount { get; set; }
        public int? Month { get; set; }
        public string MonthName { get; set; }
        public int? Year { get; set; }
    }
}
