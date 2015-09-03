using System;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Activity Domain Model
    /// </summary>
    public class Activity
    {
        public int ActivityId { get; set; }
        public int ActivityTypeId { get; set; }
        public string ActivityCode { get; set; }
        public string ActivityRef { get; set; }
        public DateTime? ActivityDate { get; set; }
        public DateTime ActivityTime { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public int? ActivityProbability { get; set; }
        public int? ActivityPrice { get; set; }
        public int? ActivityUnit { get; set; }
        public string ActivityNotes { get; set; }
        public int IsActivityAlarm { get; set; }
        public DateTime? AlarmDate { get; set; }
        public DateTime? AlarmTime { get; set; }
        public int ActivityLink { get; set; }
        public bool? IsCustomerActivity { get; set; }
        public int? ContactId { get; set; }
        public Guid? SystemUserId { get; set; }
        public bool? IsPrivate { get; set; }
        public int IsComplete { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? CompletionTime { get; set; }
        public int? CompletionSuccess { get; set; }
        public string CompletionResult { get; set; }
        public int? CompletedBy { get; set; }
        public int? IsFollowedUp { get; set; }
        public int? FollowedActivityId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? LastModifiedtime { get; set; }
        public int? LastModifiedBy { get; set; }
        public Guid? CreatedBy { get; set; }
        public int? CampaignId { get; set; }
        public short? IsLocked { get; set; }
        public int? LockedBy { get; set; }
        public int? SystemSiteId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? SourceId { get; set; }
        public int? FlagId { get; set; }
        public long? CompanyId { get; set; }

        public virtual Company Company { get; set; }



        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(Activity target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.ActivityTypeId = ActivityTypeId;
            target.ActivityCode = ActivityCode;
            target.ActivityRef = ActivityRef;
            target.ActivityTime = ActivityTime;
            target.ActivityStartTime = ActivityStartTime;
            target.ActivityEndTime = ActivityEndTime;
              target.ActivityProbability = ActivityProbability;
            target.ActivityPrice = ActivityPrice;
            target.ActivityUnit = ActivityUnit;
            target.ActivityNotes = ActivityNotes;
            target.AlarmDate = AlarmDate;
            target.AlarmTime = AlarmTime;
              target.ActivityLink = ActivityLink;
            target.IsCustomerActivity = IsCustomerActivity;
            target.ContactId = ContactId;
            target.SystemUserId = SystemUserId;
            target.IsPrivate = IsPrivate;
            target.IsComplete = IsComplete;
              target.CompletionDate = CompletionDate;
            target.CompletionTime = CompletionTime;
            target.CompletionSuccess = CompletionSuccess;
            target.CompletionResult = CompletionResult;
            target.CompletedBy = CompletedBy;
            target.IsFollowedUp = IsFollowedUp;
              target.FollowedActivityId = FollowedActivityId;
            target.LastModifiedDate = LastModifiedDate;
            target.LastModifiedtime = LastModifiedtime;
            target.LastModifiedBy = LastModifiedBy;
            target.CreatedBy = CreatedBy;
            target.CampaignId = CampaignId;
              target.IsLocked = IsLocked;
            target.LockedBy = LockedBy;
            target.SystemSiteId = SystemSiteId;
            target.ProductTypeId = ProductTypeId;
            target.SourceId = SourceId;
              target.FlagId = FlagId;


        }

        #endregion
    }
}
