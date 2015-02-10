CREATE PROCEDURE dbo.sp_Customers_IsAddressUsed_checkAddress_in_Estimate

	(
		@AddressID int
	)

AS
	SELECT tbl_estimates.AddressID
         FROM tbl_estimates WHERE tbl_estimates.AddressID=@AddressID
	
	RETURN