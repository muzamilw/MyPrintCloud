using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{

    public  class UserActionsLog
    {
        public long LogId { get; set; }
        public string Action { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }
        public string TableName { get; set; }
        public Nullable<long> RecordId { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public string DomainId { get; set; }
        public string Comments { get; set; }
        public Nullable<long> OrganisationId { get; set; }
    }
}
