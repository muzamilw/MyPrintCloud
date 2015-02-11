CREATE PROCEDURE dbo.sp_Suppliers_IsSupplierUsed_SQL_CHK_SUPPLIERID_STOCKITEMS
	(
		@SupplierID int
	)

AS
	SELECT tbl_stockitems.SupplierID
         FROM tbl_stockitems WHERE tbl_stockitems.SupplierID=@SupplierID
	RETURN