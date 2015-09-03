using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class MarketingBriefHistory
    {
        public long MarketingBriefHistoryId { get; set; }
        public string HtmlMsg { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> OrganisationId { get; set; }
        public Nullable<long> ContactId { get; set; }
        public Nullable<long> ItemId { get; set; }
    }
}
