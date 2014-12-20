using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Campaign
    {

        public long CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string Description { get; set; }
        public int? DataSourceType { get; set; }
        public short? RunCampaignFor { get; set; }
        public bool? IncludeCustomers { get; set; }
        public bool? IncludeSuppliers { get; set; }
        public bool? IncludeProspects { get; set; }
        public bool? IncludeKeyword { get; set; }
        public string SearchKeyword { get; set; }
        public bool? IncludeType { get; set; }
        public bool? IncludeFlag { get; set; }
        public bool? IncludeName { get; set; }
        public bool? IncludeAddress { get; set; }
        public bool? IncludeContactName { get; set; }
        public string SubjectA { get; set; }
        public string HTMLMessageA { get; set; }
        public string AttachmentFileNameFieldName { get; set; }
        public string AttachmentType { get; set; }
        public string EmailField { get; set; }
        public string CCEmailField { get; set; }
        public string SMTPServer { get; set; }
        public string FromAddress { get; set; }
        public string ReturnPathAddress { get; set; }
        public string ReplyToAddress { get; set; }
        public string ErrorsToAddress { get; set; }
        public string SMTPServerType { get; set; }
        public bool? EnableSchedule { get; set; }
        public DateTime? StartDateTime { get; set; }
        public int? RepeatEvery { get; set; }
        public string RepeatInterval { get; set; }
        public DateTime? LastRunOn { get; set; }
        public short? MessageType { get; set; }
        public bool? ABMessaging { get; set; }
        public string SubjectB { get; set; }
        public string HTMLMessageB { get; set; }
        public bool? IncludePlainText { get; set; }
        public int? SMTPDelaySeconds { get; set; }
        public bool? EmailAddressLogFiles { get; set; }
        public string PlainTextMessageA { get; set; }
        public string PlainTextMessageB { get; set; }
        public int? UId { get; set; }
        public bool? Private { get; set; }
        public string SubscribeEmailUpdateField { get; set; }
        public string UnsubscribeEmailHyperlinkText { get; set; }
        public string SubscribeEmailAddress { get; set; }
        public string SubscribeEmailHyperlinkText { get; set; }
        public long? UnSubscribeCount { get; set; }
        public long? SubscribeCount { get; set; }
        public long? BounceCount { get; set; }
        public DateTime? LastRunEndDateTime { get; set; }
        public long? MessageCount { get; set; }
        public long? SuccessCount { get; set; }
        public long? FailedCount { get; set; }
        public bool? EnableLogFiles { get; set; }
        public bool? ContinueIfWritebackError { get; set; }
        public string EmailFieldPreviewColumn2 { get; set; }
        public string EmailFieldPreviewColumn3 { get; set; }
        public string AttachmentFileName { get; set; }
        public bool? ClearCounters { get; set; }
        public string SMTPServer2 { get; set; }
        public int? LockedByUID { get; set; }
        public int? UnSubscribeCountTotal { get; set; }
        public int? SubscribeCountTotal { get; set; }
        public int? BounceCountTotal { get; set; }
        public bool? EnableEmailAddressCache { get; set; }
        public int? OpenedCount { get; set; }
        public int? OpenedCountTotal { get; set; }
        public string OpenedEmailUpdateField { get; set; }
        public int? EnableOpenedEmail { get; set; }
        public string ABMessagingUpdateField { get; set; }
        public string EmailLogFileAddress2 { get; set; }
        public string EmailLogFileAddress3 { get; set; }
        public string CampaignCategory { get; set; }
        public int? SoftBounceCountTotal { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
        public bool? EnableWriteBackTab { get; set; }
        public bool? EnableClickThruTab { get; set; }
        public string UnsubscribeMethod { get; set; }
        public bool? UseWYSWYG { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? Status { get; set; }
        public bool ValidateEmail { get; set; }
        public short? CampaignType { get; set; }
        public int? SystemSiteId { get; set; }
        public bool? IncludeCorporateCustomers { get; set; }
        public bool? IsEnabled { get; set; }
        public int? SendEmailAfterDays { get; set; }
        public int? EmailEvent { get; set; }
        public bool? isSystemEmail { get; set; }
        public string FlagIDs { get; set; }
        public string CustomerTypeIDs { get; set; }
        public string GroupIDs { get; set; }
        public string FromName { get; set; }
        public int? CampaignReportId { get; set; }
        public long? OrganisationId { get; set; }
        public long? CompanyId { get; set; }
        public string UnsubscribeEmailAddress { get; set; }

        public virtual ICollection<CampaignImage> CampaignImages { get; set; }

    }
}
