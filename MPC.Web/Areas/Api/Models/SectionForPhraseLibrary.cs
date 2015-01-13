using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Section For Phrase Library
    /// </summary>
    public class SectionForPhraseLibrary
    {
        /// <summary>
        /// Section Id
        /// </summary>
        public int SectionId { get; set; }

        /// <summary>
        /// Section Name
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// Parent Id
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Child Sections
        /// </summary>
        public List<SectionForPhraseLibrary> ChildSections { get; set; }
    }
}