using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CompanyDomain
    {
        public long CompanyDomainId { get; set; }

        public string Domain { get; set; }

        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
