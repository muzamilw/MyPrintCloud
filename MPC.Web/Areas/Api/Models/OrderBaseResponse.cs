using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Order Base Response
    /// </summary>
    public class OrderBaseResponse
    {
        /// <summary>
        /// Section Flags
        /// </summary>
        public IEnumerable<SectionFlagDropDown> SectionFlags { get; set; }

        /// <summary>
        /// System Users
        /// </summary>
        public IEnumerable<SystemUserDropDown> SystemUsers { get; set; }

        /// <summary>
        /// Pipeline Sources
        /// </summary>
        public IEnumerable<PipeLineSource> PipeLineSources { get; set; }
    }
}
