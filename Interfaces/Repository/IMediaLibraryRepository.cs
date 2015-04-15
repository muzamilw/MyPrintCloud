using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Media Library Repository Interface
    /// </summary>
    public interface IMediaLibraryRepository : IBaseRepository<MediaLibrary, long>
    {
        /// <summary>
        /// Get Media Libraries By Company Id
        /// </summary>
        IEnumerable<MediaLibrary> GetMediaLibrariesByCompanyId(long companyId);
    }
}
