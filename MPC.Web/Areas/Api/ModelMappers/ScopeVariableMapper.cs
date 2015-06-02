using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{

    /// <summary>
    /// Company Contact Variable Mapper
    /// </summary>
    public static class ScopeVariableMapper
    {
        #region Public
        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.ScopeVariable CreateFrom(this ScopeVariable source)
        {
            return new DomainModels.ScopeVariable
            {
                ScopeVariableId = source.ScopeVariableId,
                Id = source.Id,
                VariableId = source.VariableId,
                Value = source.Value,
                Scope = source.Scope,
                FakeVariableId = source.FakeVariableId,
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ScopeVariable CreateFrom(this DomainModels.ScopeVariable source)
        {
            return new ScopeVariable
            {
                ScopeVariableId = source.ScopeVariableId,
                Id = source.Id,
                FakeVariableId = source.FakeVariableId,
                VariableId = source.VariableId,
                Value = source.Value,
                Scope = source.Scope,
                Type = source.FieldVariable != null ? source.FieldVariable.VariableType : null,
                WaterMark = source.FieldVariable != null ? source.FieldVariable.WaterMark : null,
                Title = source.FieldVariable != null ? source.FieldVariable.VariableTitle : string.Empty,
                VariableOptions = source.FieldVariable != null ? (source.FieldVariable.VariableOptions != null ? source.FieldVariable.VariableOptions.Select(vo => vo.CreateFrom()).ToList()
            : null) : null
            };
        }


        #endregion
    }
}