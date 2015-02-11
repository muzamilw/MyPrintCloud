CREATE PROCEDURE dbo.sp_Suppliers_IsContact_Used_SQL_CHK_SUPPLIER_FROM_ACTIVITY
	(
		@SupplierContactID int
	)

AS
SELECT tbl_activity.SupplierContactID
     FROM tbl_activity WHERE tbl_activity.SupplierContactID=@SupplierContactID
	RETURN