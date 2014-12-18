using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Cost Centre Repository Interface
    /// </summary>
    public interface ICostCentreRepository : IBaseRepository<CostCentre, long>
    {
        /// <summary>
        /// Get All Cost Centres that are not system defined
        /// </summary>
        IEnumerable<CostCentre> GetAllNonSystemCostCentres();
    }
}
