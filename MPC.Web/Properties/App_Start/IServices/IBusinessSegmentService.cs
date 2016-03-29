
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Business Segment Service Interface
    /// </summary>
    public interface IBusinessSegmentService
    {
        /// <summary>
        /// Search Business Segment
        /// </summary>
        BusinessSegmentSearchRequestResponse SearchBusinessSegment(BusinessSegmentSearchRequest request);

        /// <summary>
        /// Delete BusinessSegment by BusinessSegmentId
        /// </summary>
        void DeleteBusinessSegment(long businessSegmentId);

        /// <summary>
        /// Add or Update BusinessSegment objesct
        /// </summary>
        BusinessSegment AddUpdateBusinessSegment(BusinessSegment businessSegment);
    }
}
