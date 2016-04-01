using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
using ResponseModels = MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Campaign Mapper
    /// </summary>
    public static class CampaignMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Campaign CreateFrom(this DomainModels.Campaign source)
        {
            return new Campaign
            {
                CampaignId = source.CampaignId,
                CampaignName = source.CampaignName,
                Description = source.Description,
                CampaignType = source.CampaignType,
                IsEnabled = source.IsEnabled,
                IncludeCustomers = source.IncludeCustomers,
                IncludeSuppliers = source.IncludeSuppliers,
                IncludeProspects = source.IncludeProspects,
                IncludeNewsLetterSubscribers = source.IncludeNewsLetterSubscribers,
                IncludeFlag = source.IncludeFlag,
                FlagIDs = source.FlagIDs,
                CustomerTypeIDs = source.CustomerTypeIDs,
                GroupIDs = source.GroupIDs,
                SubjectA = source.SubjectA,
                HTMLMessageA = source.HTMLMessageA,
                FromAddress = source.FromAddress,
                ReturnPathAddress = source.ReturnPathAddress,
                ReplyToAddress = source.ReplyToAddress,
                EmailLogFileAddress2 = source.EmailLogFileAddress2,
                EmailEvent = source.EmailEvent,
                SendEmailAfterDays = source.SendEmailAfterDays,
                FromName = source.FromName,
                IncludeType = source.IncludeType,
                IncludeCorporateCustomers = source.IncludeCorporateCustomers,
                EnableLogFiles = source.EnableLogFiles,
                EmailLogFileAddress3 = source.EmailLogFileAddress3,
                StartDateTime = source.StartDateTime,
                CampaignImages = source.CampaignImages != null ? source.CampaignImages.Select(ci => ci.CreateFrom()).ToList() : null,
                EventName = source.CampaignEmailEvent != null ? source.CampaignEmailEvent.EventName : null,
            };
        }

        public static Campaign CreateFromForListView(this DomainModels.Campaign source)
        {
            return new Campaign
            {
                CampaignId = source.CampaignId,
                CampaignName = source.CampaignName,
                CampaignType = source.CampaignType,
                IsEnabled = source.IsEnabled,
                StartDateTime = source.StartDateTime,
                SendEmailAfterDays = source.SendEmailAfterDays,
                EventName = source.CampaignEmailEvent != null ? source.CampaignEmailEvent.EventName : null,
            };
        }


        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Campaign CreateFrom(this Campaign source)
        {
            return new DomainModels.Campaign
            {
                CampaignId = source.CampaignId < 0 ? 0 : source.CampaignId,
                CampaignName = source.CampaignName,
                Description = source.Description,
                CampaignType = source.CampaignType,
                IsEnabled = source.IsEnabled,
                IncludeCustomers = source.IncludeCustomers,
                IncludeSuppliers = source.IncludeSuppliers,
                IncludeProspects = source.IncludeProspects,
                IncludeNewsLetterSubscribers = source.IncludeNewsLetterSubscribers,
                IncludeFlag = source.IncludeFlag,
                FlagIDs = source.FlagIDs,
                CustomerTypeIDs = source.CustomerTypeIDs,
                GroupIDs = source.GroupIDs,
                SubjectA = source.SubjectA,
                HTMLMessageA = source.HTMLMessageA,
                FromAddress = source.FromAddress,
                ReturnPathAddress = source.ReturnPathAddress,
                ReplyToAddress = source.ReplyToAddress,
                EmailLogFileAddress2 = source.EmailLogFileAddress2,
                EmailEvent = source.EmailEvent,
                StartDateTime = source.StartDateTime,
                SendEmailAfterDays = source.SendEmailAfterDays,
                FromName = source.FromName,
                IncludeType = source.IncludeType,
                IncludeCorporateCustomers = source.IncludeCorporateCustomers,
                EnableLogFiles = source.EnableLogFiles,
                EmailLogFileAddress3 = source.EmailLogFileAddress3,
                CampaignImages = source.CampaignImages != null ? source.CampaignImages.Select(ci => ci.CreateFrom()).ToList() : null

            };
        }

        /// <summary>
        ///  Crete From Domain Model
        /// </summary>
        public static CampaignBaseResponse CreateFrom(this ResponseModels.CampaignBaseResponse source)
        {
            return new CampaignBaseResponse()
            {
                CompanyTypes = source.CompanyTypes.Select(ct => ct.CreateFrom()).ToList(),
                Groups = source.Groups.Select(g => g.CreateFrom()).ToList(),
                SectionFlags = source.SectionFlags.Select(g => g.CreateFromDropDown()).ToList(),
                CampaignSections = source.CampaignSections.Select(g => g.CreateFromCampaign()).ToList()
            };
        }

        public static EmailsResponse CreateFrom(this ResponseModels.EmailsResponse source)
        {
            return new EmailsResponse
            {
                OrganisationEmails = source.OrganisationEmails.Select(ct => ct.CreateFrom()).ToList(),
                EmailEvents = source.EmailEvents.Select(g => g.CreateFrom()).ToList(),
                CampaignSections = source.CampaignSections.Select(g => g.CreateFromCampaign()).ToList()
            };
        }
        #endregion
    }
}