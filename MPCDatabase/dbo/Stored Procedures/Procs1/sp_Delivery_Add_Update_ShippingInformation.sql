CREATE PROCEDURE dbo.sp_Delivery_Add_Update_ShippingInformation
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	/* SET NOCOUNT ON */
	
	select * from tbl_shippinginformation where itemid=0
	
	RETURN