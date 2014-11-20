using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.Models
{
    public class CompanyDomain
    {
        public long CompanyDomainId { get; set; }

        public string Domain { get; set; }

        public long CompanyId { get; set; }
    }
}