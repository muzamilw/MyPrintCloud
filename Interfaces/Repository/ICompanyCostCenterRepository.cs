using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Company Cost Centre Repository
    /// </summary>
    public interface ICompanyCostCenterRepository : IBaseRepository<CompanyCostCentre, long>
    {
         /// <summary>
        /// Get Company Cost Centres for Company ID
        /// </summary>
        CostCentreResponse GetCompanyCostCentreByCompanyId(GetCostCentresRequest requestModel);
    }
}
