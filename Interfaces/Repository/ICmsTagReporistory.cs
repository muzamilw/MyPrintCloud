using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Cms Tag Repository Interface
    /// </summary>
    public interface ICmsTagReporistory : IBaseRepository<CmsTag, long>
    {
        /// <summary>
        /// Get CMS Tag For CMS Page (Load Default keywords)
        /// </summary>
        IEnumerable<CmsTag> GetAllForCmsPage();
    }
}
