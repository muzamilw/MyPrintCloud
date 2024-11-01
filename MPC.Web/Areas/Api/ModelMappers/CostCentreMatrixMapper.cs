﻿using System;
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
                RowsCount= source.RowsCount,
                ColumnsCount= source.ColumnsCount,
                VariableString = GetVariableString(source)
            };
        }
        public static CostCentreMatrixDetail CreateFrom(this MPC.Models.DomainModels.CostCentreMatrixDetail source)
        {
            return new CostCentreMatrixDetail
            {
                Id = source.Id,
                MatrixId = source.MatrixId,
                Value = source.Value
               
            };
        }
        public static MPC.Models.DomainModels.CostCentreMatrixDetail CreateFrom(this CostCentreMatrixDetail source)
        {
            return new MPC.Models.DomainModels.CostCentreMatrixDetail
            {
                Id = source.Id,
                MatrixId = source.MatrixId,
                Value = source.Value
            };
        }

        public static MPC.Models.DomainModels.CostCentreMatrix CreateFrom(this CostCentreMatrix source)
        {
            return new MPC.Models.DomainModels.CostCentreMatrix
            {
                MatrixId = source.MatrixId,
                Name = source.Name,
                Description = source.Description,
                RowsCount = source.RowsCount,
                ColumnsCount = source.ColumnsCount
            };
        }

        private static string GetVariableString(MPC.Models.DomainModels.CostCentreMatrix source)
        {
            string sv = "{matrix, ID=&quot;" + source.MatrixId + "&quot;,Name=&quot;" + source.Name + "&quot;}";
            return sv;
        }
    }
}