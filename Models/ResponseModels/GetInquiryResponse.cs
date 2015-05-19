using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class GetInquiryResponse
    {
        /// <summary>
        /// Inquiries
        /// </summary>
        public IEnumerable<Inquiry> Inquiries{ get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
