CREATE PROCEDURE dbo.sp_Customers_CustomerTypes_deleteCustomerType

	(
		@CustomerTypeID int
			
	)
AS
	DELETE FROM tbl_customertypes WHERE tbl_customertypes.CustomerTypeID=@CustomerTypeID

	RETURN