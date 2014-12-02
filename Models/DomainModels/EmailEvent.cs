using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class EmailEvent
    {
        public int EmailEventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public Nullable<int> EventType { get; set; }
    }
}
