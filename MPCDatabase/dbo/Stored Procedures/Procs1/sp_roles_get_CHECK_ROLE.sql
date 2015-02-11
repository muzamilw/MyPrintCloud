CREATE PROCEDURE dbo.sp_roles_get_CHECK_ROLE

	(
		@RoleID int,
		@RoleName varchar(100),
		@CompanyID int
	)

AS
	SELECT RoleID FROM tbl_roles where (RoleID<>@RoleID and RoleName=@RoleName) and CompanyID=@CompanyID
	RETURN