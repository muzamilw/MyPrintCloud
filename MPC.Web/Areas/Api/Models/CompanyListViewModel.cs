using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CompanyListViewModel
    {
        public long CompanyId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public short Status { get; set; }
        public short IsCustomer { get; set; }
    }
}