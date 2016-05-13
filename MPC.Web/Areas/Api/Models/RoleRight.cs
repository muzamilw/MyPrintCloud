using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class RoleRight
    {
        public int RoleId { get; set; }
        public int RightId { get; set; }
        public string RoleName { get; set; }
        public string RightName { get; set; }

    }
}