CREATE PROCEDURE dbo.sp_Group_Update_Group
@GroupID int,@GroupName varchar(256),@GroupDescription varchar(1000),@LastModifiedDateTime datetime,@LastModifiedBy int,@SystemSiteID int,@Notes ntext
AS
	UPDATE tbl_groups SET
GroupName=@GroupName,GroupDescription=@GroupDescription,LastModifiedDateTime=@LastModifiedDateTime,LastModifiedBy=@LastModifiedBy,SystemSiteID=@SystemSiteID,Notes=@Notes
	WHERE GroupID = @GroupID
	RETURN