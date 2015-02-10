CREATE PROCEDURE dbo.sp_Group_Delete_GroupDetailByGroupDetailID 
	@GroupDetailID int
AS
	DELETE FROM tbl_group_detail where GroupDetailID = @GroupDetailID
	RETURN