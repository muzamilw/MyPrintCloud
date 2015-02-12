using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Calendar Base Api Controller
    /// </summary>
    public class CalendarBaseController : ApiController
    {
        #region Private

        private readonly ICalendarService calendarService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CalendarBaseController(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Calendar Base Data
        /// </summary>
        public CalendarBaseResponse Get()
        {
            return calendarService.GetBaseData().CreateFrom();
        }

        #endregion
    }
}