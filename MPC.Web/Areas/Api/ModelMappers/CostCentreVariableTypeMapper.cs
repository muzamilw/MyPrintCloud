using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostCentreVariableTypeMapper
    {
        public static CostCentreVariableType CreateFrom(this MPC.Models.DomainModels.CostCentreVariableType source)
        {
            return new CostCentreVariableType
            {
                CategoryId = source.CategoryId,
                Name = source.Name,
                VariablesList = source.VariablesList != null ? source.VariablesList.Select(x => x.CreateFrom()).ToList() : null
            };
        }

        public static MPC.Models.DomainModels.CostCentreVariableType CreateFrom(this CostCentreVariableType source)
        {
            return new MPC.Models.DomainModels.CostCentreVariableType
            {
                CategoryId = source.CategoryId,
                Name = source.Name,
                VariablesList = source.VariablesList != null ? source.VariablesList.Select(x => x.CreateFrom()).ToList() : null
            };
        }
    }
}