using System;

namespace MPC.Models.DomainModels
{
    public class usp_ChartTopPerformingStores_Result
    {
        public string Name { get; set; }
        public int? TotalCustomers { get; set; }
        public int? Month { get; set; }
        public string MonthName { get; set; }
        public int? Year { get; set; }
    }
}
