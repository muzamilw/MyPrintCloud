namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Cms Skin Page Widget Param Api Model
    /// </summary>
    public class CmsSkinPageWidgetParam
    {
        public long PageWidgetParamId { get; set; }
        public long? PageWidgetId { get; set; }
        public string ParamName { get; set; }
        public string ParamValue { get; set; }
    }
}