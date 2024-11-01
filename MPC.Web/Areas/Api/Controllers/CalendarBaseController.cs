﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

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
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCalendar })]
        [CompressFilterAttribute]
        public CalendarBaseResponse Get()
        {
            return calendarService.GetBaseData().CreateFrom();
        }

        /// <summary>
        /// Get Company By Id
        /// </summary>
        /// <returns></returns>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCalendar })]
        [CompressFilterAttribute]
        public IEnumerable<CompanyContactDropDown> Get([FromUri]long companyId)
        {
            return
                calendarService.GetCompanyContactsByCompanyId(companyId).Select(cc => cc.CreateFromDropDown()).ToList();
        }
        #endregion
    }
}