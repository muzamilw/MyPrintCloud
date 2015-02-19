using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// SectionCostCentreResource Mapper
    /// </summary>
    public static class SectionCostCentreResourceMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static SectionCostCentreResource CreateFrom(this DomainModels.SectionCostCentreResource source)
        {
            return new SectionCostCentreResource
            {
                SectionCostCentreResourceId = source.SectionCostCentreResourceId,
                ResourceId = source.ResourceId,
                SectionCostcentreId = source.SectionCostcentreId,
                ResourceTime = source.ResourceTime,
                IsScheduleable = source.IsScheduleable,
                IsScheduled = source.IsScheduled
            };
        }

        /// <summary>
        /// Create From WebApi Model
        /// </summary>
        public static DomainModels.SectionCostCentreResource CreateFrom(this SectionCostCentreResource source)
        {
            return new DomainModels.SectionCostCentreResource
            {
                SectionCostCentreResourceId = source.SectionCostCentreResourceId
            };
        }

    }
}