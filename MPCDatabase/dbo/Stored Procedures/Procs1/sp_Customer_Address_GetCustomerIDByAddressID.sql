CREATE PROCEDURE dbo.sp_Customer_Address_GetCustomerIDByAddressID

	(
	  @AddressID int
	)
	
    AS
	
	select customerid from tbl_customeraddresses where addressid=@AddressID
	

	RETURN