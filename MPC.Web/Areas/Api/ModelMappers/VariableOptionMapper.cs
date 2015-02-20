using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Variable Option Mapper
    /// </summary>
    public static class VariableOptionMapper
    {
        #region
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static VariableOption CreateFrom(this DomainModels.VariableOption source)
        {
            return new VariableOption
            {
                VariableId = source.VariableId,
                SortOrder = source.SortOrder,
                Value = source.Value,
                VariableOptionId = source.VariableOptionId
            };
        }

        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.VariableOption CreateFrom(this VariableOption source)
        {
            return new DomainModels.VariableOption
            {
                VariableId = source.VariableId,
                SortOrder = source.SortOrder,
                Value = source.Value,
                FakeId = source.FakeId,
                VariableOptionId = source.VariableOptionId
            };
        }
        #endregion
    }
}