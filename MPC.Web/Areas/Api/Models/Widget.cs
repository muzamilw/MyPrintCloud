namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Widget API Model
    /// </summary>
    public class Widget
    {

        public long WidgetId { get; set; }
        public string WidgetName { get; set; }
        public string WidgetCode { get; set; }
        public string WidgetControlName { get; set; }
        public string WidgetCss { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public string WidgetHtml { get; set; }
        public long? OrganisationId { get; set; }
        public long? CompanyId { get; set; }
        
    }
}