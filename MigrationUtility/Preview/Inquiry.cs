//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility.Preview
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inquiry
    {
        public Inquiry()
        {
            this.InquiryAttachments = new HashSet<InquiryAttachment>();
            this.InquiryItems = new HashSet<InquiryItem>();
        }
    
        public int InquiryId { get; set; }
        public string Title { get; set; }
        public long ContactId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> SourceId { get; set; }
        public Nullable<int> ContactCompanyId { get; set; }
        public Nullable<System.DateTime> RequireByDate { get; set; }
        public Nullable<int> SystemUserId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsDirectInquiry { get; set; }
        public Nullable<int> FlagId { get; set; }
        public string InquiryCode { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> BrokerContactCompanyId { get; set; }
        public Nullable<long> OrganisationId { get; set; }
    
        public virtual CompanyContact CompanyContact { get; set; }
        public virtual ICollection<InquiryAttachment> InquiryAttachments { get; set; }
        public virtual ICollection<InquiryItem> InquiryItems { get; set; }
    }
}