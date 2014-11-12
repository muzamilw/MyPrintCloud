using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Vehicle Interface
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Get By Hire Group
        /// </summary>
        GetVehicleResponse GetByHireGroup(VehicleSearchRequest request);

        /// <summary>
        /// Get By Id
        /// </summary>
        Vehicle GetById(long vehicleId);

        /// <summary>
        /// Get Base Data 
        /// </summary>
        /// <returns></returns>
        VehicleBaseDataResponse GetBaseData();

        /// <summary>
        /// Get Vehicles List Based on search Criteria
        /// </summary>
        /// <returns></returns>
        GetVehicleResponse LoadVehicles(VehicleSearchRequest request);

        /// <summary>
        /// Delete Vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        void DeleteVehicle(Vehicle vehicle);

        /// <summary>
        /// Find By Vehicle Id
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        Vehicle FindById(long vehicleId);

        /// <summary>
        /// Save Vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        Vehicle SaveVehicle(Vehicle vehicle);

        /// <summary>
        /// Get Vehicle Detail By ID
        /// </summary>
        /// <param name="vehicleId">vehicleId</param>
        /// <returns></returns>
        Vehicle GetVehicleDetail(long vehicleId);
    }
}
