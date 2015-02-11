CREATE PROCEDURE [dbo].[sp_Suppliers_DeleteSupplier_SQL_DELETE_SUPPLIER]
	(
		@SupplierID int
	)
AS

	DELETE FROM tbl_ContactCompanies
       WHERE tbl_ContactCompanies.Contactcompanyid=@SupplierID
	RETURN