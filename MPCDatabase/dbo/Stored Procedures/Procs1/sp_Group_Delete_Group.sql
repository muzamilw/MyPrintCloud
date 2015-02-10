CREATE PROCEDURE dbo.sp_Group_Delete_Group
	@GroupID int
AS
	Delete FROM tbl_groups where GroupID = @GroupID
	RETURN