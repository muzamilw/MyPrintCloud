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
