using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Phrase Library Save Api Model
    /// </summary>
    public class PhraseLibrarySaveModel
    {
        public IEnumerable<SectionForPhraseLibrary> Sections { get; set; }
    }
}