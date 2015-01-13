﻿using System.Collections.Generic;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Phrase Library Service Interface
    /// </summary>
    public interface IPhraseLibraryService
    {
        IEnumerable<Section> GetSections();

        /// <summary>
        /// Get Phrase Field By Section Id
        /// </summary>
        IEnumerable<PhraseField> GetPhraseFieldsBySectionId(long sectionId);
    }
}
