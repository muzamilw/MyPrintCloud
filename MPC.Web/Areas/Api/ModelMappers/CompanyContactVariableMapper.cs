using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{

    /// <summary>
    /// Company Contact Variable Mapper
    /// </summary>
    public static class CompanyContactVariableMapper
    {
        #region Public
        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.ScopeVariable CreateFrom(this CompanyContactVariable source)
        {
            return new DomainModels.ScopeVariable
            {
                ScopeVariableId = source.ContactVariableId,
                Id = source.ContactId,
                VariableId = source.VariableId,
                Value = source.Value,
                FakeVariableId = source.FakeVariableId,
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static CompanyContactVariable CreateFrom(this DomainModels.ScopeVariable source)
        {
            return new CompanyContactVariable
            {

                ContactVariableId = source.ScopeVariableId,
                ContactId = source.Id,
                FakeVariableId = source.FakeVariableId,
                VariableId = source.VariableId,
                Value = source.Value,
                Type = source.FieldVariable != null ? source.FieldVariable.VariableType : null,
                Title = source.FieldVariable != null ? source.FieldVariable.VariableTitle : string.Empty,
                VariableOptions = source.FieldVariable != null ? (source.FieldVariable.VariableOptions != null ? source.FieldVariable.VariableOptions.Select(vo => vo.CreateFrom()).ToList()
            : null) : null
            };
        }


        #endregion
    }
}