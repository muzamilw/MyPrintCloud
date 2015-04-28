using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class CompanyCostCentreService : ICompanyCostCentreService
    {
        #region Private
        private readonly ICompanyCostCenterRepository companyCostCenterRepository;
        #endregion
        #region Constructor
        public CompanyCostCentreService(ICompanyCostCenterRepository companyCostCenterRepository)
        {
            this.companyCostCenterRepository = companyCostCenterRepository;
        }
        #endregion
        #region Public
         /// <summary>
        /// Get Company Cost Centres for Company ID
        /// </summary>
        public CostCentreResponse GetCompanyCostCentreByCompanyId(GetCostCentresRequest requestModel)
        {
            return companyCostCenterRepository.GetCompanyCostCentreByCompanyId(requestModel);
        }
        #endregion
       
    }
}
