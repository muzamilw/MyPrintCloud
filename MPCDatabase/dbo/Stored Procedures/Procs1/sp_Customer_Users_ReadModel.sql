CREATE PROCEDURE [dbo].[sp_Customer_Users_ReadModel]
	(
	@CustomerUserID int
	)
AS
	Select * from tbl_contacts where contactid=@CustomerUserID

	RETURN