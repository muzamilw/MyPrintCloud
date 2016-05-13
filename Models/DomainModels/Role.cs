using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Role Domain Model
    /// </summary>
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public short IsSystemRole { get; set; }
        public int? LockedBy { get; set; }
        public short IsCompanyLevel { get; set; }
        public long OrganisationId { get; set; }
        public int CompanyId { get; set; }
        public virtual ICollection<Roleright> Rolerights { get; set; }
    }
}
