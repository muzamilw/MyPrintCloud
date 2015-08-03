using System;

namespace MPC.Models.DomainModels
{
    public class usp_ChartTopPerformingStores_Result
    {
        public Nullable<double> CurrentMonthEarning { get; set; }
        public Nullable<double> LastMonthEarning { get; set; }
        public string Name { get; set; }
        public string MonthName { get; set; }
        public int year { get; set; }
        public int Month { get; set; }
    }
}
