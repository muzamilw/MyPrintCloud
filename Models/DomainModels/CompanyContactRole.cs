using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CompanyContactRole
    {
        public int ContactRoleId { get; set; }
        public string ContactRoleName { get; set; }

        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}
