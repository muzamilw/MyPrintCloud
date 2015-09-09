namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Company Contact For Calendar Api Model
    /// </summary>
    public class CompanyContactForCalendar
    {
        public long ContactId { get; set; }
        public long CompanyId { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
    }
}