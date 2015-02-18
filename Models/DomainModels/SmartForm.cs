using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Smart From Domain Model
    /// </summary>
    public class SmartForm
    {
        public long SmartFormId { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<SmartFormDetail> SmartFormDetails { get; set; }
    }
}
