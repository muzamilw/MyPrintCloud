﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Antlr.Runtime;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostCenterBaseResponseMapper
    {
        public static CostCenterBaseResponse CreateFrom(this MPC.Models.ResponseModels.CostCenterBaseResponse source)
        {
            List<CostCenterCalculationTypes> calType = new List<CostCenterCalculationTypes>();
            //calType.Add(new CostCenterCalculationTypes{TypeId = 1, TypeName = "Fixed"});
            calType.Add(new CostCenterCalculationTypes { TypeId = 3, TypeName = "Per Quantity Cost" });
            calType.Add(new CostCenterCalculationTypes { TypeId = 4, TypeName = "Formula String Cost" });
            calType.Add(new CostCenterCalculationTypes { TypeId = 2, TypeName = "Per Hour Cost" });
            return new CostCenterBaseResponse
            {
                CostCenterResources = source.CostCenterResources != null? source.CostCenterResources.Select(s => s.CreateFrom()): new List<SystemUserDropDown>(),
                Markups = source.Markups != null? source.Markups.Select(s => s.CreateFrom()): new List<Markup>(),
                NominalCodes = source.NominalCodes != null ? source.NominalCodes.Select(o => o.CreateFrom()) : new  List<ChartOfAccount>(),
                CostCenterCategories = source.CostCenterCategories != null ? source.CostCenterCategories.Select(c => c.CreateFrom()) : new List<CostCentreType>(),
                CostCentreVariables = source.CostCentreVariables != null? source.CostCentreVariables.Select(c => c.CreateFrom()): new List<CostCentreVariable>(),
                CalculationTypes = calType,
                DeliveryCarriers= source.DeliveryCarriers.Select(s=>s.CreateFrom()),
                CurrencySymbol = source.CurrencySymbol == null ? null : source.CurrencySymbol
                
            };

        }
    }
}