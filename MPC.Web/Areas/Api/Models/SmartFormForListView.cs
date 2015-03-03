namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// SmartForm FOR List View
    /// </summary>
    public class SmartFormForListView
    {
        public long SmartFormId { get; set; }
        public long? CompanyId { get; set; }
        public string Name { get; set; }
        public string Heading { get; set; }
    }
}