CREATE PROCEDURE dbo.sp_roles_delete_SQL_DELETE_SECTION
	(
		@RoleID int,
		@SectionID int)
AS
	delete from tbl_rolesections where RoleID=@RoleID and SectionID=@SectionID
	RETURN