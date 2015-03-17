using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
