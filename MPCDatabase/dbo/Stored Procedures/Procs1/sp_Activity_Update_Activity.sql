
Create PROCEDURE [dbo].[sp_Activity_Update_Activity]
		@ActivityTypeID int,@ActivityCode varchar(50), @ActivityRef varchar(500),
        @ActivityStartTime datetime, @ActivityEndTime datetime,
        @ActivityProbability int,@ActivityPrice int,@ActivityUnit int,
        @ActivityNotes ntext,@IsActivityAlarm int,
        @AlarmDate datetime,@AlarmTime datetime,@ActivityLink int,  
        @CustomerContactID int,@SupplierContactID int,@ProspectContactID int,
        @IsPrivate int,@LastModifiedDate datetime,
        @LastModifiedTime datetime,@LastModifiedBy int,@IsComplete int,@CompletionDate datetime,
        @CompletionTime datetime,@CompletionSuccess int,@CompletionResult varchar(100),@CompletedBy int,
        @ActivityID int
AS
	UPDATE tbl_activity 
        SET ActivityTypeID=@ActivityTypeID,ActivityCode=@ActivityCode, ActivityRef=@ActivityRef,
        ActivityStartTime=@ActivityStartTime, ActivityEndTime=@ActivityEndTime,
        ActivityProbability=@ActivityProbability,ActivityPrice=@ActivityPrice,ActivityUnit=@ActivityUnit,
        ActivityNotes=@ActivityNotes,IsActivityAlarm=@IsActivityAlarm,
        AlarmDate=@AlarmDate,AlarmTime=@AlarmTime,ActivityLink=@ActivityLink, 
       ContactID=@CustomerContactID,SupplierContactID =@SupplierContactID,ProspectContactID=@ProspectContactID,
        IsPrivate=@IsPrivate,LastModifiedDate=@LastModifiedDate,
        LastModifiedTime=@LastModifiedTime,LastModifiedBy=@LastModifiedBy,IsComplete=@IsComplete,CompletionDate=@CompletionDate,CompletionTime=@CompletionTime,CompletionSuccess=@CompletionSuccess,CompletionResult=@CompletionResult,CompletedBy=@CompletedBy WHERE ActivityID=@ActivityID
	RETURN