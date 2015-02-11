CREATE PROCEDURE dbo.sp_roles_add_SQL_INSERT_SECTION
(
@RoleID int,
@SectionID int
)
AS
	insert into tbl_rolesections (RoleID,SectionID) VALUES (@RoleID,@SectionID)
	RETURN