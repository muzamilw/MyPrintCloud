using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Work Location Service interface
    /// </summary>
    public interface IWorkLocationService
    {
        /// <summary>
        /// Load WorkLocation BaseData
        /// </summary>
        WorkLocationBaseDataResponse LoadWorkLocationBaseData();

     
        /// <summary>
        /// Search Work Locations
        /// </summary>
        WorkLocationSerachRequestResponse SearchWorkLocation(WorkLocationSearchRequest request);


        /// <summary>
        /// Delete Work Location
        /// </summary>
        void DeleteWorkLocation(long workLocationId);


        /// <summary>
        /// Save / Update Work Location
        /// </summary>
        WorkLocation SaveWorkLocation(WorkLocation workLocationrequest);

        
    }
}
