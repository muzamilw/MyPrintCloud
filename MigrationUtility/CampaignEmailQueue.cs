//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility
{
    using System;
    using System.Collections.Generic;
    
    public partial class CampaignEmailQueue
    {
        public int EmailQueueId { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string EmailFrom { get; set; }
        public Nullable<short> Type { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Images { get; set; }
        public Nullable<System.DateTime> SendDateTime { get; set; }
        public Nullable<byte> IsDeliverd { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public string SMTPServer { get; set; }
        public string ErrorResponse { get; set; }
        public string FileAttachment { get; set; }
        public Nullable<int> AttemptCount { get; set; }
        public string ToName { get; set; }
        public string FromName { get; set; }
        public Nullable<int> CampaignReportId { get; set; }
        public Nullable<long> OrganisationId { get; set; }
    }
}
