CREATE PROCEDURE [dbo].[sp_Customers_Customer_CheckCustomerAccountNo]
(
	@AccountNumber varchar(10)
)
AS
	SELECT count(*) FROM tbl_ContactCompanies WHERE AccountNumber=@AccountNumber  

	RETURN