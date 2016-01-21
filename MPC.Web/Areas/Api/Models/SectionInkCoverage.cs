using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class SectionInkCoverage
    {
        public int Id { get; set; }
        public long? SectionId { get; set; }
        public int? InkOrder { get; set; }
        public int? InkId { get; set; }
        public int? CoverageGroupId { get; set; }
        public double? CoverageRate { get; set; }
        public int? Side { get; set; }
    }
}