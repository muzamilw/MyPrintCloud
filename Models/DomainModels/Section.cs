using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Section Domain Model
    /// </summary>
    public class Section
    {
        #region Persisted Properties
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
        /// Sec Order
        /// </summary>
        public int SecOrder { get; set; }

        /// <summary>
        /// Href
        /// </summary>
        public string href { get; set; }

        /// <summary>
        /// Section Image
        /// </summary>
        public string SectionImage { get; set; }

        /// <summary>
        /// Independent
        /// </summary>
        public bool Independent { get; set; }

        #endregion

        #region Reference Properties
        /// <summary>
        /// Section Flags
        /// </summary>
        public virtual ICollection<SectionFlag> SectionFlags { get; set; }

        /// <summary>
        /// Phrase Fields
        /// </summary>
        public virtual ICollection<PhraseField> PhraseFields { get; set; }
        #endregion

    }
}
