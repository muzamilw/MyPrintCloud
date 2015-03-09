namespace MPC.Models.ResponseModels
{
    public class WidgetForTheme
    {
        public long? PageId { get; set; }
        public string PageName { get; set; }
        public long? WidgetId { get; set; }
        public long? SkinId { get; set; }
        public short? Sequence { get; set; }
        public string ParamValue { get; set; }
    }
}