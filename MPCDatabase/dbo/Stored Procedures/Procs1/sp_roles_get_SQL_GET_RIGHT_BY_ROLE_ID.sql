CREATE PROCEDURE dbo.sp_roles_get_SQL_GET_RIGHT_BY_ROLE_ID
(@RoleID int)
AS
	SELECT tbl_rolerights.RRID,tbl_rolerights.RoleID,tbl_rolerights.RightID FROM tbl_rolerights where tbl_rolerights.RoleID=@RoleID
	RETURN