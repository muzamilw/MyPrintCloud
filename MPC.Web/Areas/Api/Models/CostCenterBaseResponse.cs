using System.Collections.Generic;
using MPC.Models.Common;

namespace MPC.MIS.Areas.Api.Models
{
    public class CostCenterBaseResponse
    {
        public IEnumerable<SystemUserDropDown> CostCenterResources { get; set; }
        public IEnumerable<Markup> Markups { get; set; }
        public IEnumerable<ChartOfAccount> NominalCodes { get; set; }
        public IEnumerable<CostCenterCalculationTypes> CalculationTypes { get; set; }
        public IEnumerable<CostCentreType> CostCenterCategories { get; set; }
        public IEnumerable<CostCentreVariable> CostCentreVariables { get; set; }
        public IEnumerable<DeliveryCarrier> DeliveryCarriers { get; set; }

        public string CurrencySymbol { get; set; }

    }
}