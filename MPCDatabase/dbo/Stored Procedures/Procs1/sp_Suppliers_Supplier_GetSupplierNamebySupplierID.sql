CREATE PROCEDURE [dbo].[sp_Suppliers_Supplier_GetSupplierNamebySupplierID]
(
@SupplierID int
)
AS
SELECT Name   from tbl_contactCompanies where tbl_contactCompanies.ContactCompanyID = @SupplierID 

	RETURN