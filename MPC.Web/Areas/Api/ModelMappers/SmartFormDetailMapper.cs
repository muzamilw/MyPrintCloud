using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Smart Form Detail
    /// </summary>
    public static class SmartFormDetailMapper
    {
        #region Public
        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.SmartFormDetail CreateFrom(this SmartFormDetail source)
        {
            return new DomainModels.SmartFormDetail
            {
                SmartFormDetailId = source.SmartFormDetailId,
                SmartFormId = source.SmartFormId,
                CaptionValue = source.CaptionValue,
                IsRequired = source.IsRequired,
                ObjectType = source.ObjectType,
                SortOrder = source.SortOrder,
                VariableId = source.VariableId < 0 ? null : source.VariableId,
                FakeVariableId = source.VariableId
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static SmartFormDetail CreateFrom(this DomainModels.SmartFormDetail source)
        {
            return new SmartFormDetail
            {
                SmartFormDetailId = source.SmartFormDetailId,
                SmartFormId = source.SmartFormId,
                CaptionValue = source.CaptionValue,
                IsRequired = source.IsRequired,
                ObjectType = source.ObjectType,
                SortOrder = source.SortOrder,
                VariableId = source.VariableId,
                VariableType = source.FieldVariable != null ? source.FieldVariable.VariableType : 0,
                Title = source.FieldVariable != null ? source.FieldVariable.VariableTitle : string.Empty,
                WaterMark = source.FieldVariable != null ? source.FieldVariable.WaterMark : string.Empty,
                DefaultValue = source.FieldVariable != null ? source.FieldVariable.DefaultValue : string.Empty
            };
        }

        #endregion
    }
}