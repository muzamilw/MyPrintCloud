using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.Models.RequestModels;


namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Live Jobs API Controller
    /// </summary>
    public class LiveJobsController : ApiController
    {
        #region Private

        private readonly ILiveJobsService liveJobsService;


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LiveJobsController(ILiveJobsService liveJobsService)
        {
            this.liveJobsService = liveJobsService;
        }

        #endregion

        #region Public

        /// <summary>
        ///   Get Items For Live Jobs
        /// </summary>
        public LiveJobsSearchResponse Get([FromUri] LiveJobsRequestModel request)
        {
            return liveJobsService.GetItemsForLiveJobs(request).CreateFrom();
        }
        #endregion
    }
}
