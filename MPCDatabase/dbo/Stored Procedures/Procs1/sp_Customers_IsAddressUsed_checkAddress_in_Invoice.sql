CREATE PROCEDURE dbo.sp_Customers_IsAddressUsed_checkAddress_in_Invoice

	(
		@AddressID int
	)


AS
	SELECT tbl_invoices.AddressID
         FROM tbl_invoices WHERE tbl_invoices.AddressID=@AddressID
	
	RETURN