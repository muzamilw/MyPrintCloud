using System;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Item For Item Job Status Response Model
    /// </summary>
    public class ItemForItemJobStatus
    {
        /// <summary>
        /// Item Id
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// Estimate Id
        /// </summary>
        public long? EstimateId { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Order Code
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Qty1
        /// </summary>
        public int? Qty1 { get; set; }

        /// <summary>
        /// Qty1 Markup Id 1
        /// </summary>
        public double? Qty1NetTotal { get; set; }

        /// <summary>
        /// Status Id
        /// </summary>
        public int? StatusId { get; set; }

        /// <summary>
        /// Expected Shipping Date
        /// </summary>
        public DateTime? JobEstimatedCompletionDateTime { get; set; }

        public DateTime? JobEstimatedStartDateTime { get; set; }
        public int? OrderdItemsCount { get; set; }

        public string OrderFlagColor { get; set; }
        public string OrderFlagTitle { get; set; }
    }
}
