CREATE PROCEDURE dbo.sp_roles_add_SQL_INSERT_RIGHT
(
@RoleID int,
@RightID int
)
AS
	insert into tbl_rolerights (RoleID,RightID) VALUES (@RoleID,@RightID)
	RETURN