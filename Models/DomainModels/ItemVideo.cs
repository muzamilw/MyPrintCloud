namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Item Video Domain Model
    /// </summary>
    public class ItemVideo
    {
        #region Persisted Properties

        /// <summary>
        /// Video Id
        /// </summary>
        public long VideoId { get; set; }

        /// <summary>
        /// Item Id
        /// </summary>
        public long? ItemId { get; set; }

        /// <summary>
        /// Video Link
        /// </summary>
        public string VideoLink { get; set; }

        /// <summary>
        /// Caption
        /// </summary>
        public string Caption { get; set; }

        #endregion

        #region Reference Properties

        /// <summary>
        /// Item 
        /// </summary>
        public virtual Item Item { get; set; }

        #endregion
    }
}
