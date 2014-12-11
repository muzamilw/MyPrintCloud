namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item RelatedItem WebApi Model
    /// </summary>
    public class ItemRelatedItem
    {
        #region Public

        /// <summary>
        /// Unique Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Related Item Id
        /// </summary>
        public long? RelatedItemId { get; set; }
        
        /// <summary>
        /// Item Id
        /// </summary>
        public long? ItemId { get; set; }

        /// <summary>
        /// Related Item Name
        /// </summary>
        public string RelatedItemName { get; set; }

        /// <summary>
        /// Related Item Code
        /// </summary>
        public string RelatedItemCode { get; set; }

        #endregion
    }
}