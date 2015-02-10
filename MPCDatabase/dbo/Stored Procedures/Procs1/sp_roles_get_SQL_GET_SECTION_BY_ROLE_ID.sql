CREATE PROCEDURE dbo.sp_roles_get_SQL_GET_SECTION_BY_ROLE_ID

	(
		@RoleID int
	)

AS
	SELECT tbl_rolesections.RSID,tbl_rolesections.RoleID,tbl_rolesections.SectionID FROM tbl_rolesections where tbl_rolesections.RoleID=@RoleID
	RETURN