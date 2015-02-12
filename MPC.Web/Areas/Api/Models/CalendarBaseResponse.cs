using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Calendar Base Response
    /// </summary>
    public class CalendarBaseResponse
    {
        public IEnumerable<SystemUserDropDown> SystemUsers { get; set; }
        public IEnumerable<CompanyContactDropDown> CompanyContacts { get; set; }
        public IEnumerable<PipeLineProduct> PipeLineProducts { get; set; }
        public IEnumerable<PipeLineSource> PipeLineSources { get; set; }
        public IEnumerable<SectionFlagDropDown> SectionFlags { get; set; }
        //public IEnumerable<Activity> Activities { get; set; }
    }
}