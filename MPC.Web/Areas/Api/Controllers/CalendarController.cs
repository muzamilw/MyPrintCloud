using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Calendar API Controller
    /// </summary>
    public class CalendarController : ApiController
    {
        #region Private

        private readonly ICalendarService calendarService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CalendarController(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }

        #endregion

        #region Public

        ///// <summary>
        ///// Get Calendar Base Data
        ///// </summary>
        //public CalendarBaseResponse Get()
        //{
        //    return calendarService.GetBaseData().CreateFrom();
        //}

        /// <summary>
        /// Get Companies By Is Customer Type
        /// </summary>
        public CompanySearchResponseForCalendar Get([FromUri]CompanyRequestModelForCalendar request)
        {
            var response = calendarService.GetCompaniesByCustomerType(request);
            return new CompanySearchResponseForCalendar
            {
                Companies = response.Companies.Select(c => c.CreateFromForCalendar()),
                TotalCount = response.TotalCount
            };
        }
        #endregion
    }
}