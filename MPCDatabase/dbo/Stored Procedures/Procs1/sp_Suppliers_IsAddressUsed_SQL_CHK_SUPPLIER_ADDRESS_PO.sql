CREATE PROCEDURE dbo.sp_Suppliers_IsAddressUsed_SQL_CHK_SUPPLIER_ADDRESS_PO
	(
		@SupplierContactAddressID int
	)

AS
SELECT tbl_purchase.SupplierContactAddressID
     FROM tbl_purchase WHERE tbl_purchase.SupplierContactAddressID=@SupplierContactAddressID
	RETURN