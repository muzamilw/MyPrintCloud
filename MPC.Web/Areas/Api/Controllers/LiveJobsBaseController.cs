using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;


namespace MPC.MIS.Areas.Api.Controllers
{

    /// <summary>
    /// Live Jobs Base API Controller 
    /// </summary>
    public class LiveJobsBaseController : ApiController
    {
        #region Private

        private readonly ILiveJobsService liveJobsService;


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LiveJobsBaseController(ILiveJobsService liveJobsService)
        {
            this.liveJobsService = liveJobsService;
        }



        #endregion

        #region Public

        /// <summary>
        /// Base Data
        /// </summary>
        public IEnumerable<SystemUserDropDown> Get()
        {
            return liveJobsService.GetSystemUsers().Select(user => user.CreateFrom());
        }

        #endregion
    }
}