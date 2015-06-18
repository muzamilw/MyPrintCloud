using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class SectionController : ApiController
    {
        // GET: Api/SectionFlags

        private readonly ISectionService _SectionService;

        public SectionController(ISectionService SectionFlags)
        {

            this._SectionService = SectionFlags;

        }

        public IEnumerable<Section> getSectionLibray()
        {

            return _SectionService.GetSectionsForPhraseLibrary().Select(g => g.CreateFromCampaign()).ToList();

        }
    }
}