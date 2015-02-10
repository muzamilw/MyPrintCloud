CREATE PROCEDURE dbo.sp_Invoice_checkbyestimate

	(
	@EstimateID int
	)

AS
	/* SET NOCOUNT ON */
	
	select InvoiceID from tbl_invoices where EstimateID=@EstimateID
	
	RETURN