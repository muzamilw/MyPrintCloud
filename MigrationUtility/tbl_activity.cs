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
    
    public partial class tbl_activity
    {
        public int ActivityID { get; set; }
        public int ActivityTypeID { get; set; }
        public string ActivityCode { get; set; }
        public string ActivityRef { get; set; }
        public Nullable<System.DateTime> ActivityDate { get; set; }
        public System.DateTime ActivityTime { get; set; }
        public Nullable<System.DateTime> ActivityStartTime { get; set; }
        public Nullable<System.DateTime> ActivityEndTime { get; set; }
        public Nullable<int> ActivityProbability { get; set; }
        public Nullable<int> ActivityPrice { get; set; }
        public Nullable<int> ActivityUnit { get; set; }
        public string ActivityNotes { get; set; }
        public int IsActivityAlarm { get; set; }
        public Nullable<System.DateTime> AlarmDate { get; set; }
        public Nullable<System.DateTime> AlarmTime { get; set; }
        public int ActivityLink { get; set; }
        public Nullable<bool> IsCustomerActivity { get; set; }
        public Nullable<int> ContactID { get; set; }
        public Nullable<int> SupplierContactID { get; set; }
        public Nullable<int> ProspectContactID { get; set; }
        public int SystemUserID { get; set; }
        public Nullable<bool> IsPrivate { get; set; }
        public int IsComplete { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<System.DateTime> CompletionTime { get; set; }
        public Nullable<int> CompletionSuccess { get; set; }
        public string CompletionResult { get; set; }
        public Nullable<int> CompletedBy { get; set; }
        public Nullable<int> IsFollowedUp { get; set; }
        public Nullable<int> FollowedActivityID { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedtime { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> CampaignID { get; set; }
        public Nullable<short> IsLocked { get; set; }
        public Nullable<int> LockedBy { get; set; }
        public Nullable<int> SystemSiteID { get; set; }
        public Nullable<int> ContactCompanyID { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public Nullable<int> SourceID { get; set; }
        public Nullable<int> FlagID { get; set; }
    }
}
