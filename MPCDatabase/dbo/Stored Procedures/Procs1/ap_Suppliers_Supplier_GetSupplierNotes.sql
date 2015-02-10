CREATE PROCEDURE dbo.ap_Suppliers_Supplier_GetSupplierNotes

	(
		@SupplierID int
	)

AS
	SELECT Notes,NotesLastUpdatedDate,tbl_systemusers.FullName as NotesLastUpdatedBy from tbl_suppliers Inner join tbl_systemusers on ( tbl_suppliers.NotesLastUpdatedBy =  tbl_systemusers.SystemuserID ) WHERE SupplierID=@SupplierID

	RETURN