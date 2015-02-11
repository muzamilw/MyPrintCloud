CREATE PROCEDURE [dbo].[sp_Suppliers_Supplier_GetParaentCompany]
	(
	@SupplierID int
	)
AS
SELECT ContactCompanyID,Name FROM tbl_ContactCompanies 
       WHERE IsParaentCompany<>0 AND ContactCompanyID<>@SupplierID ORDER BY Name
	RETURN