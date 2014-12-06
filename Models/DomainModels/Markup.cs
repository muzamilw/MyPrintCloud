using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Markup
    /// </summary>
    public class Markup
    {
        #region Persisted Properties

        /// <summary>
        /// Markup id
        /// </summary>
        public long MarkUpId { get; set; }

        /// <summary>
        /// Markup Name
        /// </summary>
        public string MarkUpName { get; set; }

        /// <summary>
        /// Markup Rate
        /// </summary>
        public double? MarkUpRate { get; set; }

        /// <summary>
        /// Is Fixed
        /// </summary>
        public int? IsFixed { get; set; }

        /// <summary>
        /// System Site Id
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// User Domain Key
        /// </summary>
        public int UserDomainKey { get; set; }

        #endregion

        #region Reference Properties

        /// <summary>
        /// Prefixes
        /// </summary>
        public virtual ICollection<Prefix> Prefixes { get; set; }

        #endregion
    }
}
