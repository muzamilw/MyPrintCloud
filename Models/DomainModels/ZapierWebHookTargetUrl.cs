using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ZapierWebHookTargetUrl
    {
        public long ZapierTargetUrlId { get; set; }
        public string TargetUrl { get; set; }
        public int? WebHookEvent { get; set; }
    }
}
