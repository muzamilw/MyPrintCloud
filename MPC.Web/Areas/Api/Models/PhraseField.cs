using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Phrase Field Api Model
    /// </summary>
    public class PhraseField
    {

        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public int? SectionId { get; set; }
        public int? SortOrder { get; set; }
        public long? OrganisationId { get; set; }
        public IEnumerable<Phrase> Phrases { get; set; }
    }
}