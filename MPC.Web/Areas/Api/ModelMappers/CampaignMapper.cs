using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

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
                //EventName = source.EmailEvent != null ? source.EmailEvent.EventName : null,
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

        #endregion
    }
}