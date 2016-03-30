
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Operations Work Place service interfce
    /// </summary>
    public interface IOperationsWorkPlaceService
    {
        /// <summary>
        /// Get Operations Work Place By Workplace Ido
        /// </summary>
        OperationWorkplaceSearchByIdResponse GetOperationsWorkPlaceByWorkplaceId(long workplaceId);
    }
}
