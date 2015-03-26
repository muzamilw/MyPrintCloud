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
                Description = source.Description

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
    }
}