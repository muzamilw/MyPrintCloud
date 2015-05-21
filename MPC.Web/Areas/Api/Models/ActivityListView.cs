using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Activity For List 
    /// </summary>
    public class ActivityListView
    {
        public int ActivityId { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public string ActivityRef { get; set; }
        public int? FlagId { get; set; }
        public Guid? SystemUserId { get; set; }
    }
}