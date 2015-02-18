CREATE PROCEDURE [dbo].[sp_Suppliers_IsGeneralSupplier]
	(
		@SupplierID int
	)

AS
	SELECT tbl_ContactCompanies.Contactcompanyid
         FROM tbl_ContactCompanies WHERE Contactcompanyid=@SupplierID and tbl_ContactCompanies.IsGeneral<>0
	RETURN