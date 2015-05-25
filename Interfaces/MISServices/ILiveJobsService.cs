using System.IO;
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

        Stream DownloadArtwork();
    }
}
