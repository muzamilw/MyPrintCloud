using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.SessionModels
{
    public class CompanyDTO
    {
        public long CompanyId { get; set; }

        public string Name { get; set; }

        public short IsCustomer { get; set; }
    }
}