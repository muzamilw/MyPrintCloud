using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
using DomainReponse = MPC.Models.ResponseModels;

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
                IsSystem = source.IsSystem,
                VariableOptions = source.VariableOptions != null ? source.VariableOptions.Select(vo => vo.CreateFrom()).ToList() : null,
                VariableExtensions = source.VariableExtensions != null ? source.VariableExtensions.Select(vo => vo.CreateFrom()).ToList() : new List<VariableExtension>()
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
                FakeIdVariableId = source.FakeIdVariableId,
                VariableName = source.VariableName != null ? source.VariableName.Trim() : null,
                DefaultValue = source.DefaultValue,
                Scope = source.Scope,
                VariableTag = source.VariableTag != null ? source.VariableTag.Trim() : null,
                VariableTitle = source.VariableTitle,
                VariableType = source.VariableType,
                WaterMark = source.WaterMark,
                VariableOptions = source.VariableOptions != null ? source.VariableOptions.Select(vo => vo.CreateFrom()).ToList() : null,
                VariableExtensions = source.VariableExtensions != null ? source.VariableExtensions.Select(vo => vo.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static FieldVariable CreateFromListView(this DomainModels.FieldVariable source)
        {
            return new FieldVariable
            {
                VariableId = source.VariableId,
                VariableName = source.VariableName,
                ScopeName = ScopeName(source.Scope),
                VariableTag = source.VariableTag,
                TypeName = source.VariableType == 1 ? "Dropdown" : "Input",
            };
        }
        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static FieldVariableForSmartForm CreateFromForSmartForm(this DomainModels.FieldVariable source)
        {
            return new FieldVariableForSmartForm
            {
                VariableId = source.VariableId,
                VariableName = source.VariableName,
                ScopeName = ScopeName(source.Scope),
                VariableTag = source.VariableTag,
                Type = source.VariableType,
                DefaultValue = source.DefaultValue,
                WaterMark = source.WaterMark,
                VariableTitle = source.VariableTitle,
                Scope = source.Scope,
                TypeName = source.VariableType == 1 ? "Dropdown" : "Input",
            };
        }

        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static FieldVariableResponse CreateFrom(this DomainReponse.FieldVariableResponse source)
        {
            return new FieldVariableResponse
            {
                FieldVariables = source.FieldVariables != null ? source.FieldVariables.Select(vf => vf.CreateFromListView()) : new List<FieldVariable>(),
                RowCount = source.RowCount
            };
        }

        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static SystemVariablesResponse CreateFromForSytemVariables(this DomainReponse.FieldVariableResponse source)
        {
            return new SystemVariablesResponse
            {
                SystemVariables = source.FieldVariables != null ? source.FieldVariables.Select(vf => vf.CreateFromSytemVariableListView()) : new List<SystemVariableForListView>(),
                RowCount = source.RowCount
            };
        }

        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static SystemVariableForListView CreateFromSytemVariableListView(this DomainModels.FieldVariable source)
        {
            return new SystemVariableForListView
            {
                VariableId = source.VariableId,
                VariableName = source.VariableName,
                ScopeName = ScopeName(source.Scope),
                VariableTag = source.VariableTag,
                TypeName = source.VariableType == 1 ? "Dropdown" : "Input",
            };
        }
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ScopeVariable CreateFromFieldVariable(this DomainModels.FieldVariable source)
        {
            return new ScopeVariable
            {

                ScopeVariableId = 0,
                Id = 0,
                VariableId = source.VariableId,
                Value = source.DefaultValue,
                Type = source.VariableType,
                Title = source.VariableTitle,
                Scope = source.Scope,
                VariableOptions = source.VariableOptions != null ? source.VariableOptions.Select(vo => vo.CreateFrom()).ToList() : null
            };
        }
        #endregion

        #region Private

        /// <summary>
        /// Get Scope Name
        /// </summary>
        private static string ScopeName(int? scope)
        {
            switch (scope)
            {
                case 1:
                    return "Store";
                case 2:
                    return "Contact";
                case 3:
                    return "Address";
                case 4:
                    return "Territory";
                case 7:
                    return "System Store";
                case 8:
                    return "System Contact";
                case 9:
                    return "System Address";
                case 10:
                    return "System Territory";
            }
            return string.Empty;
        }
        #endregion
    }
}