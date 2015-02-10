CREATE PROCEDURE dbo.sp_Purchases_ADD_Detail
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

INSERT INTO tbl_purchasedetail
                      ( quantity, ItemID, PurchaseID, price, packqty, ItemCode, ServiceDetail, ItemName, TaxID, TotalPrice, Discount, NetTax, freeitems, 
                      ItemBalance, DepartmentID)
VALUES     ( @Qty, @ItemID, @PurchaseID, @price, @packqty, @ItemCode, @ServiceDetail, @ItemName, @TaxID, @TotalPrice, @Discount, @NetTax, @freeitems, 
                      @ItemBalance, @DepartmentID)
	RETURN