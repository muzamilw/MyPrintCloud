using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class EmailsResponse
    {
        public IEnumerable<Campaign> OrganisationEmails { get; set; }
        public List<Section> CampaignSections { get; set; }
        public IEnumerable<EmailEvent> EmailEvents { get; set; }
    }
}
