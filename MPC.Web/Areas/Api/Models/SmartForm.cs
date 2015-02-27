using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Smart Form API Model
    /// </summary>
    public class SmartForm
    {
        public long SmartFormId { get; set; }
        public string Name { get; set; }
        public string Heading { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }
        public List<SmartFormDetail> SmartFormDetails { get; set; }
    }
}