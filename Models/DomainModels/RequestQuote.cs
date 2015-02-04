using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
   public class RequestQuote
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
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
       
        public string Mobile { get; set; }
        public string Password { get; set; }
        
        public string InquiryItemTitle1 { get; set; }
        public string InquiryItemNotes1 { get; set; }
        public string InquiryItemDeliveryDate1 { set; get; }

        public string InquiryItemTitle2 { get; set; }
        public string InquiryItemNotes2 { get; set; }
        public string InquiryItemDeliveryDate2 { get; set; }

        public string InquiryItemTitle3 { get; set; }
        public string InquiryItemNotes3 { get; set; }
        public string InquiryItemDeliveryDate3 { get; set; }


        public virtual ICollection<InquiryAttachment> InquiryAttachments { get; set; }
    }
}
