CREATE PROCEDURE dbo.sp_Group_Delete_GroupDetailByGroupID
	@GroupID int
AS
	DELETE FROM tbl_group_detail where GroupID = @GroupID
	RETURN