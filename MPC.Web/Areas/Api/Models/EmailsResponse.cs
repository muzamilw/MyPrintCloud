using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class EmailsResponse
    {
        public IEnumerable<Campaign> OrganisationEmails { get; set; }
        public List<Section> CampaignSections { get; set; }
        public IEnumerable<EmailEvent> EmailEvents { get; set; }
        
    }
}