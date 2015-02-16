CREATE PROCEDURE dbo.sp_users_get_username
(
@UserName varchar(100),
@SystemUserID int
)
AS
	SELECT tbl_systemusers.SystemUserID FROM tbl_systemusers WHERE ((tbl_systemusers.UserName = @UserName) and (tbl_systemusers.SystemUserID <> @SystemUserID))
	RETURN