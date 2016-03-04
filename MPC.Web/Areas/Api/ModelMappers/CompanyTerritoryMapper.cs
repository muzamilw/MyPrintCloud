﻿using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CompanyTerritoryMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static CompanyTerritory CreateFrom(this MPC.Models.DomainModels.CompanyTerritory source)
        {
            return new CompanyTerritory
            {
                TerritoryId = source.TerritoryId,
                TerritoryName = source.TerritoryName,
                CompanyId = source.CompanyId,
                TerritoryCode = source.TerritoryCode,
                isDefault = source.isDefault,
                IsUseTerritoryColor = source.IsUseTerritoryColor
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static MPC.Models.DomainModels.CompanyTerritory CreateFrom(this CompanyTerritory source)
        {
            var companyTerritory = new MPC.Models.DomainModels.CompanyTerritory
            {
                TerritoryId = source.TerritoryId,
                TerritoryName = source.TerritoryName,
                CompanyId = source.CompanyId,
                TerritoryCode = source.TerritoryCode,
                isDefault = source.isDefault,
                IsUseTerritoryColor = source.IsUseTerritoryColor,
                ScopeVariables = source.ScopeVariables != null ? source.ScopeVariables.Select(ccv => ccv.CreateFrom()).ToList() : null
            };

            return companyTerritory;
        }
    }
}