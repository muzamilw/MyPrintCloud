using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Inquiry
    {
        public int InquiryId { get; set; }
        public string Title { get; set; }
        public long ContactId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int? SourceId { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? RequireByDate { get; set; }
        public Guid? SystemUserId { get; set; }
        public int? Status { get; set; }
        public bool? IsDirectInquiry { get; set; }
        public int? FlagId { get; set; }
        public string InquiryCode { get; set; }
        public Guid? CreatedBy { get; set; }
        public long? OrganisationId { get; set; }
        public long? EstimateId { get; set; }
        public virtual Company Company { get; set; }
        public virtual CompanyContact CompanyContact { get; set; }
        public virtual ICollection<InquiryAttachment> InquiryAttachments { get; set; }
        public virtual ICollection<InquiryItem> InquiryItems { get; set; }
    }
}
