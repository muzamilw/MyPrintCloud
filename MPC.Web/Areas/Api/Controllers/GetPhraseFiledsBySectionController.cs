using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Phrase Fileds By Section Api Controller
    /// </summary>
    public class GetPhraseFiledsBySectionController : ApiController
    {
        #region Private

        private readonly IPhraseLibraryService phraseLibraryService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetPhraseFiledsBySectionController(IPhraseLibraryService phraseLibraryService)
        {
            this.phraseLibraryService = phraseLibraryService;
        }

        #endregion

        #region Public



        /// <summary>
        /// Get Phrase Fields By Section Id
        /// </summary>
        public IEnumerable<PhraseField> Get([FromUri]long sectionId)
        {
            return phraseLibraryService.GetPhraseFiledsBySectionId(sectionId).Select(pf => pf.CreateFrom());
        }



        #endregion
    }
}