using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Phrase Mapper
    /// </summary>
    public static class PhraseMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static Phrase CreateFrom(this DomainModels.Phrase source)
        {
            return new Phrase
            {
                PhraseId = source.PhraseId,
                FieldId = source.FieldId,
                Phrase1 = source.Phrase1
            };
        }

        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.Phrase CreateFrom(this Phrase source)
        {
            return new DomainModels.Phrase
            {
                PhraseId = source.PhraseId,
                FieldId = source.FieldId,
                Phrase1 = source.Phrase1,
                IsDeleted = source.IsDeleted,
            };
        }
    }
}