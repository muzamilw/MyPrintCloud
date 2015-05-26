using System.Collections.Generic;
using System.IO;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Live Jobs Service Interface
    /// </summary>
    public interface ILiveJobsService
    {
        /// <summary>
        /// Get Items For Live Job Items 
        /// </summary>
        LiveJobsSearchResponse GetItemsForLiveJobs(LiveJobsRequestModel request);

        /// <summary>
        /// Download Artwork
        /// </summary>
        Stream DownloadArtwork(List<long?> itemList);

        /// <summary>
        /// Get System Users
        /// </summary>
        IEnumerable<SystemUser> GetSystemUsers();
    }
}
