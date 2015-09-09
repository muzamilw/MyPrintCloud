using System;
using System.Collections.Generic;

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
        public bool? IncludeNewsLetterSubscribers { get; set; }
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

        public virtual Company Company { get; set; }
        public virtual EmailEvent CampaignEmailEvent { get; set; }
        public virtual ICollection<CampaignImage> CampaignImages { get; set; }



        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(Campaign target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.CampaignName = CampaignName;
            target.Description = Description;
            target.DataSourceType = DataSourceType;
            target.RunCampaignFor = RunCampaignFor;
            target.IncludeCustomers = IncludeCustomers;
            target.OrganisationId = OrganisationId;
              target.CampaignName = CampaignName;
            target.IncludeSuppliers = IncludeSuppliers;
            target.IncludeProspects = IncludeProspects;
            target.IncludeNewsLetterSubscribers = IncludeNewsLetterSubscribers;
            target.IncludeKeyword = IncludeKeyword;
            target.SearchKeyword = SearchKeyword;
            
            target.IncludeType = IncludeType;
            target.IncludeFlag = IncludeFlag;
            target.IncludeName = IncludeName;
          
            target.IncludeContactName = IncludeContactName;
            target.SubjectA = SubjectA;
            target.HTMLMessageA = HTMLMessageA;
            target.AttachmentFileNameFieldName = AttachmentFileNameFieldName;
           
            target.AttachmentType = AttachmentType;
            target.EmailField = EmailField;
            target.CCEmailField = CCEmailField;
            target.SMTPServer = SMTPServer;
           target.FromAddress = FromAddress;
            target.ReturnPathAddress = ReturnPathAddress;
            target.ReplyToAddress = ReplyToAddress;
          
            target.ErrorsToAddress = ErrorsToAddress;
            target.SMTPServerType = SMTPServerType;
            target.StartDateTime = StartDateTime;
            target.RepeatEvery = RepeatEvery;
           
            target.RepeatInterval = RepeatInterval;
            target.LastRunOn = LastRunOn;
            target.MessageType = MessageType;
            target.ABMessaging = ABMessaging;
              target.SubjectB = SubjectB;
            target.HTMLMessageB = HTMLMessageB;
            target.IncludePlainText = IncludePlainText;
          
            target.SMTPDelaySeconds = SMTPDelaySeconds;
            target.EmailAddressLogFiles = EmailAddressLogFiles;
            target.PlainTextMessageA = PlainTextMessageA;
            target.PlainTextMessageB = PlainTextMessageB;
           
            target.UId = UId;
            target.SubscribeEmailUpdateField = SubscribeEmailUpdateField;
            target.UnsubscribeEmailHyperlinkText = UnsubscribeEmailHyperlinkText;
            target.SubscribeEmailHyperlinkText = SubscribeEmailHyperlinkText;
              target.UnSubscribeCount = UnSubscribeCount;
            target.SubscribeCount = SubscribeCount;
            target.BounceCount = BounceCount;
          
            target.LastRunEndDateTime = LastRunEndDateTime;
            target.FailedCount = FailedCount;
            target.EnableLogFiles = EnableLogFiles;
            target.ContinueIfWritebackError = ContinueIfWritebackError;
           
            target.EmailFieldPreviewColumn2 = EmailFieldPreviewColumn2;
            target.EmailFieldPreviewColumn3 = EmailFieldPreviewColumn3;
            target.AttachmentFileName = AttachmentFileName;
            target.ClearCounters = ClearCounters;
              target.SMTPServer2 = SMTPServer2;
            target.LockedByUID = LockedByUID;
            target.UnSubscribeCountTotal = UnSubscribeCountTotal;
          
            target.SubscribeCountTotal = SubscribeCountTotal;
            target.BounceCountTotal = BounceCountTotal;
            target.EnableEmailAddressCache = EnableEmailAddressCache;
            target.OpenedCount = OpenedCount;
           
            target.OpenedCountTotal = OpenedCountTotal;
            target.OpenedEmailUpdateField = OpenedEmailUpdateField;
            target.EnableOpenedEmail = EnableOpenedEmail;
              target.ABMessagingUpdateField = ABMessagingUpdateField;
            target.EmailLogFileAddress2 = EmailLogFileAddress2;
            target.EmailLogFileAddress3 = EmailLogFileAddress3;
          
            target.CampaignCategory = CampaignCategory;
            target.SoftBounceCountTotal = SoftBounceCountTotal;
            target.SMTPUsername = SMTPUsername;
            target.SMTPPassword = SMTPPassword;
           
            target.EnableWriteBackTab = EnableWriteBackTab;
            target.EnableClickThruTab = EnableClickThruTab;
            target.UnsubscribeMethod = UnsubscribeMethod;
              target.UseWYSWYG = UseWYSWYG;
            target.CreationDate = CreationDate;
            target.Status = Status;
          
            target.CampaignType = CampaignType;
            target.SystemSiteId = SystemSiteId;
            target.IncludeCorporateCustomers = IncludeCorporateCustomers;
            target.IsEnabled = IsEnabled;
           
            target.SendEmailAfterDays = SendEmailAfterDays;
            target.EmailEvent = EmailEvent;
            target.FlagIDs = FlagIDs;
            target.GroupIDs = GroupIDs;
            target.FromName = FromName;
            target.CampaignReportId = CampaignReportId;
             target.OrganisationId = OrganisationId;
            
            target.UnsubscribeEmailAddress = UnsubscribeEmailAddress;




        }

        #endregion
    }
}
