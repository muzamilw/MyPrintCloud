
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Designation Service Interface
    /// </summary>
    public interface IDesignationService
    {
       

        /// <summary>
        /// Search Designation
        /// </summary>
        DesignationSearchRequestResponse SearchDesignation(DesignationSearchRequest request);

        /// <summary>
        /// Delete Designation by id
        /// </summary>
        void DeleteDesignation(long designationId);

        /// <summary>
        /// Add /Update Designation
        /// </summary>
        Designation SaveDesignation(Designation designation);

    }
}
