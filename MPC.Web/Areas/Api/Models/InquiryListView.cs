using System;

namespace MPC.MIS.Areas.Api.Models
{
    public class InquiryListView
    {
        public int InquiryId { get; set; }
        public string Title { get; set; }
        public long ContactId { get; set; }
        public string ContactName { get; set; }
        public System.DateTime CreatedDate { get; set; }
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
        public int InquiryItemsCount { get; set; }
    }
}