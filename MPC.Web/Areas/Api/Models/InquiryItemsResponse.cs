using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class InquiryItemsResponse
    {
        /// <summary>
        /// Inquiries
        /// </summary>
        public IEnumerable<InquiryItem> InquiryItems { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}