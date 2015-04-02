using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostCentreMatrixMapper
    {
        public static CostCentreMatrix CreateFrom(this MPC.Models.DomainModels.CostCentreMatrix source)
        {
            return new CostCentreMatrix
            {
                MatrixId = source.MatrixId,
                Name = source.Name,
                Description = source.Description,
                VariableString = GetVariableString(source)
            };
        }

        public static MPC.Models.DomainModels.CostCentreMatrix CreateFrom(this CostCentreMatrix source)
        {
            return new MPC.Models.DomainModels.CostCentreMatrix
            {
                MatrixId = source.MatrixId,
                Name = source.Name,
                Description = source.Description
            };
        }

        private static string GetVariableString(MPC.Models.DomainModels.CostCentreMatrix source)
        {
            string sv = "{matrix, ID=&quot;" + source.MatrixId + "&quot;,Name=&quot;" + source.Name + "&quot;}";
            return sv;
        }
    }
}