using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class usp_ChartMonthlyOrdersCount_Result
    {
        public string CompanyName { get; set; }
        public Nullable<int> TotalOrders { get; set; }
        public Nullable<int> Month { get; set; }
        public string monthname { get; set; }
        public Nullable<int> year { get; set; }
    }
}
