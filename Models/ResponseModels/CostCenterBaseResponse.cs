using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    
    public class CostCenterBaseResponse
    {
        public IEnumerable<SystemUser> CostCenterResources { get; set; }
        public IEnumerable<Markup> Markups { get; set; }
        public IEnumerable<ChartOfAccount> NominalCodes { get; set; }
        public IEnumerable<CostCentreType> CostCenterCategories { get; set; }
        public IEnumerable<CostCentreVariable> CostCentreVariables { get; set; }
        public IEnumerable<DeliveryCarrier> DeliveryCarriers { get; set; }
    }
}
