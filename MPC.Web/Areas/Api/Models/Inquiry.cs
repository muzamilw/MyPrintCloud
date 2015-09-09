using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class Inquiry
    {
        public int InquiryId { get; set; }
        public string Title { get; set; }
        public long ContactId { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public int? SourceId { get; set; }
        public long? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? RequireByDate { get; set; }
        public Guid? SystemUserId { get; set; }
        public int? Status { get; set; }
        public bool? IsDirectInquiry { get; set; }
        public int? FlagId { get; set; }
        public string InquiryCode { get; set; }
        public Guid? CreatedBy { get; set; }
        public long? OrganisationId { get; set; }
        public long? EstimateId { get; set; }
        public int InquiryItemsCount { get; set; }
        public virtual Company Company { get; set; }
        public virtual CompanyContact CompanyContact { get; set; }
        public virtual IEnumerable<InquiryAttachment> InquiryAttachments { get; set; }
        public virtual IEnumerable<InquiryItem> InquiryItems { get; set; }
    }
}