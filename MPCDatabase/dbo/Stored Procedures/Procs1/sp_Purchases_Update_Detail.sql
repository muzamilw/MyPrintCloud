CREATE PROCEDURE dbo.sp_Purchases_Update_Detail
	@PurchaseDetailID int,
	@ItemID int,
	@Qty float,
	@PurchaseID int,
	@price float,
	@packqty int,
	@ItemCode varchar(50),
	@ServiceDetail text,
	@ItemName varchar(255),
	@TaxID int,
	@TotalPrice float,
	@Discount float,
	@NetTax float,
	@freeitems int,
	@ItemBalance float,
	@DepartmentID int
	
AS

UPDATE    tbl_purchasedetail
SET               quantity =@Qty, ItemID =@ItemID , PurchaseID =@PurchaseID , price =@price , packqty =@packqty , ItemCode =@ItemCode , 
				  ServiceDetail =@ServiceDetail , ItemName =@ItemName , TaxID =@TaxID , TotalPrice =@TotalPrice , Discount =@Discount , 
                  NetTax =@NetTax , freeitems =@freeitems , ItemBalance =@ItemBalance , DepartmentID =@DepartmentID  
                  where PurchaseDetailID =@PurchaseDetailID 

	RETURN