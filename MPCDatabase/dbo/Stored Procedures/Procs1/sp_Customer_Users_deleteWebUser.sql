CREATE PROCEDURE dbo.sp_Customer_Users_deleteWebUser
	(
	@CustomerUserID int
	)
AS
	DELETE FROM tbl_customerusers WHERE tbl_customerusers.CustomerUserID=@CustomerUserID

	RETURN