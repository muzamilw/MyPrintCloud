using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MPC.MIS.Areas.Api.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public short IsSystemRole { get; set; }
        public int? LockedBy { get; set; }
        public short IsCompanyLevel { get; set; }
        public List<RoleRight> Rolerights { get; set; }
    }
}