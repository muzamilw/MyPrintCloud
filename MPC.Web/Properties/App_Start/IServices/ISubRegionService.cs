
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Sub Region Service Interface
    /// </summary>
    public interface ISubRegionService
    {
        /// <summary>
        /// Load Sub Region Base Data
        /// </summary>
        SubRegionBaseDataResponse LoadSubRegionBaseData();

        /// <summary>
        /// Search Sub Region
        /// </summary>
        SubRegionSearchRequestResponse SearchSubRegion(SubRegionSearchRequest request);

        /// <summary>
        /// Delete Sub Region by id
        /// </summary>
        void DeleteSubRegion(long subRegionId);

        /// <summary>
        /// Add /Update sub Region
        /// </summary>
        SubRegion SaveSubRegion(SubRegion subRegion);

    }
}
