namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Purchase Order Search Request Model
    /// </summary>
    public class PurchaseOrderSearchRequestModel : GetPagedListRequest
    {
        /// <summary>
        /// Purchase Order Type
        /// </summary>
        public int PurchaseOrderType { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }
    }
}
