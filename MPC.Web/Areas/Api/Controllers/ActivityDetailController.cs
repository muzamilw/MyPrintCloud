using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Activity Detail API Controller
    /// </summary>
    public class ActivityDetailController : ApiController
    {
        #region Private

        private readonly ICalendarService calendarService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ActivityDetailController(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }

        #endregion

        #region Public


        /// <summary>
        /// Get Activity Detail By ID
        /// </summary>
        public Activity Get([FromUri]int activityId)
        {
            return calendarService.ActivityDetail(activityId).CreateFrom();
        }

        /// <summary>
        /// Add/Update a Calendar Activity
        /// </summary>
        [ApiException]
        public void Post(Activity activity)
        {
            if (activity == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            calendarService.SaveActivityDropOrResize(activity.CreateFrom());
        }
        #endregion
    }
}