CREATE PROCEDURE [dbo].[sp_Customers_Customer_GetAllParaentCompany]
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	SELECT ContactCompanyID,Name FROM tbl_ContactCompanies WHERE 
     IsParaentCompany<>0 ORDER BY Name
	RETURN