using System.Collections.Generic;
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Additional Driver Service
    /// </summary>
    public interface IAdditionalDriverService
    {
        /// <summary>
        /// Load Base Data
        /// </summary>
        AdditionalDriverBaseResponse GetBaseData();

        /// <summary>
        /// Load Additional Driver Charge Based on search criteria
        /// </summary>
        /// <returns></returns>
        AdditionalDriverChargeSearchResponse LoadAll(AdditionalDriverChargeSearchRequest request);

        /// <summary>
        /// Get Additional Driver Charge Detail
        /// </summary>
        /// <param name="additionalDriverChargeId"></param>
        /// <returns></returns>
        IEnumerable<AdditionalDriverCharge> GetAdditionalDriverChargeDetail(long additionalDriverChargeId);

        /// <summary>
        /// Add Additional Driver Charge
        /// </summary>
        /// <param name="additionalDriverCharge"></param>
        /// <returns></returns>
        AdditionalDriverChargeSearchContent SaveAdditionalDriverCharge(AdditionalDriverCharge additionalDriverCharge);

        /// <summary>
        /// Additional Driver Charge Delete
        /// </summary>
        /// <param name="additionalDriverCharge"></param>
        void AdditionalDriverChargeDelete(AdditionalDriverCharge additionalDriverCharge);

        /// <summary>
        /// Find By Id
        /// </summary>
        /// <param name="additionalDriverChargeId"></param>
        /// <returns></returns>
       AdditionalDriverCharge FindById(long additionalDriverChargeId);
    }
}
