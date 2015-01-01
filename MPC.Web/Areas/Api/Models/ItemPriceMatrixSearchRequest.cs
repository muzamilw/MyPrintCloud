namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Price Matrix Search Request WebAPi Model
    /// </summary>
    public class ItemPriceMatrixSearchRequest
    {
        /// <summary>
        /// Item Id
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// Flag Id
        /// </summary>
        public long FlagId { get; set; }
    }
}