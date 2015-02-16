CREATE PROCEDURE [dbo].[sp_Purchases_Get_OrderPos]

	(
		@OrderID int
	)

AS
	/* SET NOCOUNT ON */
	
	
	SELECT tbl_purchase.PurchaseID,tbl_purchase.Code,tbl_ContactCompanies.Name,tbl_purchase.GrandTotal
        FROM tbl_items
        INNER JOIN tbl_purchase ON (tbl_items.EstimateId = tbl_purchase.JobID)
        INNER JOIN tbl_ContactCompanies ON (tbl_purchase.SupplierID = tbl_ContactCompanies.ContactCompanyID)
        WHERE tbl_items.EstimateID  =  @OrderID
	
	RETURN