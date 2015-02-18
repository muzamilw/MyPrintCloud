CREATE PROCEDURE [dbo].[sp_Customer_SetDefaultContactUncheck]

	(
		@CustomerID int
	)

AS
	UPDATE tbl_contacts SET IsDefaultContact=0
         WHERE ContactCompanyID = @CustomerID
         
	RETURN