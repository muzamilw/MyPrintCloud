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
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore, SecurityAccessRight.CanViewProduct, SecurityAccessRight.CanViewSecurity })]
        public IEnumerable<PhraseField> Get([FromUri]long sectionId)
        {
            return phraseLibraryService.GetPhraseFiledsBySectionId(sectionId).Select(pf => pf.CreateFrom());
        }



        #endregion
    }
}