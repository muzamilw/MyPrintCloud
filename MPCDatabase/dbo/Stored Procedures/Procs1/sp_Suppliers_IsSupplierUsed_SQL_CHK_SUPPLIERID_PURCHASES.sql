CREATE PROCEDURE dbo.sp_Suppliers_IsSupplierUsed_SQL_CHK_SUPPLIERID_PURCHASES
	(
		@SupplierID int
	)

AS
	SELECT tbl_purchase.SupplierID
        FROM tbl_purchase WHERE tbl_purchase.SupplierID=@SupplierID
	RETURN