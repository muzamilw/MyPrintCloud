using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class InquiryBaseResponse
    {
        /// <summary>
        /// Section Flags
        /// </summary>
        public IEnumerable<SectionFlagDropDown> SectionFlags { get; set; }
    }
}