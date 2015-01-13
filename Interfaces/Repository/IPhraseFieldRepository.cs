using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Phrase Field Repository Interface
    /// </summary>
    public interface IPhraseFieldRepository : IBaseRepository<PhraseField, long>
    {
        /// <summary>
        /// Get All Phrase Field By Section Id
        /// </summary>
        IEnumerable<PhraseField> GetPhraseFieldsBySectionId(long sectionId);
    }
}
