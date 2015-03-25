using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Companies For Calendar Controller
    /// </summary>
    public class GetCompaniesForCalendarController : ApiController
    {
        #region Private

        private readonly ICalendarService calendarService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetCompaniesForCalendarController(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }

        #endregion

        #region Public


        /// <summary>
        /// Get Companies By Is Customer Type
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCalendar })]
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