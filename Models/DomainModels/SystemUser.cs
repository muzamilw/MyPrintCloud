using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class SystemUser
    {
        public int SystemUserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public Nullable<int> OrganizationId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserType { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<short> IsAccountDisabled { get; set; }
        public Nullable<short> IsTillSupervisor { get; set; }
        public Nullable<float> CostPerHour { get; set; }
        public string CurrentMachineName { get; set; }
        public string CurrentMachineIP { get; set; }
        public Nullable<int> LockedBy { get; set; }
        public Nullable<short> IsScheduleable { get; set; }
        public Nullable<long> CompanySiteId { get; set; }
        public string ReplyEmail { get; set; }
        public Nullable<bool> CanSendEmail { get; set; }
        public Nullable<int> IsSystemUser { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string EmailSignature { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string UserAuthToken { get; set; }
        public Nullable<bool> canDesignTemplate { get; set; }
        public Nullable<bool> canApproveTemplate { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
