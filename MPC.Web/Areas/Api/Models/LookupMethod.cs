using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class LookupMethod
    {
        public long MethodId { get; set; }
        public string Name { get; set; }
        public long? Type { get; set; }
        public int? LockedBy { get; set; }
        public int CompanyId { get; set; }
        public int? FlagId { get; set; }
        public int SystemSiteId { get; set; }
    }
}