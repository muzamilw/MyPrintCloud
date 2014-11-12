
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Region Service Interface
    /// </summary>
    public interface IRegionService
    {
        /// <summary>
        /// Load Region Base Data
        /// </summary>
        RegionBaseDataResponse LoadRegionBaseData();

        /// <summary>
        /// Search Region
        /// </summary>
        RegionSearchRequestResponse SearchRegion(RegionSearchRequest request);

        /// <summary>
        /// Delete Region by id
        /// </summary>
        void DeleteRegion(long regionId);

        /// <summary>
        /// Add /Update region
        /// </summary>
        Region SaveRegion(Region region);

    }
}
