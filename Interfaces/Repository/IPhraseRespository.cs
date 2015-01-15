using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Phrase Respository Interface
    /// </summary>
    public interface IPhraseRespository : IBaseRepository<Phrase, long>
    {
        /// <summary>
        /// Get Phrases By Phrase Filed Id
        /// </summary>
        IEnumerable<Phrase> GetPhrasesByPhraseFiledId(long phraseFieldId);
    }
}
