using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Section Api Model
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Section Id
        /// </summary>
        public long SectionId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// Campaign Email Variables
        /// </summary>
        public List<CampaignEmailVariable> CampaignEmailVariables { get; set; }
    }
}