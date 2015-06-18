using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Section Flag Model To update Data
    /// </summary>
    public class SectionFlagUpdateModel
    {
        /// <summary>
        /// Section Flags
        /// </summary>
        public IEnumerable<SectionFlag> SectionFlags { get; set; } 
    }
}