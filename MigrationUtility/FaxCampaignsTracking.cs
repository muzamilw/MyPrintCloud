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
    
    public partial class FaxCampaignsTracking
    {
        public int FAXCampaignTrackingId { get; set; }
        public int FAXCampaignId { get; set; }
        public string ReplyCode { get; set; }
        public string FAXNumber { get; set; }
        public Nullable<bool> IsDeliverd { get; set; }
        public int IsWithError { get; set; }
        public Nullable<int> Id { get; set; }
        public Nullable<int> ContactId { get; set; }
        public Nullable<short> MailFlag { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    }
}
