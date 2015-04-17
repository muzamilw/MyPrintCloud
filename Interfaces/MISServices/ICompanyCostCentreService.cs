using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    ///  Company Cost Centre Service Interface
    /// </summary>
    public interface ICompanyCostCentreService
    {
        /// <summary>
        /// Get Company Cost Centres for Company ID
        /// </summary>
        CostCentreResponse GetCompanyCostCentreByCompanyId(GetCostCentresRequest requestModel);
    }
}
