using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    /// Sections For Phrase Library Api Controller
    /// </summary>
    public class SectionsForPhraseLibraryController : ApiController
    {
        #region Private

        private readonly IPhraseLibraryService phraseLibraryService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SectionsForPhraseLibraryController(IPhraseLibraryService phraseLibraryService)
        {
            this.phraseLibraryService = phraseLibraryService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get All Sections
        /// </summary>
        public IEnumerable<SectionForPhraseLibrary> Get()
        {
            return phraseLibraryService.GetSections().Select(s => s.CreateFrom());

        }

        /// <summary>
        /// Get Phrases By Phrase Field Id
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Phrase> Get([FromUri]int fieldId)
        {
            return phraseLibraryService.GetPhrasesByPhraseFiledId(fieldId).Select(p => p.CreateFrom());
        }

        [ApiException]
        [HttpPost]
        public int Post(PhraseLibrarySaveModel Sections)
        {
            //FormCollection
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            phraseLibraryService.SavePhaseLibrary(Sections.CreateFrom());
            return 1;
        }


        #endregion
    }
}