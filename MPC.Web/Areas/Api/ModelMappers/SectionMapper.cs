using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
using RequestModels = MPC.Models.RequestModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Section Mapper
    /// </summary>
    public static class SectionMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static SectionForPhraseLibrary CreateFrom(this DomainModels.Section source)
        {
            return new SectionForPhraseLibrary
            {
                SectionId = source.SectionId,
                ParentId = source.ParentId,
                SectionName = source.SectionName,
                PhrasesFields = source.PhraseFields != null ? source.PhraseFields.Select(s => s.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static DomainModels.Section CreateFrom(this SectionForPhraseLibrary source)
        {
            return new DomainModels.Section
            {
                SectionId = source.SectionId,
                SectionName = source.SectionName,
                PhraseFields = source.PhrasesFields != null ? source.PhrasesFields.Select(pf => pf.CreateFrom()).ToList() : null
            };
        }
        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static RequestModels.PhraseLibrarySaveModel CreateFrom(this PhraseLibrarySaveModel source)
        {
            return new RequestModels.PhraseLibrarySaveModel
            {
                Sections = source.Sections != null ? source.Sections.Select(s => s.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static Section CreateFromCampaign(this DomainModels.Section source)
        {
            return new Section
            {
                SectionId = source.SectionId,
                SectionName = source.SectionName,
                CampaignEmailVariables = source.CampaignEmailVariables != null ? source.CampaignEmailVariables.Select(cev => cev.CreateFrom()).ToList() : null
            };
        }
    }
}