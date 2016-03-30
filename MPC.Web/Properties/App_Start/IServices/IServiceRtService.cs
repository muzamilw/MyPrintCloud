using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Service Rate Service Interface
    /// </summary>
    public interface IServiceRtService
    {
        /// <summary>
        /// Load Service Rate bsed on search criteria
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ServiceRateSearchResponse LoadServiceRates(ServiceRateSearchRequest request);

        /// <summary>
        /// Get Base Data
        /// </summary>
        /// <returns></returns>
        ServiceRateBaseResponse GetBaseData();

        /// <summary>
        ///Get Service Rate Detail 
        /// </summary>
        /// <returns>Service Rate Detail Response</returns>
        ServiceRtDetailResponse GetServiceRtDetail(long serviceRtMainId);

        /// <summary>
        /// Delete Service Rate
        /// </summary>
        /// <param name="serviceRtMain"></param>
        void DeleteServiceRate(ServiceRtMain serviceRtMain);

        /// <summary>
        /// Get Service Rate By ID
        /// </summary>
        /// <param name="serviceRtMainId"></param>
        /// <returns></returns>
        ServiceRtMain FindById(long serviceRtMainId);

        /// <summary>
        /// Add/Edit Service Rate
        /// </summary>
        /// <param name="serviceRtMain"></param>
        /// <returns></returns>
        ServiceRtMainContent SaveInsuranceRate(ServiceRtMain serviceRtMain);
    }
}
