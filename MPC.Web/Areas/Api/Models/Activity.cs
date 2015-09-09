using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Activity API Model
    /// </summary>
    public class Activity
    {
        public int ActivityId { get; set; }
        public int ActivityTypeId { get; set; }
        public string ActivityRef { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public string ActivityNotes { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool? IsCustomerActivity { get; set; }
        public int? ContactId { get; set; }
        public Guid? SystemUserId { get; set; }
        public bool? IsPrivate { get; set; }
        public long? CompanyId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? SourceId { get; set; }
        public int? FlagId { get; set; }
        public string CompanyName { get; set; }
        public int IsCustomerType { get; set; }

    }
}