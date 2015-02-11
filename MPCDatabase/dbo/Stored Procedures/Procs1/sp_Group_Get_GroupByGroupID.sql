CREATE PROCEDURE dbo.sp_Group_Get_GroupByGroupID
@GroupID int
AS
	SELECT GroupID,GroupName,GroupDescription,CreationDateTime,CreatedBy,LastModifiedDateTime,LastModifiedBy,SystemSiteID,Notes
	FROM tbl_groups  WHERE GroupID = @GroupID
	RETURN