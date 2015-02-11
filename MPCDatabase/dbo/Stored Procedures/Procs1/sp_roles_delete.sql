CREATE PROCEDURE [dbo].[sp_roles_delete]
	(
		@RoleID int
		)
AS
	delete from dbo.tbl_rolerights where roleID = @RoleID
	delete from dbo.tbl_role_sites where RoleID = @RoleID
	delete from tbl_rolesections where RoleID = @RoleID
	delete from tbl_roles where RoleID=@RoleID 
	
	RETURN