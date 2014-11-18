namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Markup
    /// </summary>
    public class MarkUp
    {
        #region Persisted Properties

        /// <summary>
        /// Markup id
        /// </summary>
        public int MarkUpId { get; set; }

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
        public int SystemSiteId { get; set; }

        /// <summary>
        /// User Domain Key
        /// </summary>
        public int UserDomainKey { get; set; }

        #endregion
    }
}
