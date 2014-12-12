using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Campaign Email Queue Domain Model
    /// </summary>
    public class CampaignEmailQueue
    {
        public int EmailQueueId { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string EmailFrom { get; set; }
        public short? Type { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Images { get; set; }
        public DateTime? SendDateTime { get; set; }
        public byte? IsDeliverd { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public string SMTPServer { get; set; }
        public string ErrorResponse { get; set; }
        public string FileAttachment { get; set; }
        public int? AttemptCount { get; set; }
        public string ToName { get; set; }
        public string FromName { get; set; }
        public int? CampaignReportId { get; set; }
        public long? OrganisationId { get; set; }
    }
}
