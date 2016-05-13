using System;

namespace MPC.MIS.Areas.Api.Models
{
    public class SystemUser
    {
        public Guid SystemUserId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public int? OrganizationId { get; set; }
        public int? DepartmentId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserType { get; set; }
        public int? RoleId { get; set; }
        public short? IsAccountDisabled { get; set; }
        public short? IsTillSupervisor { get; set; }
        public float? CostPerHour { get; set; }
        public string CurrentMachineName { get; set; }
        public string CurrentMachineIP { get; set; }
        public int? LockedBy { get; set; }
        public short? IsScheduleable { get; set; }
        public long? CompanySiteId { get; set; }
        public string ReplyEmail { get; set; }
        public bool? CanSendEmail { get; set; }
        public int? IsSystemUser { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string EmailSignature { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string UserAuthToken { get; set; }
        public bool? canDesignTemplate { get; set; }
        public bool? canApproveTemplate { get; set; }
        public string RoleName { get; set; }  
        //public virtual Organisation Organisation { get; set; }
    }
}