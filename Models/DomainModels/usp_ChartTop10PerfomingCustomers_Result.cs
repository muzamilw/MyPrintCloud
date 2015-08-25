using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class usp_ChartTop10PerfomingCustomers_Result
    {
        public string ContactName { get; set; }
        public double? CurrentMonthOrders { get; set; }
        public double? LastMonthOrders { get; set; }
        
    }
}
