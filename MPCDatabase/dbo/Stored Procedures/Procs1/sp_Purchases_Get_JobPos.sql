CREATE PROCEDURE dbo.sp_Purchases_Get_JobPos

	(
		@JobID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_purchase.PurchaseID,tbl_purchase.Code,tbl_suppliers.SupplierName ,tbl_purchase.GrandTotal
        FROM tbl_purchase 
        INNER JOIN tbl_suppliers ON (tbl_purchase.SupplierID = tbl_suppliers.SupplierID) 
        WHERE tbl_purchase.JobID =  @JobID
	
	RETURN