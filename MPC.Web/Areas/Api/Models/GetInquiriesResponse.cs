using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class GetInquiriesResponse
    {
        /// <summary>
        /// Inquiries
        /// </summary>
        public IEnumerable<InquiryListView> Inquiries { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}