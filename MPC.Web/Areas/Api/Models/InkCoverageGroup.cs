using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class InkCoverageGroup
    {
        public int CoverageGroupId { get; set; }
        public string GroupName { get; set; }
        public double? Percentage { get; set; }
        public int IsFixed { get; set; }
        public int SystemSiteId { get; set; }
    }
}