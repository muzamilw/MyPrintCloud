using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// NewsLetter Subscriber Domain Model
    /// </summary>
    public class NewsLetterSubscriber
    {
        public int SubscriberId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Status { get; set; }
        public string SubscriptionCode { get; set; }
        public DateTime SubscribeDate { get; set; }
        public DateTime? UnSubscribeDate { get; set; }
        public long? ContactId { get; set; }
        public int? ContactCompanyID { get; set; }
        public int? FlagId { get; set; }
        public CompanyContact CompanyContact { get; set; }
    }
}
