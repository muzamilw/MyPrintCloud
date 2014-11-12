using System.Collections.Generic;
using Cares.Models.DomainModels;
using Cares.Models.RequestModels;
using Cares.Models.ResponseModels;

namespace Cares.Interfaces.IServices
{
    /// <summary>
    /// Chauffer Charge Service Interface
    /// </summary>
    public interface IChaufferChargeService
    {
        /// <summary>
        /// Load Chauffer Charge Base data
        /// </summary>
        ChaufferChargeBaseResponse GetBaseData();

        /// <summary>
        /// Load Chauffer Charge Main  Based on search criteria
        /// </summary>
        /// <returns></returns>
        ChaufferChargeSearchResponse LoadAll(ChaufferChargeSearchRequest request);

        /// <summary>
        /// Save Chauffer Charge
        /// </summary>
        /// <param name="chaufferCharge"></param>
        /// <returns></returns>
        ChaufferChargeMainContent SaveChaufferCharge(ChaufferChargeMain chaufferCharge);

        /// <summary>
        /// Delete Chauffer Charge
        /// </summary>
        /// <param name="chaufferChargeMain"></param>
        void DeleteAdditionalCharge(ChaufferChargeMain chaufferChargeMain);

        /// <summary>
        /// Find Chauffer Charge By Id
        /// </summary>
        /// <param name="chaufferChargeMainId"></param>
        /// <returns></returns>
        ChaufferChargeMain FindById(long chaufferChargeMainId);

        /// <summary>
        /// Get Chauffer Charges By Chauffer Charge Main Id
        /// </summary>
        /// <param name="chaufferChargeMainId"></param>
        /// <returns></returns>
        IEnumerable<ChaufferCharge> GetChaufferChargesByChaufferChargeMainId(long chaufferChargeMainId);
    }
}
