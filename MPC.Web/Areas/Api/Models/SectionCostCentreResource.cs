using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Section Cost Center Resource WebApi Model
    /// </summary>
    public class SectionCostCentreResource
    {
        public int SectionCostCentreResourceId { get; set; }
        public int? SectionCostcentreId { get; set; }
        public Guid? ResourceId { get; set; }
        public int? ResourceTime { get; set; }
        public short? IsScheduleable { get; set; }
        public short? IsScheduled { get; set; }
    }
}