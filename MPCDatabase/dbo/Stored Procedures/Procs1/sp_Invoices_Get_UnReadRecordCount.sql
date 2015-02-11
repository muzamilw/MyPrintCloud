CREATE PROCEDURE dbo.sp_Invoices_Get_UnReadRecordCount
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	/* SET NOCOUNT ON */
	
	SELECT InvoiceID , InvoiceStatus FROM tbl_invoices where IsRead=0
	
	RETURN