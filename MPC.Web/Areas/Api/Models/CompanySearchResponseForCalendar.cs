using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Company Search Api Response For Calendar
    /// </summary>
    public class CompanySearchResponseForCalendar
    {
        public IEnumerable<CompanyForCalender> Companies { get; set; }
        public int TotalCount { get; set; }
    }
}