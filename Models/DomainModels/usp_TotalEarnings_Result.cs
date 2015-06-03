namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Total Earnings Result Domain Model
    /// </summary>
    public class usp_TotalEarnings_Result
    {
        public double? Total { get; set; }
        public int? Orders { get; set; }
        public string store { get; set; }
        public string Month { get; set; }
        public int? monthname { get; set; }
        public int? year { get; set; }
    }
}
