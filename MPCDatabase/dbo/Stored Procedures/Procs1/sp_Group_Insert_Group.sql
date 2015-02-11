CREATE PROCEDURE dbo.sp_Group_Insert_Group
@GroupName varchar(256),@GroupDescription varchar(1000),@CreationDateTime datetime,@CreatedBy int,@LastModifiedDateTime datetime,@LastModifiedBy int,@SystemSiteID int,@Notes ntext
AS
	INSERT INTO tbl_groups 
(GroupName,GroupDescription,CreationDateTime,CreatedBy,LastModifiedDateTime,LastModifiedBy,SystemSiteID,Notes)
Values
(@GroupName,@GroupDescription,@CreationDateTime,@CreatedBy,@LastModifiedDateTime,@LastModifiedBy,@SystemSiteID,@Notes);select @@Identity as GroupID
	RETURN