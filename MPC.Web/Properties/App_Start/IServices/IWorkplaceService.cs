using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Workplace Service Interface
    /// </summary>
    public interface IWorkplaceService
    {
        /// <summary>
        /// Load Workplace BaseData
        /// </summary>
        WorkplaceBaseDataResponse LoadWorkplaceBaseData();

        /// <summary>
        /// Search Workplace
        /// </summary>
        WorkplaceSearchRequestResponse SearchWorkplace(WorkplaceSearchRequest request);

        /// <summary>
        /// Save WorkPlace
        /// </summary>
        WorkPlace SaveWorkPlace(WorkPlace workPlaceRequest);


        /// <summary>
        /// Delete WorkPlace
        /// </summary>
        void DeleteWorkPlace(long workPlaceId);
    }
}
