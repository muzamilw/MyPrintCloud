CREATE PROCEDURE dbo.sp_Suppliers_IsSupplierUsed_SQL_CHK_SUPPLIERID_COST_CENTERS
	(
		@PreferredSupplierID int
	)

AS
	SELECT tbl_costcentres.PreferredSupplierID
        FROM tbl_costcentres WHERE tbl_costcentres.PreferredSupplierID=@PreferredSupplierID
	RETURN