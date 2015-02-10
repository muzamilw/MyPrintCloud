CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_GetCustomerFields
	(
		@CompanyID int
	)
AS
	select * from tbl_customizedfields where CompanyID=@CompanyID
	RETURN