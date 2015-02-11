CREATE PROCEDURE dbo.sp_Puchase_Update_PurchaseDetail

AS
	/* SET NOCOUNT ON */
	
	select PurchaseDetailID,PurchaseID,ItemID,ItemCode,ItemName,ServiceDetail,packqty,quantity,price,TotalPrice,TaxID,NetTax,Discount,freeitems,DepartmentID from tbl_purchasedetail where PurchaseDetailID=0
	
	RETURN