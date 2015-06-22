using System.Collections.Generic;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Phrase Library Service Interface
    /// </summary>
    public interface IPhraseLibraryService
    {
        IEnumerable<Section> GetSections();

        /// <summary>
        /// Get Phrases By Phrase Field Id
        /// </summary>
        IEnumerable<Phrase> GetPhrasesByPhraseFiledId(long sectionId);


        /// <summary>
        /// Save Phase Library
        /// </summary>
        void SavePhaseLibrary(PhraseLibrarySaveModel phaseLibrary);

        /// <summary>
        /// Get Phrase Fields By Section Id
        /// </summary>
        IEnumerable<PhraseField> GetPhraseFiledsBySectionId(long sectionId);

        
    }
}
