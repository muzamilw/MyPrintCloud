using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class SectionFlagController : Controller
    {
        #region Private
        private readonly ISectionFlagsService _sectionflagsService;
        #endregion

        public SectionFlagController(ISectionFlagsService sectionflagservice)
        {
            this._sectionflagsService = sectionflagservice;
        }

        public IEnumerable<SectionFlag> Get()
        {
            IEnumerable<SectionFlag> objlistflags = _sectionflagsService.GetAllSectionFlag().Select(s=>s)
        }
    }
}