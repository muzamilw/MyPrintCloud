using System;
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    public class EmailEvent
    {
        public int EmailEventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public int? EventType { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
