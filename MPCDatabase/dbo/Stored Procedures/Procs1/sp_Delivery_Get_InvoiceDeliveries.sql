CREATE PROCEDURE dbo.sp_Delivery_Get_InvoiceDeliveries

	(
		@ID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT  tbl_deliverynotes.DeliveryID,tbl_deliverynotes.Code,tbl_customers.CustomerName,tbl_customers.FlagID,(case tbl_deliverynotes.IsStatus when 1 then 'Un-Delivered' when 2 then 'Delivered' end) as Status
        FROM tbl_deliverynotes
        INNER JOIN tbl_customers ON (tbl_deliverynotes.CustomerId = tbl_customers.CustomerID)
        WHERE tbl_deliverynotes.InvoiceID = @ID
	
	RETURN