
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Service Type Service Interface
    /// </summary>
    public interface IServiceTypeService
    {
        /// <summary>
        /// Search Service Type 
        /// </summary>
        ServiceTypeSearchRequestResponse SearchServiceType(ServiceTypeSearchRequest request);

        /// <summary>
        /// Delete Service Type by id
        /// </summary>
        void DeleteServiceType(long serviceTypeId);

        /// <summary>
        /// Add /Update Service Type
        /// </summary>
        ServiceType SaveServiceType(ServiceType serviceType);

    }
}
