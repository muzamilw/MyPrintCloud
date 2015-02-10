CREATE PROCEDURE [dbo].[sp_Suppliers_Contacts_SetDefaultContactUncheck]
(
@SupplierID int
)
AS
	UPDATE tbl_Contacts SET IsDefaultContact=0
       WHERE ContactCompanyID = @SupplierID
	RETURN