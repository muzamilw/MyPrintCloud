using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

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
        /// Get Phrases By Section Id
        /// </summary>
        /// <returns></returns>
        public void Get([FromUri]int sectionId)
        {
            // return companyService.GetCompanyById(companyId).CreateFrom();
        }
        #endregion
    }
}