CREATE PROCEDURE dbo.sp_roles_get_SQL_GET_ROLE
( @CompanyID int )
AS
	SELECT tbl_roles.RoleID,tbl_roles.RoleName, tbl_roles.RoleDescription,tbl_roles.IsSystemRole,tbl_roles.IsCompanyLevel FROM tbl_roles Where CompanyID=@CompanyID order by tbl_roles.RoleName
	RETURN