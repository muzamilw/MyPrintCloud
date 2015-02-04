
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostCentreVariableMapper
    {
        public static CostCentreVariable CreateFrom(this MPC.Models.DomainModels.CostCentreVariable source)
        {
            return new CostCentreVariable
            {
               VarId = source.VarId,
               Name = source.Name,
               RefTableName = source.RefTableName,
               RefFieldName = source.RefFieldName,
               CriteriaFieldName = source.CriteriaFieldName,
               Criteria = source.Criteria,
               CategoryId = source.CategoryId,
               IsCriteriaUsed = source.IsCriteriaUsed,
               Type = source.Type,
               PropertyType = source.PropertyType,
               VariableDescription = source.VariableDescription,
               VariableValue = source.VariableValue,
            };
        }

        public static MPC.Models.DomainModels.CostCentreVariable CreateFrom(this CostCentreVariable source)
        {
            return new MPC.Models.DomainModels.CostCentreVariable
            {
                VarId = source.VarId,
                Name = source.Name,
                RefTableName = source.RefTableName,
                RefFieldName = source.RefFieldName,
                CriteriaFieldName = source.CriteriaFieldName,
                Criteria = source.Criteria,
                CategoryId = source.CategoryId,
                IsCriteriaUsed = source.IsCriteriaUsed,
                Type = source.Type,
                PropertyType = source.PropertyType,
                VariableDescription = source.VariableDescription,
                VariableValue = source.VariableValue,
            };
        }
    }
}