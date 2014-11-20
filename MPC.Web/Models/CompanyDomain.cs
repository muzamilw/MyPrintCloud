using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Web.Models
{
    public class CompanyDomain
    {
        public int CompanyDomainId { get; set; }

        public string Domain { get; set; }

        public int CompanyId { get; set; }
    }
}