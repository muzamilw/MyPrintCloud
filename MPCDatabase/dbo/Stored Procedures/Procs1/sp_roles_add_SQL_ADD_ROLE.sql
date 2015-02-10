CREATE PROCEDURE dbo.sp_roles_add_SQL_ADD_ROLE
	(
	@IsCompanyLevel bit ,
	@RoleName varchar(100),
	@RoleDescription text,
	@CompanyID int
	
	)
AS
	insert into tbl_roles (IsCompanyLevel,RoleName,RoleDescription,CompanyID) VALUES (@IsCompanyLevel,@RoleName,@RoleDescription,@CompanyID);
	 select @@Identity as RoleID
	RETURN