using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CampaignImage
    {
        public long CampaignImageId { get; set; }
        public Nullable<long> CampaignId { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public virtual Campaign Campaign { get; set; }
    }
}
