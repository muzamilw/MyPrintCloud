
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    public class SystemUser
    {
        public System.Guid SystemUserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public int? OrganizationId { get; set; }
        public int? DepartmentId { get; set; }
        public string FullName { get; set; }
        public string UserType { get; set; }
        public int? RoleId { get; set; }
        public short? IsAccountDisabled { get; set; }
        public float? CostPerHour { get; set; }
        public short? IsScheduleable { get; set; }
        public int? IsSystemUser { get; set; }
        public string UserAuthToken { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ItemStockUpdateHistory> ItemStockUpdateHistories { get; set; } 
    }
}
