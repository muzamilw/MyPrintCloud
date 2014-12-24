using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Widget Mapper
    /// </summary>
    public static class WidgetMapper
    {
        public static Widget CreateFrom(this DomainModels.Widget source)
        {
            return new Widget()
            {
                WidgetId = source.WidgetId,
                WidgetName = source.WidgetName
            };
        }
    }
}