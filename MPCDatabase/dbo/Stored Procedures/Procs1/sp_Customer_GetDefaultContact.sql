CREATE PROCEDURE dbo.sp_Customer_GetDefaultContact

	(
		@CustomerContactID int
	)
AS
	SELECT IsDefaultContact FROM  tbl_customercontacts WHERE CustomerContactID=@CustomerContactID

	RETURN