
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Section Repository Interface
    /// </summary>
    public interface ISectionRepository : IBaseRepository<Section, long>
    {
        /// <summary>
        /// Get All Sections For Phrase Library
        /// </summary>
        IEnumerable<Section> GetSectionsForPhraseLibrary();

        /// <summary>
        /// Get Sections By Parent Id
        /// </summary>
        IEnumerable<Section> GetSectionsByParentId(long parentId);
    }
}
