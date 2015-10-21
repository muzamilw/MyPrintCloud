using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Widget Mapper
    /// </summary>
    public static class WidgetMapper
    {
        #region Public
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static Widget CreateFrom(this DomainModels.Widget source)
        {
            return new Widget()
            {
                WidgetId = source.WidgetId,
                WidgetName = source.WidgetName,
                WidgetCode = source.WidgetCode,
                WidgetControlName = source.WidgetControlName,
                WidgetCss = source.WidgetCss,
                ThumbnailUrl = source.ThumbnailUrl,
                Description = source.Description
                
            };
        }

        #endregion
    }
}