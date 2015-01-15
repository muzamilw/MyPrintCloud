using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Phrase Library Save Model
    /// </summary>
    public class PhraseLibrarySaveModel
    {
        public IEnumerable<Section> Sections { get; set; }
    }
}
