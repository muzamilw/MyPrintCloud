using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Campaign Model
    /// </summary>
    public class Campaign
    {

        public long CampaignId { get; set; }
        public string CampaignName { get; set; }
        public int? EmailEvent { get; set; }
        public string EventName { get; set; }
        public DateTime? StartDateTime { get; set; }
        public bool? IsEnabled { get; set; }
        public int? SendEmailAfterDays { get; set; }
        public short? CampaignType { get; set; }

    }
}