using System;
namespace MPC.MIS.Areas.Api.Models
{
    public class CostcentreResource
    {
        public long? CostCentreId { get; set; }
        public Guid? ResourceId { get; set; }
        public int CostCenterResourceId { get; set; }
        public virtual CostCentre CostCentre { get; set; }
    }
}