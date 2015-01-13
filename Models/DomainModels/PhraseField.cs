using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Phrase Field Domain Model
    /// </summary>
    public class PhraseField
    {
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public int? SectionId { get; set; }
        public int? SortOrder { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<Phrase> Phrases { get; set; }
    }
}
