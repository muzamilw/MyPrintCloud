using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    /// <summary>
    /// Phrase Library Service
    /// </summary>
    public class PhraseLibraryService : IPhraseLibraryService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly ISectionRepository sectionRepository;
        private readonly IPhraseRespository phraseRespository;
        private readonly IPhraseFieldRepository phraseFieldRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public PhraseLibraryService(ISectionRepository sectionRepository, IPhraseRespository phraseRespository, IPhraseFieldRepository phraseFieldRepository)
        {
            this.sectionRepository = sectionRepository;
            this.phraseRespository = phraseRespository;
            this.phraseFieldRepository = phraseFieldRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Section
        /// </summary>
        public IEnumerable<Section> GetSections()
        {
            IEnumerable<Section> sections = sectionRepository.GetSectionsForPhraseLibrary();
            foreach (var section in sections)
            {
                if (section.ParentId == 0)
                {
                    section.ChildSections = sectionRepository.GetSectionsByParentId(section.ParentId.Value);

                }
            }

            return sections;
        }

        /// <summary>
        /// Get Phrase Field By Section Id
        /// </summary>
        public IEnumerable<PhraseField> GetPhraseFieldsBySectionId(long sectionId)
        {
            return phraseFieldRepository.GetPhraseFieldsBySectionId(sectionId);
        }
        #endregion
    }
}
