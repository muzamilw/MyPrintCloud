namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Cms Skin Page Widget
    /// </summary>
    public class CmsSkinPageWidget
    {
        public long PageWidgetId { get; set; }
        public long? PageId { get; set; }
        public long? WidgetId { get; set; }
        public short? Sequence { get; set; }
        public string Html { get; set; }
        //public IEnumerable<CmsSkinPageWidgetParam> CmsSkinPageWidgetParams { get; set; }
    }
}