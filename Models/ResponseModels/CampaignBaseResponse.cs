using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Campaign Base Response
    /// </summary>

    public class CampaignBaseResponse
    {
        /// <summary>
        /// Sections
        /// </summary>
        public List<SectionFlag> SectionFlags { get; set; }
        
        /// <summary>
        /// CompanyTypes
        /// </summary>
        public List<CompanyType> CompanyTypes { get; set; }
        
        /// <summary>
        /// Groups
        /// </summary>
        public List<Group> Groups { get; set; }

        /// <summary>
        /// Campaign Sections
        /// </summary>
        public List<Section> CampaignSections { get; set; }
    }
}
