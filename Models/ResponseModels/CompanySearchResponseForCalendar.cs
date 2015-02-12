using System.Collections;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Company Search Domain Response For Calendar
    /// </summary>
    public class CompanySearchResponseForCalendar
    {
        public IEnumerable<Company> Companies { get; set; }
        public int TotalCount { get; set; }
    }
}
