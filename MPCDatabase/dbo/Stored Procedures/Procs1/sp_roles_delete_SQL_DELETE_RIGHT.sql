CREATE PROCEDURE dbo.sp_roles_delete_SQL_DELETE_RIGHT

	(
		@RoleID int,
		@RightID int
	)

AS
	delete from tbl_rolerights where RoleID=@RoleID and RightID=@RightID
	RETURN