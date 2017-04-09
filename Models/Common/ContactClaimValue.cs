using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class ContactClaimValue
    {
        public long ContactId { get; set; }
        public long ContactCompanyId { get; set; }
        public int ContactRoleId { get; set; }
        public long ContactTerritoryId { get; set; }
        public bool HasUserDamRights {get; set;}
    }
}
