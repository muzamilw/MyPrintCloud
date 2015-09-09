using ApiModel=MPC.MIS.Areas.Api.Models;
using DomainModel=MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ReportparamMapper
    {
        public static ApiModel.Reportparam CreateFrom(this DomainModel.Reportparam source){

            return new ApiModel.Reportparam {

                ParmId = source.ParmId,
                ParmName = source.ParmName,
                Caption1 = source.Caption1,
                ReportId = source.ReportId,
                ControlType = source.ControlType ,
                ComboTableName = source.ComboTableName ,
                ComboIDFieldName = source.ComboIDFieldName ,
                ComboTextFieldName = source.ComboTextFieldName ,
                CriteriaFieldName = source.CriteriaFieldName ,
                OrderByFieldName = source.OrderByFieldName ,
                SameAsPArmId = source.SameAsPArmId ,
                Caption2 = source.Caption2 ,
                Operator = source.Operator ,
                LogicalOperator = source.LogicalOperator ,
                DefaultValue1 = source.DefaultValue1 ,
                DefaultValue2 = source.DefaultValue2 ,
                MinValue = source.MinValue ,
                MaxValue = source.MaxValue ,
                FilterType = source.FilterType ,
                SortOrder = source.SortOrder 


            };

        }
        public static DomainModel.Reportparam CreateFrom(this ApiModel.Reportparam source)
        {

            return new DomainModel.Reportparam
            {

                ParmId = source.ParmId,
                ParmName = source.ParmName,
                Caption1 = source.Caption1,
                ReportId = source.ReportId,
                ControlType = source.ControlType,
                ComboTableName = source.ComboTableName,
                ComboIDFieldName = source.ComboIDFieldName,
                ComboTextFieldName = source.ComboTextFieldName,
                CriteriaFieldName = source.CriteriaFieldName,
                OrderByFieldName = source.OrderByFieldName,
                SameAsPArmId = source.SameAsPArmId,
                Caption2 = source.Caption2,
                Operator = source.Operator,
                LogicalOperator = source.LogicalOperator,
                DefaultValue1 = source.DefaultValue1,
                DefaultValue2 = source.DefaultValue2,
                MinValue = source.MinValue,
                MaxValue = source.MaxValue,
                FilterType = source.FilterType,
                SortOrder = source.SortOrder


            };

        }
    }
}