using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

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
                ChildSections = source.ChildSections.Select(s => s.CreateFromForChild()).ToList()
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static SectionForPhraseLibrary CreateFromForChild(this DomainModels.Section source)
        {
            return new SectionForPhraseLibrary
            {
                SectionId = source.SectionId,
                ParentId = source.ParentId,
                SectionName = source.SectionName
            };
        }
    }
}