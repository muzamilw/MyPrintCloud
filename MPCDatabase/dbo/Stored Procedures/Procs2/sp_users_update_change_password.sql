CREATE PROCEDURE dbo.sp_users_update_change_password
( @Password  text,
@SystemUserID int)

AS
	update tbl_systemusers set  Password=@Password where SystemUserID=@SystemUserID
	RETURN