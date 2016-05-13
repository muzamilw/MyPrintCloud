using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class SystemUsersResponse
    {
        public List<Role> UserRoles { get; set; }
        public List<SystemUser> SystemUsers { get; set; }
    }
}