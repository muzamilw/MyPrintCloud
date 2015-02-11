CREATE PROCEDURE dbo.sp_roles_get_ROLE_SITES
(@RoleID int)
AS
	Select * from tbl_role_sites where RoleID=@RoleID
	RETURN