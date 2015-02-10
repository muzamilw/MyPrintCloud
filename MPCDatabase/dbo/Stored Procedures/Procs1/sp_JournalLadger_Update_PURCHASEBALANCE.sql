Create PROCEDURE dbo.sp_JournalLadger_Update_PURCHASEBALANCE 
	@PurchaseID int
AS
	update tbl_purchasedetail  
	set itembalance = totalprice - (TotalPrice  * isnull(Discount,0) /100) 
	where PurchaseID = @PurchaseID

RETURN