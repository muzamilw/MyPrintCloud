namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Email Event Api Model
    /// </summary>
    public class EmailEvent
    {
        public int EmailEventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public int? EventType { get; set; }
    }
}