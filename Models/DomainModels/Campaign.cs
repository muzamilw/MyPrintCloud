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
        public Nullable<int> DataSourceType { get; set; }
        public Nullable<short> RunCampaignFor { get; set; }
        public Nullable<bool> IncludeCustomers { get; set; }
        public Nullable<bool> IncludeSuppliers { get; set; }
        public Nullable<bool> IncludeProspects { get; set; }
        public Nullable<bool> IncludeKeyword { get; set; }
        public string SearchKeyword { get; set; }
        public Nullable<bool> IncludeType { get; set; }
        public Nullable<bool> IncludeFlag { get; set; }
        public Nullable<bool> IncludeName { get; set; }
        public Nullable<bool> IncludeAddress { get; set; }
        public Nullable<bool> IncludeContactName { get; set; }
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
        public Nullable<bool> EnableSchedule { get; set; }
        public Nullable<System.DateTime> StartDateTime { get; set; }
        public Nullable<int> RepeatEvery { get; set; }
        public string RepeatInterval { get; set; }
        public Nullable<System.DateTime> LastRunOn { get; set; }
        public Nullable<short> MessageType { get; set; }
        public Nullable<bool> ABMessaging { get; set; }
        public string SubjectB { get; set; }
        public string HTMLMessageB { get; set; }
        public Nullable<bool> IncludePlainText { get; set; }
        public Nullable<int> SMTPDelaySeconds { get; set; }
        public Nullable<bool> EmailAddressLogFiles { get; set; }
        public string PlainTextMessageA { get; set; }
        public string PlainTextMessageB { get; set; }
        public Nullable<int> UId { get; set; }
        public Nullable<bool> Private { get; set; }
        public string SubscribeEmailUpdateField { get; set; }
        public string UnsubscribeEmailHyperlinkText { get; set; }
        public string SubscribeEmailAddress { get; set; }
        public string SubscribeEmailHyperlinkText { get; set; }
        public Nullable<long> UnSubscribeCount { get; set; }
        public Nullable<long> SubscribeCount { get; set; }
        public Nullable<long> BounceCount { get; set; }
        public Nullable<System.DateTime> LastRunEndDateTime { get; set; }
        public Nullable<long> MessageCount { get; set; }
        public Nullable<long> SuccessCount { get; set; }
        public Nullable<long> FailedCount { get; set; }
        public Nullable<bool> EnableLogFiles { get; set; }
        public Nullable<bool> ContinueIfWritebackError { get; set; }
        public string EmailFieldPreviewColumn2 { get; set; }
        public string EmailFieldPreviewColumn3 { get; set; }
        public string AttachmentFileName { get; set; }
        public Nullable<bool> ClearCounters { get; set; }
        public string SMTPServer2 { get; set; }
        public Nullable<int> LockedByUID { get; set; }
        public Nullable<int> UnSubscribeCountTotal { get; set; }
        public Nullable<int> SubscribeCountTotal { get; set; }
        public Nullable<int> BounceCountTotal { get; set; }
        public Nullable<bool> EnableEmailAddressCache { get; set; }
        public Nullable<int> OpenedCount { get; set; }
        public Nullable<int> OpenedCountTotal { get; set; }
        public string OpenedEmailUpdateField { get; set; }
        public Nullable<int> EnableOpenedEmail { get; set; }
        public string ABMessagingUpdateField { get; set; }
        public string EmailLogFileAddress2 { get; set; }
        public string EmailLogFileAddress3 { get; set; }
        public string CampaignCategory { get; set; }
        public Nullable<int> SoftBounceCountTotal { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
        public Nullable<bool> EnableWriteBackTab { get; set; }
        public Nullable<bool> EnableClickThruTab { get; set; }
        public string UnsubscribeMethod { get; set; }
        public Nullable<bool> UseWYSWYG { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> Status { get; set; }
        public bool ValidateEmail { get; set; }
        public Nullable<short> CampaignType { get; set; }
        public Nullable<int> SystemSiteId { get; set; }
        public Nullable<bool> IncludeCorporateCustomers { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public Nullable<int> SendEmailAfterDays { get; set; }
        public Nullable<int> EmailEvent { get; set; }
        public Nullable<bool> isSystemEmail { get; set; }
        public string FlagIDs { get; set; }
        public string CustomerTypeIDs { get; set; }
        public string GroupIDs { get; set; }
        public string FromName { get; set; }
        public Nullable<int> CampaignReportId { get; set; }
        public Nullable<long> OrganisationId { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public string UnsubscribeEmailAddress { get; set; }

        public virtual ICollection<CampaignImage> CampaignImages { get; set; }
    }
}
