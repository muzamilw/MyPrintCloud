using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cost centre Resource Domain Model
    /// </summary>
    public class CostcentreResource
    {
        public long? CostCentreId { get; set; }
        public Guid? ResourceId { get; set; }
        public int CostCenterResourceId { get; set; }

        public virtual CostCentre CostCentre { get; set; }
    }
}
