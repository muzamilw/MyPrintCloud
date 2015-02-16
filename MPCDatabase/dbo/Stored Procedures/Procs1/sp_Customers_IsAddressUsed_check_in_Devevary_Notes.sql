CREATE PROCEDURE dbo.sp_Customers_IsAddressUsed_check_in_Devevary_Notes

	(
		@AddressID int
	)

AS
	SELECT tbl_deliverynotes.AddressID
      FROM tbl_deliverynotes WHERE tbl_deliverynotes.AddressID=@AddressID
      
	RETURN