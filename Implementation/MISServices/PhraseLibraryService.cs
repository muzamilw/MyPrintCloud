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

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public PhraseLibraryService(ISectionRepository sectionRepository)
        {
            this.sectionRepository = sectionRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Section
        /// </summary>
        public void GetSections()
        {
            List<Section> sections = sectionRepository.GetAll().ToList();
            foreach (var section in sections)
            {

            }
        }


        #endregion
    }
}
