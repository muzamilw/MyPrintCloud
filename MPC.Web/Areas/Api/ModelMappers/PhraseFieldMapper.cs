using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Phrase Field Mapper
    /// </summary>
    public static class PhraseFieldMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static PhraseField CreateFrom(this DomainModels.PhraseField source)
        {
            return new PhraseField
            {
                FieldId = source.FieldId,
                FieldName = source.FieldName,
                SectionId = source.SectionId,
          
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static DomainModels.PhraseField CreateFrom(this PhraseField source)
        {
            return new DomainModels.PhraseField
            {
                FieldId = source.FieldId,
                FieldName = source.FieldName,
                SectionId = source.SectionId,
                Phrases = source.Phrases != null ? source.Phrases.Select(p => p.CreateFrom()).ToList() : null
            };
        }
    }
}