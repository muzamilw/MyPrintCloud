CREATE PROCEDURE [dbo].[sp_Suppliers_Supplier_CheckSupplierAccountNo]
	(
	@AccountNumber varchar(10)
	)
AS
SELECT * FROM tbl_contactCompanies WHERE AccountNumber=@AccountNumber
	RETURN