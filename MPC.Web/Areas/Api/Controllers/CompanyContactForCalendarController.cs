using System.Collections.Generic;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Company Contact For Calendar API Controller
    /// </summary>
    public class CompanyContactForCalendarController : ApiController
    {
        #region Private

        private readonly ICalendarService calendarService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyContactForCalendarController(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }

        #endregion

        #region Public

        public CompanyContactResponseForCalendar Get([FromUri]CompanyContactForCalendarRequestModel request)
        {
            return calendarService.GetCompanyContacts(request).CreateFromForCalendar();
        }


        #endregion
    }
}