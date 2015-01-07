using System.Linq;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Areas.Api.Models;
using DomainResponseModels = MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Cms Skin Page Widget Param Mapper
    /// </summary>
    public static class CmsSkinPageWidgetParamMapper
    {
        #region Public
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.CmsSkinPageWidgetParam CreateFrom(this ApiModels.CmsSkinPageWidgetParam source)
        {
            return new DomainModels.CmsSkinPageWidgetParam
            {
                PageWidgetParamId = source.PageWidgetParamId,
                PageWidgetId = source.PageWidgetId,
                ParamName = "innerHTML",
                ParamValue = source.ParamValue,
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static ApiModels.CmsSkinPageWidgetParam CreateFrom(this DomainModels.CmsSkinPageWidgetParam source)
        {
            return new ApiModels.CmsSkinPageWidgetParam
            {
                PageWidgetParamId = source.PageWidgetParamId,
                PageWidgetId = source.PageWidgetId,
                ParamValue = source.ParamValue,
            };
        }
        #endregion
    }
}