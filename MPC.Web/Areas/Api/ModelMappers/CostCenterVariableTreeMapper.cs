using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Antlr.Runtime;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostCenterVariableTreeMapper
    {
        public static CostCenterVariablesResponseModel CreateFrom(this MPC.Models.ResponseModels.CostCenterVariablesResponseModel source)
        {
            return new CostCenterVariablesResponseModel
            {
                 CostCenterVariables = source.CostCenterVariables != null? source.CostCenterVariables.Select(s => s.CreateFrom()): new List<CostCentreType>(),
                 VariableVariables = source.VariableVariables != null ? source.VariableVariables.Select(s => s.CreateFrom()) : new List<CostCentreVariableType>(),
                 ResourceVariables = source.ResourceVariables != null ? source.ResourceVariables.Select(s => s.CreateFrom()) : new List<SystemUserDropDown>(),
                 QuestionVariables = source.QuestionVariables != null ? source.QuestionVariables.Select(s => s.CreateFrom()) : new List<CostCentreQuestion>(),
                 MatricesVariables = source.MatricesVariables != null ? source.MatricesVariables.Select(s => s.CreateFrom()) : new List<CostCentreMatrix>(),
                 LookupVariables = source.LookupVariables != null ? source.LookupVariables.Select(s => s.CreateFrom()) : new List<LookupMethod>(),
                 ClickChargeZones = source.ClickChargeZones != null? source.ClickChargeZones.Select(s => s.CreateFrom()) : new LinkedList<MachineClickChargeZone>(),
                 StockVariables = "Stock Items"
                
            };

        }
    }
}