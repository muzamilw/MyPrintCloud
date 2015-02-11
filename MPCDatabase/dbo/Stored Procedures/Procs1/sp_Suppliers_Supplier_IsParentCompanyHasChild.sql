CREATE PROCEDURE dbo.sp_Suppliers_Supplier_IsParentCompanyHasChild
(
 @SupplierID int
)
AS
	Select ParaentCompanyID from tbl_suppliers WHERE ParaentCompanyID=@SupplierID
	RETURN