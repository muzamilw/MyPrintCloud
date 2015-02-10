CREATE PROCEDURE [dbo].[sp_Suppliers_Supplier_GetAllParaentCompany]

AS
SELECT ContactCompanyID,Name FROM tbl_ContactCompanies
       WHERE IsParaentCompany<>0 ORDER BY Name
	RETURN