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
        public string CompanyName { get; set; }
        public Nullable<int> OrderCount { get; set; }
        public Nullable<double> OrderAmount { get; set; }
        public Nullable<int> Month { get; set; }
        public string monthname { get; set; }
        public Nullable<int> year { get; set; }
    }
}
