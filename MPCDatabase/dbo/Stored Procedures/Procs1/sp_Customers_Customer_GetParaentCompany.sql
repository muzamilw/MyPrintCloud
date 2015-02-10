CREATE PROCEDURE [dbo].[sp_Customers_Customer_GetParaentCompany]
(
	@CustomerID int
)
AS
	SELECT ContactCompanyID,Name FROM tbl_contactCompanies WHERE 
         IsParaentCompany<>0 AND ContactCompanyID<>@CustomerID ORDER BY Name
	RETURN

--select top 1 * from tbl_contactCompanies