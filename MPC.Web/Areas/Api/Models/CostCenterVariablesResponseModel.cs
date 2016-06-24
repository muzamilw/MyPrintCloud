using System.Collections.Generic;
namespace MPC.MIS.Areas.Api.Models
{
   
    public class CostCenterVariablesResponseModel
    {
        public IEnumerable<CostCentreType> CostCenterVariables { get; set; }
        public IEnumerable<CostCentreVariableType> VariableVariables { get; set; }
        public IEnumerable<SystemUserDropDown> ResourceVariables { get; set; }
        public IEnumerable<CostCentreQuestion> QuestionVariables { get; set; }
        public IEnumerable<CostCentreMatrix> MatricesVariables { get; set; }
        public IEnumerable<LookupMethod> LookupVariables { get; set; }
        public IEnumerable<MachineClickChargeZone> ClickChargeZones { get; set; }
        public string StockVariables { get; set; }
        
    }
}
