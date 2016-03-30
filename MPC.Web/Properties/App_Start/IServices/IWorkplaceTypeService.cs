
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Work Place Type Service Interface
    /// </summary>
    public interface IWorkplaceTypeService
    {

        /// <summary>
        /// Search Workplace Type
        /// </summary>
        WorkPlaceTypeSearchRequestResponse SearchWorkplaceType(WorkplaceTypeSearchRequest request);

        /// <summary>
        /// Delete Workplace Type
        /// </summary>
        void DeleteWorkPlaceType(long workPlaceTypeId);


        /// <summary>
        /// Add / Update WorkPlaceType
        /// </summary>
        WorkPlaceType AddUpdateWorkPlaceType(WorkPlaceType workPlaceType);
    }
}
