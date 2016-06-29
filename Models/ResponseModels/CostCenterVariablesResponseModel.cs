using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class CostCenterVariablesResponseModel
    {
        public IEnumerable<CostCentreType> CostCenterVariables { get; set; }
        public IEnumerable<CostCentreVariableType> VariableVariables { get; set; }
        public IEnumerable<SystemUser> ResourceVariables { get; set; }
        public IEnumerable<CostCentreQuestion> QuestionVariables { get; set; }
        public IEnumerable<CostCentreMatrix> MatricesVariables { get; set; }
        public IEnumerable<LookupMethod> LookupVariables { get; set; }
        public IEnumerable<MachineClickChargeZone> ClickChargeZones { get; set; }
        public string StockVariables { get; set; }
    }
}
