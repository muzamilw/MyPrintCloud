using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Field Variable Mapper
    /// </summary>
    public static class FieldVariableMapper
    {
        #region
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static FieldVariable CreateFrom(this DomainModels.FieldVariable source)
        {
            return new FieldVariable
            {
                VariableId = source.VariableId,
                CompanyId = source.CompanyId,
                InputMask = source.InputMask,
                VariableName = source.VariableName,
                DefaultValue = source.DefaultValue,
                Scope = source.Scope,
                VariableTag = source.VariableTag,
                VariableTitle = source.VariableTitle,
                VariableType = source.VariableType,
                WaterMark = source.WaterMark,
                VariableOptions = source.VariableOptions != null ? source.VariableOptions.Select(vo => vo.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.FieldVariable CreateFrom(this FieldVariable source)
        {
            return new DomainModels.FieldVariable
            {
                VariableId = source.VariableId,
                CompanyId = source.CompanyId,
                InputMask = source.InputMask,
                VariableName = source.VariableName,
                DefaultValue = source.DefaultValue,
                Scope = source.Scope,
                VariableTag = source.VariableTag,
                VariableTitle = source.VariableTitle,
                VariableType = source.VariableType,
                WaterMark = source.WaterMark,
                VariableOptions = source.VariableOptions != null ? source.VariableOptions.Select(vo => vo.CreateFrom()).ToList() : null
            };
        }
        #endregion
    }
}