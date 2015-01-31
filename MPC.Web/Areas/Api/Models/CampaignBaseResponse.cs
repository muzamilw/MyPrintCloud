using System.Collections.Generic;
using MPC.Models.Common;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Campaign Base Response
    /// </summary>
    public class CampaignBaseResponse
    {
        /// <summary>
        /// Sections
        /// </summary>
        public List<SectionFlagDropDown> SectionFlags { get; set; }

        /// <summary>
        /// CompanyTypes
        /// </summary>
        public List<CompanyType> CompanyTypes { get; set; }

        /// <summary>
        /// Groups
        /// </summary>
        public List<GroupForCampaign> Groups { get; set; }

        /// <summary>
        /// Campaign Sections
        /// </summary>
        public List<Section> CampaignSections { get; set; }
    }
}