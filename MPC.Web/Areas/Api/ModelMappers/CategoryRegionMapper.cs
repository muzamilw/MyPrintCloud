using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.Common;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// CategoryRegion Mapper
    /// </summary>
    public static class CategoryRegionMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static CategoryRegion CreateFrom(this DomainModels.CategoryRegion source)
        {
            return new CategoryRegion
            {
                RegionId = source.RegionId,
                RegionName = source.RegionName
            };
        }
        
    }
}