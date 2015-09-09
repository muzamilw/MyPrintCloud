

namespace MPC.Models.DomainModels
{
    public class usp_ChartMonthlyEarningsbyStore_Result
    {
        public string Name { get; set; }
        public double? TotalEarning { get; set; }
        public string MonthName { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
    }
}
