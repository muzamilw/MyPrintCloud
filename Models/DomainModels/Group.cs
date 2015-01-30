using System;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Group Domain Model
    /// </summary>
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public int? LastModifiedBy { get; set; }
        public int? SystemSiteId { get; set; }
        public bool? IsPrivate { get; set; }
        public string Notes { get; set; }
    }
}
