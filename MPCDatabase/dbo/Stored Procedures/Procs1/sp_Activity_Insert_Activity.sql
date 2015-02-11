
CREATE PROCEDURE [dbo].[sp_Activity_Insert_Activity]
		@ActivityTypeID int,@ActivityCode varchar(50) ,@ActivityRef varchar(500),@ActivityStartTime datetime,
        @ActivityEndTime datetime ,@ActivityProbability int,@ActivityPrice int,@ActivityUnit int,@ActivityNotes ntext,@IsActivityAlarm int,@AlarmDate datetime,@AlarmTime datetime, 
        @ActivityLink int,@IsCustomerActivity int,@CustomerContactID int,@SupplierContactID int,@ProspectContactID int,@SystemUserID int,@IsPrivate int,@IsFollowedUp int,@FollowedActivityID int,@LastModifiedDate datetime,@LastModifiedTime datetime,@LastModifiedBy int,@IsComplete int,@CompletionDate datetime ,@CompletionTime datetime ,@CompletionSuccess int,@CompletionResult varchar(100) ,@CompletedBy int ,@CreatedBy int
		,@SystemSiteID int,@ContactCompanyID int
AS
	INSERT INTO tbl_activity (ActivityTypeID,
        ActivityCode, ActivityRef,ActivityStartTime,ActivityEndTime,ActivityProbability,
        ActivityPrice,ActivityUnit,ActivityNotes,IsActivityAlarm,AlarmDate,AlarmTime,ActivityLink,
        IsCustomerActivity, ContactID,SupplierContactID,ProspectContactID,SystemUserID,IsPrivate,IsFollowedUp,FollowedActivityID,LastModifiedDate,LastModifiedTime,LastModifiedBy,IsComplete,CompletionDate,CompletionTime,CompletionSuccess,CompletionResult,CompletedBy,CreatedBy,SystemSiteID,ContactCompanyID) 
         VALUES (@ActivityTypeID,@ActivityCode,@ActivityRef,@ActivityStartTime,
        @ActivityEndTime,@ActivityProbability,@ActivityPrice,@ActivityUnit,@ActivityNotes,@IsActivityAlarm,@AlarmDate,@AlarmTime, 
        @ActivityLink,@IsCustomerActivity,@CustomerContactID,@SupplierContactID,@ProspectContactID,@SystemUserID,@IsPrivate,@IsFollowedUp,@FollowedActivityID,@LastModifiedDate,@LastModifiedTime,@LastModifiedBy,@IsComplete,@CompletionDate,@CompletionTime,@CompletionSuccess,@CompletionResult,@CompletedBy,@CreatedBy,@SystemSiteID,@ContactCompanyID);select @@Identity as ActivityID
	RETURN