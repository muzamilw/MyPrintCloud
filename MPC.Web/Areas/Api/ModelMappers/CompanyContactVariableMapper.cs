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
        public static DomainModels.CompanyContactVariable CreateFrom(this CompanyContactVariable source)
        {
            return new DomainModels.CompanyContactVariable
            {
                ContactVariableId = source.ContactVariableId,
                ContactId = source.ContactId,
                VariableId = source.VariableId,
                Value = source.Value,
                FakeVariableId = source.FakeVariableId,
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static CompanyContactVariable CreateFrom(this DomainModels.CompanyContactVariable source)
        {
            return new CompanyContactVariable
            {

                ContactVariableId = source.ContactVariableId,
                ContactId = source.ContactId,
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