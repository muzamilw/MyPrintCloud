using System;
using System.Collections.Generic;


namespace MPC.MIS.Areas.Api.Models
{
    public class UserRoleResponse
    {
        public List<Role> UserRoles { get; set; }
        public List<AccessRight> AccessRights { get; set; }
    }
}