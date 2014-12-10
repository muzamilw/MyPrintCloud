namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Video WebApi Model
    /// </summary>
    public class ItemVideo
    {
        #region Public

        /// <summary>
        /// Video Id
        /// </summary>
        public long VideoId { get; set; }

        /// <summary>
        /// Video Link
        /// </summary>
        public string VideoLink { get; set; }

        /// <summary>
        /// Caption
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Item Id
        /// </summary>
        public long? ItemId { get; set; }

        #endregion
    }
}