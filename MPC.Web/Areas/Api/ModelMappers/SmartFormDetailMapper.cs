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
                VariableId = source.VariableId
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
                VariableType = 1
            };
        }

        #endregion
    }
}