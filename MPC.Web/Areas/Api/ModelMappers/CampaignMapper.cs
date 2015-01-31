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
                EmailEvent = source.EmailEvent,
                IsEnabled = source.IsEnabled,
                StartDateTime = source.StartDateTime,
                SendEmailAfterDays = source.SendEmailAfterDays,
                CampaignType = source.CampaignType,
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
                CampaignId = source.CampaignId,
                CampaignName = source.CampaignName,
                EmailEvent = source.EmailEvent,
                IsEnabled = source.IsEnabled,
                StartDateTime = source.StartDateTime,
                SendEmailAfterDays = source.SendEmailAfterDays,
                CampaignType = source.CampaignType,
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
        #endregion
    }
}