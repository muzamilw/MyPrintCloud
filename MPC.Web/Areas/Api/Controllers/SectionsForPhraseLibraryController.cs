using System.Web.Http;
using MPC.Interfaces.MISServices;

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
        public void Get()
        {
            phraseLibraryService.GetSections();

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