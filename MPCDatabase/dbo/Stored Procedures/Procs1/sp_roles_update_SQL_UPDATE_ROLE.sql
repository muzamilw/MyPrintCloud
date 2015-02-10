CREATE PROCEDURE dbo.sp_roles_update_SQL_UPDATE_ROLE

	(
	@IsCompanyLevel bit ,
	@RoleName varchar(100),
	@RoleDescription text,
	@RoleID int
	)

AS
	update tbl_roles set IsCompanyLevel=@IsCompanyLevel,RoleName=@RoleName,RoleDescription=@RoleDescription where (RoleID=@RoleID)
	RETURN