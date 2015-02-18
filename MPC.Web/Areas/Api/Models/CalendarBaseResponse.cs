using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Calendar Base Response
    /// </summary>
    public class CalendarBaseResponse
    {
        public IEnumerable<SystemUserDropDown> SystemUsers { get; set; }
        public IEnumerable<PipeLineProduct> PipeLineProducts { get; set; }
        public IEnumerable<PipeLineSource> PipeLineSources { get; set; }
        public IEnumerable<SectionFlagDropDown> SectionFlags { get; set; }
        public IEnumerable<ActivityTypeDropDown> ActivityTypes { get; set; }
        public Guid LoggedInUserId { get; set; }
    }
}