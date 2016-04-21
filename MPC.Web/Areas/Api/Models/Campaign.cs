using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Campaign Model
    /// </summary>
    public class Campaign
    {

        public long CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string Description { get; set; }
        public short? CampaignType { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? StartDateTime { get; set; }
        public bool? IncludeCustomers { get; set; }
        public bool? IncludeSuppliers { get; set; }
        public bool? IncludeProspects { get; set; }
        public bool? IncludeNewsLetterSubscribers { get; set; }
        public bool? IncludeFlag { get; set; }
        public string FlagIDs { get; set; }
        public string CustomerTypeIDs { get; set; }
        public string GroupIDs { get; set; }
        public string SubjectA { get; set; }
        public string HTMLMessageA { get; set; }
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string ReturnPathAddress { get; set; }
        public string ReplyToAddress { get; set; }
        public string EmailLogFileAddress2 { get; set; }
        public int? EmailEvent { get; set; }
        public string EventName { get; set; }
        public int? SendEmailAfterDays { get; set; }

        public bool? IncludeType { get; set; }
        public bool? IncludeCorporateCustomers { get; set; }
        public bool? EnableLogFiles { get; set; }
        public string EmailLogFileAddress3 { get; set; }
        public string NotificationEmailIds { get; set; }

        public List<CampaignImage> CampaignImages { get; set; }
    }
}