CREATE PROCEDURE dbo.sp_Purchases_Get_PurchaseOrderDetail

	(
		@PurchaseID int
	)

AS
	/* SET NOCOUNT ON */
	select PurchaseDetailID,PurchaseID,ItemID,ItemCode,ItemName,ServiceDetail,packqty,quantity,price,TotalPrice,TaxID,NetTax,Discount,freeitems,DepartmentID,ItemBalance from tbl_purchasedetail where PurchaseID=@PurchaseID
	
	RETURN