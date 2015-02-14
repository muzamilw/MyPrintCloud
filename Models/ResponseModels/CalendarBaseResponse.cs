using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Calendar Base Response
    /// </summary>
    public class CalendarBaseResponse
    {
        public IEnumerable<SystemUser> SystemUsers { get; set; }
        public IEnumerable<CompanyContact> CompanyContacts { get; set; }
        public IEnumerable<PipeLineProduct> PipeLineProducts { get; set; }
        public IEnumerable<PipeLineSource> PipeLineSources { get; set; }
        public IEnumerable<SectionFlag> SectionFlags { get; set; }
        public IEnumerable<ActivityType> ActivityTypes { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
        public Guid LoggedInUserId { get; set; }
    }
}
